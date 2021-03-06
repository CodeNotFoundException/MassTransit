﻿// Copyright 2007-2017 Chris Patterson, Dru Sellers, Travis Smith, et. al.
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
namespace MassTransit.Context.Converters
{
    using System;
    using System.Linq;
    using GreenPipes.Filters;
    using Internals.Extensions;


    public class SendContextConverterFactory :
        IPipeContextConverterFactory<SendContext>
    {
        IPipeContextConverter<SendContext, TOutput> IPipeContextConverterFactory<SendContext>.GetConverter<TOutput>()
        {
            var innerType = typeof(TOutput).GetClosingArguments(typeof(SendContext<>)).Single();

            return (IPipeContextConverter<SendContext, TOutput>)Activator.CreateInstance(typeof(Converter<>).MakeGenericType(innerType));
        }


        class Converter<T> :
            IPipeContextConverter<SendContext, SendContext<T>>
            where T : class
        {
            bool IPipeContextConverter<SendContext, SendContext<T>>.TryConvert(SendContext input, out SendContext<T> output)
            {
                output = input as SendContext<T>;

                return output != null;
            }
        }
    }
}