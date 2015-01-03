namespace Recipes.Domain
{
	public interface IAggregate
	{
		int Id { get; }
		int Version { get; }
	}
}