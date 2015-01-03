using System;

namespace Recipes.Domain
{
	public interface IRecipeApplicationService 
	{
		int When(CreateRecipe cmd);
	}

	public class RecipeApplicationServiceFake : IRecipeApplicationService
	{
		public int When(CreateRecipe cmd)
		{
			return 99;
		}
	}

	public class RecipeApplicationService : IRecipeApplicationService
	{
		private readonly IRepository repository;
		private readonly IUniqueKeyGenerator keyGenerator;

		public RecipeApplicationService(IRepository repository, IUniqueKeyGenerator generator)
		{
			this.repository = repository;
			this.keyGenerator = generator;
		}

		public int When(CreateRecipe cmd)
		{
			var id = keyGenerator.GetId<RecipeAggregate>();
            Console.WriteLine("creating recipe aggregate with id={0}", id);
			var aggregate = repository.Get<RecipeAggregate>(id);
			aggregate.Create(cmd.Name, cmd.Category, cmd.UserName);
			repository.Save(aggregate);
			return id;
		}
	}
}