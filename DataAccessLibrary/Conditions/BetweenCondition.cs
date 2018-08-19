
namespace DataAccessLibrary
{
    public class BetweenCondition
    {
        // Класс уязвим к Sql-инъекциям
        // Надо посмотреть, что будет, если ссылочный тип передавать. Может быть всё-таки строки надо?
        public string FieldName { get; private set; }
        public object Min { get; private set; }
        public object Max { get; private set; }

        public BetweenCondition(string fieldName, object min, object max)
        {
            FieldName = fieldName;
            Min = min;
            Max = max;
        }

        public string GetString()
        {
            return $"{FieldName} BETWEEN {Min} AND {Max}";
        }

        public bool Match(object value)
        {
            return false; ////
        }
    }
}
