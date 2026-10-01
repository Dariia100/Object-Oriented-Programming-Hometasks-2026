namespace Task_4_GeographicalObject_2
{
    public interface IGeoObject
    {
        double X { get; set; }
        double Y { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string GetInfo();
    }

    public class River : IGeoObject
    {
        public double X { get; set; }
        public double Y { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Speed { get; set; }
        public double Length { get; set; }

        public River(double x, double y, string n, string d, double s, double l)
        {
            X = x;
            Y = Y;
            Name = n;
            Description = d;
            Speed = s;
            Length = l;
        }
        public string getInfo()
        {
            return $"Назва: {Name}, опис: {Description}, координати: ({X}; {Y}); швидкість течії (см/с): {Speed}; довжина: {Length}";
        }
    }

    public class Mountain : IGeoObject
    {
        public double X { get; set; }
        public double Y { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double HighestPoint { get; set; }

        public Mountain(double x, double y, string n, string d, double h)
        {
            X = x;
            Y = y;
            Name = n;
            Description = d;
            HighestPoint = h;
        }
        public string getInfo()
        {
            return $"Назва: {Name}, опис: {Description}, координати: ({X}; {Y}); найвища точка: {HighestPoint}";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            IGeoObject river = new River(2, 2, "Десна", "Протікає через Київ", 22, 4000);
            IGeoObject mountain = new Mountain(4, 6, "Еверест", "найвища гора планети", 6000);
            Console.WriteLine(river.GetInfo());
            Console.WriteLine(mountain.GetInfo());
            Console.ReadKey();
        }
    }
}
