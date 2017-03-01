using BlueSkynet.Domain.Extentions;
using BlueSkynet.Domain.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueSkynet.TestUtilities
{
    public static class AggregationRootExtentions
    {
        public static void AssertContainsEvent(this IEnumerable<Event> collection, Type type)
        {
            Assert.IsTrue(collection.Any(x => x.GetType() == type));
        }

        public static void AssertContainsEvent<T>(this IEnumerable<Event> collection, Func<T, bool> predicate = null) where T : Event
        {
            Assert.IsTrue(collection.Any(x => x.GetType() == typeof(T)),
                      $"There was no {typeof(T).Name} in the collection");
            if (!predicate.IsNull())
                Assert.IsTrue(collection.Where(x => x.GetType() == typeof(T)).OfType<T>().Any(predicate),
                    $"There was no {typeof(T).Name} in the collection that matched the predicate");
        }
    }
}