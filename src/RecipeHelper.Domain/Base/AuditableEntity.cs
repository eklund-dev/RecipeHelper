namespace RecipeHelper.Domain.Base
{
    public class AuditableEntity
    {
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? LastmodifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
