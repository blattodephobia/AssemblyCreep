using Moq;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyCreepProjectMergeTests
{
    public static class TestUtils
    {
        public static Mock<IEventAggregator> GetMockAggregator()
        {
            return new Mock<IEventAggregator>();
        }

        public static Mock<TEvent> GetMockEvent<TEvent, TPayload>(Action<TPayload> callback)
            where TEvent : PubSubEvent<TPayload>
        {
            var result = new Mock<TEvent>();
            result.Setup(@event => @event.Publish(It.IsAny<TPayload>())).Callback(callback);
            return result;
        }
    }
}
