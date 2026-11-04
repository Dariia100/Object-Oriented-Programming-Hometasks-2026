namespace App1_HM7
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    namespace AbstractFactory
    {
        public class AbstractFactory
        {
            abstract class Car
            {
                public abstract void Info();
            } 
 
        class Ford : Car
            {
                public override void Info()
                {
                    Console.WriteLine("Ford");
                }
            }

            class Toyota : Car
            {
                public override void Info()
                {
                    Console.WriteLine("Toyota");
                }
            }

            /// + Toyota car
            class Mercedes : Car
            {
                public override void Info()
                {
                    Console.WriteLine("Mercedes");
                }
            }
            ///

            abstract class Engine
            {
                public virtual void GetPower()
                {
                }
            }

            class FordEngine : Engine
            {
                public override void GetPower()
                {
                    Console.WriteLine("Ford Engine 4.4");
                }
            }

            class ToyotaEngine : Engine
            {
                public override void GetPower()
                {
                    Console.WriteLine("Toyota Engine 3.2");
                }
            }

            /// + Mercedes Engine
            class MercedesEngine : Engine
            {
                public override void GetPower()
                {
                    Console.WriteLine("Mercedes Engine 4.2");
                }
            }
            ///

            interface ICarFactory
            {
                Car CreateCar();
                Engine CreateEngine();
            }


            class FordFactory : ICarFactory
            {
                Car ICarFactory.CreateCar()
                {
                    return new Ford();
                }

                Engine ICarFactory.CreateEngine()
                {
                    return new FordEngine();
                }
            }

            class ToyotaFactory : ICarFactory
            {
                Car ICarFactory.CreateCar()
                {
                    return new Toyota();
                }

                Engine ICarFactory.CreateEngine()
                {
                    return new ToyotaEngine();
                }
            }

            /// + Mercedes Factory
            class MercedesFactory : ICarFactory
            {
                Car ICarFactory.CreateCar()
                {
                    return new Mercedes();
                }

                Engine ICarFactory.CreateEngine()
                {
                    return new MercedesEngine();
                }
            }
            /// 


            static void Main(string[] args)
            {
                ICarFactory carFactory = new ToyotaFactory();
                Car myCar = carFactory.CreateCar();
                myCar.Info();
                Engine myEngine = carFactory.CreateEngine();
                myEngine.GetPower();

                carFactory = new FordFactory();
                myCar = carFactory.CreateCar();
                myCar.Info();
                myEngine = carFactory.CreateEngine();
                myEngine.GetPower();

                carFactory = new MercedesFactory();
                myCar = carFactory.CreateCar();
                myCar.Info();
                myEngine = carFactory.CreateEngine();
                myEngine.GetPower();

                Console.ReadKey();
            }
        }
    }
}
