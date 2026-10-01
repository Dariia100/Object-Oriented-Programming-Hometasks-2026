using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Task2_2_Workers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            List<Team> TeamList = new List<Team>();
            bool CreatingTeams = true;
            while (CreatingTeams)
            {
                Console.WriteLine("Введіть назву команди: ");
                string TeamName = Console.ReadLine();

                Team currTeam = new Team(TeamName);

                bool AddingWorkers = true;
                while (AddingWorkers)
                {
                    Console.WriteLine("Введіть ім'я робітника: ");
                    string WorkerName = Console.ReadLine();
                    Worker addedWorker = null;

                    while (true)
                    {
                        Console.WriteLine("Оберіть посаду робітника. 0 - Розробник, 1 - Менеджер: ");
                        string WorkerPosition = Console.ReadLine();
                        
                        if (WorkerPosition == "0")
                        {
                            addedWorker = new Developer(WorkerName);
                            break;
                        }
                        else if (WorkerPosition == "1")
                        {
                            addedWorker = new Manager(WorkerName);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Помилка. Введіть 0 або 1.");
                        } 
                    }
                    addedWorker.FillWorkDay();
                    currTeam.AddWorker(addedWorker);

                    while (true) { 
                        Console.WriteLine($"Додати ще робітника в команду {TeamName}? 0 - ні, 1 - так: ");
                        string ContinueAddingWorkers = Console.ReadLine();
                        if (ContinueAddingWorkers == "0")
                        {
                            AddingWorkers = false;
                            break;
                        }
                        else if (ContinueAddingWorkers == "1")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Помилка. Введіть 0 або 1.");
                        }
                    }
                }
                TeamList.Add(currTeam);
                while (true)
                {
                    Console.WriteLine("Додати ще команду? 0 - ні, 1 - так: ");
                    string ContinueAddingTeams = Console.ReadLine();
                    if (ContinueAddingTeams == "0")
                    {
                        CreatingTeams = false;
                        break;
                    }
                    else if (ContinueAddingTeams == "1")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Помилка. Введіть 0 або 1.");
                    }
                }
            }
            Console.Clear();
            
            for (int i = 0; i < TeamList.Count; i++)
            {
                Console.WriteLine("Інформація про команди: ");
                TeamList[i].TeamInfo();
                TeamList[i].TeamInfoDetailed();
            }
        }

        abstract class Worker
        {
            public string Name { get; protected set; }
            public string Position { get; protected set; }
            public string WorkDay { get; protected set; }

            public Worker(string name)
            {
                Name = name;
                Position = "";
                WorkDay = "";
            }

            public void Call() 
            {
                WorkDay += "Зроблено дзвінок. ";
            }
            public void WriteCode() 
            {
                WorkDay += "Працівник писав код. ";
            }
            public void Relax() 
            {
                WorkDay += "Працівник відпочив. ";
            }
            public abstract void FillWorkDay();
        }

        class Developer : Worker
        {
            public Developer(string name) : base(name)
            {
                Position = "Розробник";
            }

            public override void FillWorkDay()
            {
                Call();
                WriteCode();
                Relax();
            }
        }

        class Manager : Worker
        {
            public Manager(string name) : base(name)
            {
                Position = "Менеджер";
            }
            private Random random = new Random();
            public override void FillWorkDay()
            {
                int randomFromOneToTen = random.Next(1, 11);
                for (int i = 0; i < randomFromOneToTen; i++)
                {
                    Call();
                }
                Relax();
                int randomFromOneToFive = random.Next(1, 6);
                for (int i = 0; i < randomFromOneToFive; i++)
                {
                    Call();
                }
            }
        }

        class Team
        {
            public string TeamName { get; protected set; }
            private List<Worker> workers;
            public Team(string name)
            {
                TeamName = name;
                workers = new List<Worker>();
            }

            public void AddWorker(Worker worker)
            {
                workers.Add(worker);
            }

            public void TeamInfo()
            {
                Console.WriteLine($"Назва команди: {TeamName}");
                for (int i = 0; i < workers.Count; i++)
                {
                    Console.WriteLine(workers[i].Name);
                }
            }
            public void TeamInfoDetailed()
            {
                Console.WriteLine($"Назва команди: {TeamName}");
                for (int i = 0; i < workers.Count; i++)
                {
                    Console.WriteLine($"{workers[i].Name} - {workers[i].Position} - {workers[i].WorkDay}");
                }
            }
        }
    }
}
