namespace PM.Common.QueryFilter
{
    public class UserQueryFilter : BaseQueryFilter
    {
        public string Search { get; set; }
        public SortExpression Sorts { get; set; }
    }
}
