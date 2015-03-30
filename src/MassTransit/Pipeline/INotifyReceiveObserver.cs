﻿// Copyright 2007-2015 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.Pipeline
{
    using System;


    /// <summary>
    /// An observer that can monitor a receive endpoint to track message consumption at the 
    /// endpoint level.
    /// </summary>
    public interface INotifyReceiveObserver
    {
        /// <summary>
        /// Called when a message has been delivered by the transport is about to be received by the endpoint
        /// </summary>
        /// <param name="context">The receive context of the message</param>
        /// <returns></returns>
        void NotifyPreReceive(ReceiveContext context);

        /// <summary>
        /// Called when the message has been received and acknowledged on the transport
        /// </summary>
        /// <param name="context">The receive context of the message</param>
        /// <returns></returns>
        void NotifyPostReceive(ReceiveContext context);

        /// <summary>
        /// Called when a message has been consumed by a consumer
        /// </summary>
        /// <typeparam name="T">The message type</typeparam>
        /// <param name="context">The message consume context</param>
        /// <param name="duration">The consumer duration</param>
        /// <param name="consumerType">The consumer type</param>
        /// <returns></returns>
        void NotifyPostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType)
            where T : class;

        /// <summary>
        /// Called when a message being consumed produced a fault
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="consumerType"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        void NotifyConsumeFault<T>(ConsumeContext<T> context, string consumerType, Exception exception)
            where T : class;

        /// <summary>
        /// Called when the transport receive faults
        /// </summary>
        /// <param name="context">The receive context of the message</param>
        /// <param name="exception">The exception that was thrown</param>
        /// <returns></returns>
        void NotifyReceiveFault(ReceiveContext context, Exception exception);
    }
}