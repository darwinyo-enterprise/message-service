[![Build Status](https://travis-ci.org/darwinyo-enterprise/message-service.svg?branch=master)](https://travis-ci.org/darwinyo-enterprise/message-service)


# Publish-Subscribe Pattern Example

## Table of contents

* [What is all About?](#what-is-all-about)
* [Notes](#notes)
* [How Its works?](#how-its-works)

## What is All About?

This is Demo for Publish And Subscribe Pattern.

## Notes

Technology Used : Net Core 2.0, Net Standard, XUnit, Travis CI
Written By : Darwin Yo.

## How Its Works?

This Pattern is best suits if we have Some Object that Need To Know The Other Object To Works.
Example, If We have UI, Probably we have 1 component that need to know the other component state, or current Application state, Like We have to Logged in to saw our Account settings right. It Doesn't Make sense if we hasn't logged in, but we already able to see account setting button.

Basicly this pattern is using push notification when that event triggered, rather than we do refresh every 5 sec/less.

In This Project, I have Implement Abstraction Layer for easy development, 
They located on :
- MessageService                => Contains Concrete Layer
- MessageService.Interface      => Everything will be Inherits and Using This Interfaces.

Clients And Implementation located on :
- MessageService.Client         => Client Console, And Implementation of Abstraction Layer.

Tests Located on :
- MessageService.Client.Tests   => Unit Test

Basic Abstraction Will have Publisher => Responsible for Broadcast Observables
``` C#
/// <summary>
    /// Base Abstraction Layer To Implement other Publishers
    /// </summary>
    /// <typeparam name="T">
    /// Object For Broadcast to other subscriber
    /// </typeparam>
    public class Publisher<T> : IPublisher<T> where T : class, new()
    {

        private event EventHandler<T> _eventHandler;


        /// <summary>
        /// Event To Notify Publisher has Triggered Send...
        /// </summary>
        public event EventHandler<T> eventHandler
        {
            add
            {
                // Avoid Same Delegate Registered
                if (_eventHandler == null || !_eventHandler.GetInvocationList().Contains(value))
                {
                    _eventHandler += value;
                }
            }
            remove
            {
                _eventHandler -= value;
            }
        }

        /// <summary>
        /// Publisher Broadcast...
        /// </summary>
        /// <param name="message">
        /// Object To Broadcast..
        /// </param>
        public virtual void Send(T message)
        {
            OnEventTriggered(message);
        }

        /// <summary>
        /// When Event Triggered Then Send To Subscribers...
        /// </summary>
        /// <param name="message"></param>
        protected virtual void OnEventTriggered(T message)
        {
            // If Someone Subscribe then invoke their method...
            _eventHandler?.Invoke(this, message);
        }
        ...
```

Subject => Kinda What We Care About. Responsible To Subscribe To Publisher.
``` C#
/// <summary>
    /// base subject to implement to others subjects...
    /// </summary>
    /// <typeparam name="TPublisher">
    /// publisher to subscribe...
    /// </typeparam>
    /// <typeparam name="T">
    /// Object to Subscribe
    /// </typeparam>
    public class Subject<TPublisher, T> : ISubject<TPublisher, T>
        where T : class, new()
        where TPublisher : IPublisher<T>
    {
        public Subject()
        {
        }

        /// <summary>
        /// Add Subscriber To Subject.
        /// </summary>
        /// <param name="subscriber">
        /// Subscriber To Add.
        /// </param>
        /// <returns></returns>
        public virtual void Subscribe(TPublisher publisher, ISubscriber<T> subscriber)
        {
            try
            {
                // Subscribe Subscriber To Publisher...
                publisher.eventHandler += subscriber.Listen;
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// Remove Subscriber From Subscribe.
        /// </summary>
        /// <param name="subscriber">
        /// Subscriber To Remove.
        /// </param>
        public virtual void Unsubscribe(TPublisher publisher, ISubscriber<T> subscriber)
        {
            try
            {
                // unsubcribes
                publisher.eventHandler -= subscriber.Listen;
            }
            catch
            {

                throw;
            }
        }
    }
```

And Subscriber => Individual That Wants To Get Notification Whenever the Publisher That Individual care about, Emit Observable.

``` C#
/// <summary>
    /// base subscriber that will inherited by other Subscribers...
    /// </summary>
    /// <typeparam name="T">
    /// Object To get
    /// </typeparam>
    public abstract class Subscriber<T> : ISubscriber<T> where T : class, new()
    {
        private event EventHandler<T> _listenEventHandler;

        /// <summary>
        /// Event To Notify Publisher has Triggered Send...
        /// </summary>
        public event EventHandler<T> listenEventHandler
        {
            add
            {
                // Avoid Same Delegate Registered
                if (_listenEventHandler == null || !_listenEventHandler.GetInvocationList().Contains(value))
                {
                    _listenEventHandler += value;
                }
            }
            remove
            {
                _listenEventHandler -= value;
            }
        }

        /// <summary>
        /// This Will Triggered Every Time Publisher dispatch event...
        /// And Will Trigger listenEventHandler
        /// </summary>
        /// <param name="sender">
        /// Sender Object
        /// </param>
        /// <param name="args">
        /// Arguments passed
        /// </param>
        public virtual void Listen(object sender, T args)
        {
            _listenEventHandler?.Invoke(this, args);
        }
    }
```