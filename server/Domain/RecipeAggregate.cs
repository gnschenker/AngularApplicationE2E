using System.Collections.Generic;
using Recipes.Contracts;

namespace Recipes.Domain
{
	public class RecipeAggregate : AggregateBase<RecipeAggregate>
	{
		public RecipeAggregate(IEnumerable<object> events = null) : base(events)
		{
		}
		
		public void Create(int id, string name, string category, string userName)
		{
			Apply(new RecipeCreated(id, name, category, userName));
		}

		private void When(RecipeCreated e)
		{
			Id = e.Id;
		}
	}
}