namespace App4_HM7
{
    class MainApp
    {
        static void Main()
        {
            ChristmasTree ct = new ChristmasTree();

            OrnamentsDecorator d1 = new OrnamentsDecorator();
            LightsDecorator d2 = new LightsDecorator();

            d1.SetComponent(ct);
            d2.SetComponent(d1);
            d2.Operation();

            Console.Read();
        }
    }

    abstract class Component
    {
        public abstract void Operation();
    }
    class ChristmasTree : Component
    {
        public override void Operation()
        {
            Console.WriteLine("Christamas tree:");
        }
    }

    abstract class Decorator : Component
    {
        protected Component component;
        public void SetComponent(Component component)
        {
            this.component = component;
        }
        public override void Operation()
        {
            if (component != null)
            {
                component.Operation();
            }
        }
    }
    class OrnamentsDecorator : Decorator
    {
        public override void Operation()
        {
            base.Operation();
            Console.WriteLine("Decorations: candies, ornaments, star");
        }
    }
    class LightsDecorator : Decorator
    {
        private string addedState;
        public override void Operation()
        {
            base.Operation();
            addedState = "Christmas lights are shining";
            Console.WriteLine(addedState);
        }
    }

}
