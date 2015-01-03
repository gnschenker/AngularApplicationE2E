namespace Recipes.Domain
{
	public class RecipeAggregate : IAggregate
	{
		public int Id { get; private set; }
		public int Version { get; private set; }

		public RecipeAggregate(int id)
		{
			Id = id;
		}
		
		public void Create(string name, string category, string userName)
		{
			// TODO
		}
	}
}