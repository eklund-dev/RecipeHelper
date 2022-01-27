namespace RecipeHelper.Application.Features.Ingredients
{
    public class IngredientQueryParameters
    {
        const int _maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 5;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > _maxPageSize) ? _maxPageSize : value; }
        }
    }
}
