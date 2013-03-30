using System;

namespace Nancy.Commandly.Tests.Fixtures
{
    
    public class Test
    {
        public Test()
        {
        }

        public Test(string name, string description, Guid aggregateId)
        {
            Name = name;
            Description = description;
            AggregateId = aggregateId;
        }

        
        public string Name { get; set; }

        
        public string Description { get; set; }

        
        public Guid AggregateId { get; set; }

        
        public DateTime Date { get; set; }

        
        public bool IsChecked { get; set; }
    }
}