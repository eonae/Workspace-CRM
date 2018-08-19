
namespace DataAccessLibrary
{
    public class EqualsCondition : ICondition
    {
        // Класс уязвим к Sql-инъекциям
        public string FieldName { get; private set; }
        public object Argument { get; private set; }


        public EqualsCondition(string fieldName, object argument)
        {
            FieldName = fieldName;
            Argument = argument;
        }

        public string GetString()
        {
            return $"{FieldName} = {Argument}";
        }

        public bool Match(object value)
        {
            return false; ////
        }
    }
}
