using System.Diagnostics.Tracing;
using System.Net.Security;
using System.Text;

public class Program 
{
    public static void Main() 
    {
        var woodDuck = new WoodenDuck();
        woodDuck.Display("__\r\n( o>\r\n///\\ \r\n\\V_/_");
        woodDuck.performQuack();
        woodDuck.performFly();

        var cityDuck = new CityDuck();
        cityDuck.Display("__\r\n___( o)>\r\n\\ <_. )\r\n `---' ");
        cityDuck.performQuack();
        cityDuck.performFly();

        var rubberDuck = new RuberDuck();
        rubberDuck.Display(",~&.\r\n(\\_   (  e )>\r\n ) `~~'   (\r\n(   `-._)  )\r\n `-._____,");
        rubberDuck.performFly();
        rubberDuck.performQuack();

        // we can even change their behaviours
        Console.WriteLine("*********************************************");
        cityDuck.performFly();
        cityDuck.SetFlyStrategy(new NonFlyingBehaviour());
        cityDuck.performFly();
        Console.WriteLine("*********************************************");

        Console.WriteLine("*********************************************");
        woodDuck.performQuack();
        woodDuck.SetQuackStrategy(new DefaultQuackingBehaviour());
        woodDuck.performQuack();
        Console.WriteLine("*********************************************");

    }
}
/// <summary>
/// This is the super class on which we will demonstrate the Strategy Pattern
/// we have a city, wild, wooden and rubber ducks, and they all inherit from the 
/// super class Duck, Each of them can override the Display method and makes 
/// their own Display, but the problem here becomes when both wooden and rubber ducks
/// get introduced, rubber duck  quacks but doesn't fly and wooden duck neither flies
/// nor quacks, and so here the strategy pattern shines, instead of making each class
/// override each varying behaviour, we seperate what varies into interfaces, so one
/// for quacking and one for flying, and then a group of alogorithms that implement the
/// quacking and flying interfaces with different strategies.
/// and now the Duck class has a quacking strategy and a flying strategy
/// </summary>
public class Duck 
{
    public virtual void Display(string characterArt) 
    {
        Console.WriteLine(characterArt);
    }

    // the problem with methods or behaviours being inherited, is that they
    // can vary between children, or maybe even two children can have the same behaviour 
    // but its different from the parent behaviour

    //public void Fly(string flyBehaviour) {Console.WriteLine(flyBehaviour);}
    //public void Quack(string quackBehaviour) { Console.WriteLine(quackBehaviour); }

    // so we instead make it a compostion, now any duck has a flying and quacking behaviour
    // instead of it and its children are quacking or flying, because this way we can define
    // multiple different quacking and flying strategies and interchange them without any complications

    public IQuackStrategy QuackStrategy { get; set; } = new DefaultQuackingBehaviour();
    public IFlyStrategy FlyStrategy { get; set; } = new DefaultFlyingBehaviour();

    public void performFly() { FlyStrategy.Fly(); }
    public void performQuack() { QuackStrategy.Quack(); }

    public void SetFlyStrategy(IFlyStrategy flyStrategy) {FlyStrategy = flyStrategy;}
    public void SetQuackStrategy(IQuackStrategy quackableStrat) { QuackStrategy = quackableStrat; }
}

public class WildDuck:Duck 
{
    public WildDuck() 
    {
        QuackStrategy = new DefaultQuackingBehaviour();
        FlyStrategy= new DefaultFlyingBehaviour();
    }
    public override void Display(string characterArt)
    {
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("This is a Wild Duck Display");
        Console.WriteLine(characterArt);
        Console.WriteLine("This is a Wild Duck Display");
        Console.WriteLine("-------------------------------------");
    }
}
public class CityDuck:Duck 
{
    public CityDuck()
    {
        QuackStrategy = new DefaultQuackingBehaviour();
        FlyStrategy = new DefaultFlyingBehaviour();
    }
    public override void Display(string characterArt)
    {
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("This is a City Duck Display");
        Console.WriteLine(characterArt);
        Console.WriteLine("This is a City Duck Display");
        Console.WriteLine("-------------------------------------");
    }
}
public class WoodenDuck:Duck 
{
    public WoodenDuck()
    {
        QuackStrategy = new NonQuackingBehaviour();
        FlyStrategy = new NonFlyingBehaviour();
    }
    public override void Display(string characterArt)
    {
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("This is a Wooden Duck Display");
        Console.WriteLine(characterArt);
        Console.WriteLine("This is a Wooden Duck Display");
        Console.WriteLine("-------------------------------------");
    }
}
public class RuberDuck :Duck 
{
    public RuberDuck()
    {
        QuackStrategy = new DefaultQuackingBehaviour();
        FlyStrategy = new NonFlyingBehaviour();
    }
    public override void Display(string characterArt)
    {
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("This is a Rubber Duck Display");
        Console.WriteLine(characterArt);
        Console.WriteLine("This is a Rubber Duck Display");
        Console.WriteLine("-------------------------------------");
    }
}



/// <summary>
/// This is the quacking strategy and ints family of algorithms, we can add any new behaviour simply
/// but implementing the interface and giving our desired implementation to the method
/// </summary>

public interface IQuackStrategy 
{
    void Quack();
}
public class DefaultQuackingBehaviour: IQuackStrategy
{
    public void Quack() { Console.WriteLine("THIS DUCK QUACKSSSSSS"); }
}

public class NonQuackingBehaviour : IQuackStrategy
{
    public void Quack() { Console.WriteLine("THIS DUCK DOESN't QUACK AT ALLLLLL"); }
}

/// <summary>
/// this is the fly strategy and its family of algorithms, we can add any new behaviour simply
/// but implementing the interface and giving our desired implementation to the method
/// </summary>
public interface IFlyStrategy
{
    void Fly();
}
public class DefaultFlyingBehaviour : IFlyStrategy
{
    public void Fly()
    {
        Console.WriteLine("THIS DUCK FLIESSSSSSSSSS");
    }
}

public class NonFlyingBehaviour : IFlyStrategy 
{
    public void Fly() 
    {
        Console.WriteLine("THIS DUCK DOESN'T FLY AL ALLLLLLL");
    }
}