using Recipes.Models;

namespace Recipes.Domain{
	public static class ExtensExternalCommands{
		public static CreateRecipe ToInternal(this CreateRecipeCommand c){
			return new CreateRecipe{
				Name = c.name,
				Category = c.category,
				UserName = c.userName
			};
		}
	}
}