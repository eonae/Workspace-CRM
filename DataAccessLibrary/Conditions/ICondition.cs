
namespace DataAccessLibrary
{
    public interface ICondition
    {
        string FieldName { get; }
        string GetString();
        bool Match(object value); //////
    }
}
