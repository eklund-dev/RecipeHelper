namespace RecipeHelper.Application.Common.Contracts
{
    public interface ISingleEntityResponse<TData>
    {
        TData Data { get; set; }        
    }
}
