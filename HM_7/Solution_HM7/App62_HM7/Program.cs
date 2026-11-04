namespace App62_HM7
{
    /// Facade
    
    class Glasses
    {
        public void Place()
        {
            Console.WriteLine("The glasses are put on the table");
        }
    }

    class Plates
    {
        public void Place()
        {
            Console.WriteLine("The plates are put on the table");
        }
    }

    class Cutlery
    {
        public void Place()
        {
            Console.WriteLine("The cutlery is put on the table");
        }
    }

    class TableFacade
    {
        private Glasses glasses;
        private Plates plates;
        private Cutlery cutlery;

        public TableFacade()
        {
            glasses = new Glasses();
            plates = new Plates();
            cutlery = new Cutlery();
        }

        public void SetTheTable()
        {
            glasses.Place();
            plates.Place();
            cutlery.Place();

            Console.WriteLine("The table is set.");
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            TableFacade table = new TableFacade();
            table.SetTheTable();
            Console.ReadKey();
        }
    }
}
