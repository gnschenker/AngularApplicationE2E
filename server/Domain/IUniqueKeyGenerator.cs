namespace Recipes.Domain
{
	public interface IUniqueKeyGenerator 
	{
		int GetId<T>();
	}
}