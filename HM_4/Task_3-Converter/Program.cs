namespace Task_3_Converter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            Converter converter = new Converter(44.86m, 50.91m);

            while (true)
            {
                Console.WriteLine("Оберіть операцію:");
                Console.WriteLine("1 - з гривень в долари");
                Console.WriteLine("2 - з гривень в євро");
                Console.WriteLine("3 - з доларів в гривні");
                Console.WriteLine("4 - з євро в гривні");
                string operation = Console.ReadLine();

                decimal result = 0;
                if (operation == "1")
                {
                    try
                    {
                        Console.WriteLine("Введіть суму в гривнях: ");
                        string input = Console.ReadLine();
                        decimal uah = decimal.Parse(input);
                        if (uah <= 0)
                        {
                            throw new Exception("Помилка: Ви ввели від'ємне число або нуль");
                        }
                        result = converter.UAHtoUSD(uah);
                        Console.WriteLine($"Результат: {result:F2}");
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Помилка: Ви не ввели число");
                    }

                }

                else if (operation == "2")
                {
                    try
                    {
                        Console.WriteLine("Введіть суму в гривнях: ");
                        string input = Console.ReadLine();
                        decimal uah = decimal.Parse(input);
                        if (uah <= 0)
                        {
                            throw new Exception("Помилка: Ви ввели від'ємне число або нуль");
                        }
                        result = converter.UAHtoEUR(uah);
                        Console.WriteLine($"Результат: {result:F2}");
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Помилка: Ви не ввели число");
                    }
                }

                else if (operation == "3")
                {
                    try
                    {
                        Console.WriteLine("Введіть суму в доларах: ");
                        string input = Console.ReadLine();
                        decimal usd = decimal.Parse(input);
                        if (usd <= 0)
                        {
                            throw new Exception("Помилка: Ви ввели від'ємне число або нуль");
                        }
                        result = converter.USDtoUAH(usd);
                        Console.WriteLine($"Результат: {result:F2}");
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Помилка: Ви не ввели число");
                    }
                }

                else if (operation == "4")
                {
                    try
                    {
                        Console.WriteLine("Введіть суму в євро: ");
                        string input = Console.ReadLine();
                        decimal eur = decimal.Parse(input);
                        if (eur <= 0)
                        {
                            throw new Exception("Помилка: Ви ввели від'ємне число або нуль");
                        }
                        result = converter.EURtoUAH(eur);
                        Console.WriteLine($"Результат: {result:F2}");
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Помилка: Ви не ввели число");
                    }
                }

                else
                {
                    Console.WriteLine("Помилка введення.");
                }
            }
        }

        class Converter
        {
            public decimal USDrate { get; private set; }  
            public decimal EURrate { get; private set; }

            public Converter(decimal usdRate, decimal eurRate)
            {
                USDrate = usdRate;
                EURrate = eurRate;
            }

            public decimal UAHtoUSD(decimal uah)
            {
                return uah / USDrate;
            }

            public decimal UAHtoEUR(decimal uah)
            {
                return uah / EURrate;
            }

            public decimal USDtoUAH(decimal usd)
            {
                return usd * USDrate;
            }

            public decimal EURtoUAH(decimal eur)
            {
                return eur * EURrate;
            }
        }
        
    }
}
