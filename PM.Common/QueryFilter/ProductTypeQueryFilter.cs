namespace PM.Common.QueryFilter
{
    public class ProductTypeQueryFilter : BaseQueryFilter
    {
        public string Search { get; set; }
        public SortExpression Sorts { get; set; }
    }
}
