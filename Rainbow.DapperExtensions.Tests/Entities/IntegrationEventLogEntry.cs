using Rainbow.DapperExtensions.Mapper;
using System;

namespace Rainbow.DapperExtensions.Tests.Entities
{
    public class IntegrationEventLogEntry
    {
        public Guid EventId { get; set; }
        public string EventTypeName { get; private set; }

        public EventStateEnum State { get; set; }
        public int TimesSent { get; set; }
        public DateTime CreationTime { get; private set; }
        public string Content { get; private set; }
        public string TransactionId { get; private set; }

    }

    public enum EventStateEnum
    {
        NotPublished = 0,
        InProgress = 1,
        Published = 2,
        PublishedFailed = 3
    }

    public class IntegrationEventLogEntryMapper : ClassMapper<IntegrationEventLogEntry>
    {
        public IntegrationEventLogEntryMapper()
        {
            Table("IntegrationEventLogEntry");
            Map(p => p.EventId).Key(KeyType.Guid);
            AutoMap();
        }
    }
}
