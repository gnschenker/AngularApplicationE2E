namespace Recipes.Contracts
{
	public class RecipeCreated
	{
		public RecipeCreated(int id, string name, string category, string userName)
		{
			this.Id = id;
			this.Name = name;
			this.Category = category;
			this.UserName = userName;
		}
		
		public int Id { get; private set; }
		public string Name { get; private set; }
		public string Category { get; private set; }
		public string UserName { get; private set; }
	}
}