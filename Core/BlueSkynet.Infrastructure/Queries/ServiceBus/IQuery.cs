﻿namespace BlueSkynet.Infrastructure.Queries.ServiceBus
{
    public interface IQuery<in TQuery, out TResult>
    {
        TResult Execute(TQuery args);
    }
}