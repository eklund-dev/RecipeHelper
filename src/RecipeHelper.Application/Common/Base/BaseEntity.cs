namespace RecipeHelper.Application.Common.Base
{
    public interface IBaseEntity<TId>
    {
        public TId Id { get; set; }

        public string Name { get; set; }
    }
}
