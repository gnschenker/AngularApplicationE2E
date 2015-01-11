using System;
using System.Collections.Generic;
using System.Linq;
using EventStore.ClientAPI;

namespace Recipes.Domain
{
	public class GesRepository : IRepository
	{
	    private readonly IEventStoreConnection connection;
	    private readonly IAggregateFactory factory;
	 
	    public GesRepository(IEventStoreConnection connection, IAggregateFactory factory)
	    {
	        this.connection = connection;
	        this.factory = factory;
	    }

		public T Get<T>(int id) where T : IAggregate
		{
		    var events = new List<object>();
		    StreamEventsSlice currentSlice;
		    var nextSliceStart = StreamPosition.Start;
		    var streamName = GetStreamName<T>(id);
		    do
		    {
		        currentSlice = connection
		            .ReadStreamEventsForwardAsync(streamName, nextSliceStart, 200, false)
		            .Result;
		        nextSliceStart = currentSlice.NextEventNumber;
		        events.AddRange(currentSlice.Events.Select(x => x.DeserializeEvent()));
		    } while (!currentSlice.IsEndOfStream);
		    var aggregate = factory.Create<T>(events);
		    return (T)aggregate;
		}	

		public void Save(IAggregate aggregate)
		{
		    var commitId = Guid.NewGuid();
		    var events = aggregate.GetUncommittedEvents().ToArray();
		    if (events.Any() == false)
		        return;
		    var streamName = GetTheStreamName(aggregate.GetType(), aggregate.Id);
		    var originalVersion = aggregate.Version - events.Count();
		    var expectedVersion = originalVersion == 0 ? ExpectedVersion.NoStream : originalVersion-1;
		    var commitHeaders = new Dictionary<string, object>
		                        {
		                            {"CommitId", commitId},
		                            {"AggregateClrType", aggregate.GetType().AssemblyQualifiedName}
		                        };
		    var eventsToSave = events.Select(e => e.ToEventData(commitHeaders)).ToList();
		    connection.AppendToStreamAsync(streamName, expectedVersion, eventsToSave).Wait();
		    aggregate.ClearUncommittedEvents();
		}

		private string GetStreamName<T>(int id)
		{
		    return GetTheStreamName(typeof(T), id);
		}
		 
		private string GetTheStreamName(Type type, int id)
		{
		    return string.Format("{0}-{1}", type.Name, id);
		}
	}
}