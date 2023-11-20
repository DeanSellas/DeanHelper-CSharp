using System.Text;

namespace DeanHelper
{
    public class IEnum
    {
        public static void ToCSV<T>(IEnumerable<T> l, string path = "", string filename = "out.csv")
        {
            if (l.Count() == 0)
            {
                return;
            }

            if (path == "" || path == null)
            {
                path = Directory.GetCurrentDirectory();
            }
            path += string.Format("\\{0}", filename);

            // Create a StringBuilder to build the CSV content
            StringBuilder csvContent = new StringBuilder();

            // Get property names from the first item in the list
            var propertyNames = GetPropertyNames(l.First());

            // Append header
            csvContent.AppendLine(string.Join(",", propertyNames));

            // Append data
            foreach (var data in l)
            {
                // Extract property values using reflection
                var propertyValues = GetPropertyValues(data, propertyNames);
                csvContent.AppendLine(string.Join(",", propertyValues));
            }

            // Write the CSV content to the file
            File.WriteAllText(path, csvContent.ToString());
        }
        static List<string> GetPropertyNames(dynamic obj)
        {
            var propertyNames = new List<string>();
            var type = obj.GetType();

            foreach (var property in type.GetProperties())
            {
                propertyNames.Add(property.Name);
            }

            return propertyNames;
        }

        static List<string> GetPropertyValues(dynamic obj, List<string> propertyNames)
        {
            var propertyValues = new List<string>();

            foreach (var propertyName in propertyNames)
            {
                // Use reflection to get the value of each property
                var propertyValue = obj.GetType().GetProperty(propertyName).GetValue(obj, null);
                propertyValues.Add(propertyValue.ToString());
            }

            return propertyValues;
        }
    }
}