namespace RecipeHelper.Application.Common.Contracts
{
    public interface IResponse<TData>
    {
        TData Data { get; set; }
    }
}
