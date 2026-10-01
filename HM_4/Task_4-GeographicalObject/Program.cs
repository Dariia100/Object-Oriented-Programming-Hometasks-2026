using static Task_4_GeographicalObject.Program;
using static Task_4_GeographicalObject.Program.GeoObject;

namespace Task_4_GeographicalObject
{
    internal class Program
    {

        public abstract class GeoObject
        {
            public double X { get; set; }
            public double Y { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }

            public GeoObject() { }
            protected GeoObject(double x, double y, string n, string d)
            {
                X = x;
                Y = y;
                Name = n;
                Description = d;
            }
            public virtual string GetInfo()
            {
                return $"Назва: {Name}, опис: {Description}, координати: ({X}; {Y}); ";
            }
        }

        public class River : GeoObject
            {
                public double Speed { get; set; }
                public double Length { get; set; }
                public River(double x, double y, string n, string d, double s, double l) : base(x, y, n, d)
                {
                    Speed = s;
                    Length = l;
                }

                public override string GetInfo()
                {
                    return base.GetInfo() + $"швидкість течії (см/с): {Speed}; довжина: {Length}";
                }
            }

        public class Mountain : GeoObject
            {
                public double HighestPoint { get; set; }
                public Mountain(double x, double y, string n, string d, double h) : base(X, Y, N, D)
                {
                    HighestPoint = h;
                }
                public override string GetInfo()
                {
                    return base.GetInfo() + $"найвища точка: {HighestPoint}";
                }

            }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            River river = new River(2, 2, "Десна", "протікає через Київ", 22, 4000);
            Mountain mountain = new Mountain(4, 6, "Еверест", "найвища гора планети", 6000);
            Console.WriteLine(river.GetInfo());
            Console.WriteLine(mountain.GetInfo());
            Console.ReadKey();
        }
    }
}
