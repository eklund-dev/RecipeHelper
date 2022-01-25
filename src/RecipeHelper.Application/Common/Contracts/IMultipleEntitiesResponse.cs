namespace RecipeHelper.Application.Common.Contracts
{
    public interface IMultipleEntitiesResponse<TData>
    {
        IEnumerable<TData> Data { get; set; }        
    }
}
