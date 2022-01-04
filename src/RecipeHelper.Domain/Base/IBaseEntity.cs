namespace RecipeHelper.Domain.Base
{
    public interface IBaseEntity<TId>
    {
        TId Id { get; set; }

        string Name { get; set; }
    }
}
