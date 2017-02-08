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
    }
}