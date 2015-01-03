using System;
using System.Collections.Generic;

namespace Recipes.Domain
{
	public class InMemoryRepository : IRepository
	{
		private readonly static object sync = new object();
		private readonly Dictionary<Type, Dictionary<int, IAggregate>> cache =
			new Dictionary<Type, Dictionary<int, IAggregate>>();

		public T Get<T>(int id) where T : IAggregate
		{
			lock(sync)
			{
				var key = typeof(T);
				if(cache.ContainsKey(key) == false)
					cache[key] = new Dictionary<int, IAggregate>();
				if(cache[key].ContainsKey(id) == false)
					cache[key].Add(id, GetNewInstance<T>(id));
				return (T)cache[key][id];
			}
		}

		public void Save(IAggregate aggregate)
		{
			// TODO
		}

		private IAggregate GetNewInstance<T>(int id) where T : IAggregate
		{
			if(typeof(T) == typeof(RecipeAggregate))
				return new RecipeAggregate(id);

			throw new ArgumentException("Unexpected aggregate type");
		}
	}
}