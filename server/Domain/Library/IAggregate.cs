using System.Collections.Generic;

namespace Recipes.Domain
{
	public interface IAggregate
	{
	    IEnumerable<object> GetUncommittedEvents();
	    void ClearUncommittedEvents();
	    int Id { get; }
	    int Version { get; }
	}
}