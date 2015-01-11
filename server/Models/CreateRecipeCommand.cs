namespace Recipes.Models{
	public class CreateRecipeCommand
	{
		public string name { get; set; }
		public string category { get; set; }
		public string userName { get; set; }

		public override string ToString()
		{
			return string.Format("name={0}, category={1}, userName={2}", name, category, userName);
		}
	}
}