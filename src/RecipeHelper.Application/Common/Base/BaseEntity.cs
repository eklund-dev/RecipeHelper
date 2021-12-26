namespace RecipeHelper.Application.Common.Base
{
    public interface IBaseEntity<TId>
    {
        TId Id { get; set; }

        string Name { get; set; }
    }
}
