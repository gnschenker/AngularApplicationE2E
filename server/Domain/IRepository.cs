namespace Recipes.Domain
{
	public interface IRepository 
	{
		T Get<T>(int id) where T: IAggregate; 
		void Save(IAggregate aggregate);
	}
}