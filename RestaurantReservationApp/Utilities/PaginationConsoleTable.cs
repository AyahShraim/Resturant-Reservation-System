using ConsoleTables;
using System.Reflection;

namespace RestaurantReservationApp.Utilities
{
    public static class PaginationConsoleTable<T>
    {
        public static void Paginate(IEnumerable<T> items, int pageSize)
        {
            var pageNumber = 1;
            var totalSections = items.Count();
            var totalPages = (int)Math.Ceiling((double)totalSections / pageSize);

            while (pageNumber <= totalPages)
            {
                if(pageNumber != 1) Console.Clear();
                Console.WriteLine($"Page {pageNumber}");
                var currentPage = items.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                DisplayTable(currentPage);
                DisplayPageNumbers(pageNumber, totalPages);
                Console.ReadKey();
                Console.Clear();
                pageNumber++;
            }
        }

        private static void DisplayTable(IEnumerable<T> items)
        {
            var table = new ConsoleTable();
            var properties = typeof(T).GetProperties();
            var displayProperties = properties
                .Where(p => !IsListType(p))
                .Select(p => p.Name)
                .ToArray();

            table.AddColumn(displayProperties);
            foreach (var item in items)
            {
                table.AddRow(displayProperties.Select(p => properties.First(prop => prop.Name == p).GetValue(item)?.ToString()).ToArray());
            }
            table.Write();
            Console.WriteLine();

            static bool IsListType(PropertyInfo property)
            {
                return property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>);
            }
        }

        private static void DisplayPageNumbers(int currentPage, int totalPages)
        {
            Console.WriteLine();
            for (int p = 1; p <= totalPages; p++)
            {
                Console.ForegroundColor = p == currentPage ? ConsoleColor.Yellow : ConsoleColor.DarkGray;
                Console.Write($"{p} ");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}