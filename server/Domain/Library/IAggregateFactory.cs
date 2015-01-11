using System.Collections.Generic;

namespace Recipes.Domain
{
	public interface IAggregateFactory
	{
		IAggregate Create<T>(IEnumerable<object> events) where T: IAggregate;
	}
}