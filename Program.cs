namespace WordFrequency;

internal class Program
{
    static void Main(string[] args)
    {
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            string[] text;
            try
            {
                using (StreamReader reader = File.OpenText(args[0]))
                {
                    text = (new string(reader.ReadToEnd().Where(c => !char.IsPunctuation(c)).ToArray())).Split();
                }
                var result = text
                    .Where(word => !string.IsNullOrWhiteSpace(word))
                    .GroupBy(word => word)
                    .OrderByDescending(group => group.Count())
                    .Select(group => group.Key)
                    .Take(10);
                foreach (var item in result)
                {
                    Console.WriteLine(item);
                }
                Console.Read();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Файл {args[0]} не найден.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Путь к файлу или папке содержит недопустимые символы.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Недопустимое действие или недостаточно прав на чтение файлов.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}