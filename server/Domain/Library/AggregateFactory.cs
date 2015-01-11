using System;
using System.Collections.Generic;

namespace Recipes.Domain
{
	public class AggregateFactory : IAggregateFactory
	{
	    public IAggregate Create<T>(IEnumerable<object> events) where T: IAggregate
	    {
	        if (typeof (T) == typeof (RecipeAggregate))
	            return new RecipeAggregate(events);
	 
	        throw new ArgumentException("Unknown aggregate type");
	    }
	}
}