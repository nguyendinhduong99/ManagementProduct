namespace PM.Common.QueryFilter
{
    public class ProductQueryFilter : BaseQueryFilter
    {
        public string Search { get; set; }
        public SortExpression Sorts { get; set; }
    }
}
