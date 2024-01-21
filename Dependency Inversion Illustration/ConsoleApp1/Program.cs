
using System.Globalization;

public class Program 
{
    public static void Main(string[] args) {
        
        var parent = new Person("Johnny");
        var child1 = new Person("HAAAMADAAA");
        var child2 = new Person("HAMAAAAAAAADAAA2");
        RelationShips relations = new RelationShips();
        relations.AddParentRelationShip(parent,child1);
        relations.AddParentRelationShip(parent, child2);

        var research = new Research(relations,"Johnny");
    }
}

public enum RelationShip 
{
    Parent,Child,Sibling
}
public class Person 
{
    public string Name;
    public Person(string _name) 
    {
        this.Name = _name;
    }
}


public class RelationShips:IDataStoreBrowser<Person>
{
    private List<(Person, RelationShip, Person)> relations = new List<(Person, RelationShip, Person)>();
    public void AddParentRelationShip(Person parent, Person child) 
    {
        // we are storing data in a denormalized way
        // which is now a more common approach in order 
        // access data faster
        relations.Add((parent, RelationShip.Parent, child));
        relations.Add((child,RelationShip.Child,parent));
    }

    /// <summary>
    /// what we are doing here is making a public getter for the relations
    /// and then we can then in another class, maybe a research class
    /// have it depend on another low level || concrete class
    /// </summary>
    //public List<(Person, RelationShip, Person)> GetRelations() 
    //{
    //   return relations;
    //}

    /// <summary>
    /// Now we implement the interface method and then use it in the high level class
    /// without exposing the private fields or depending on concrete objects
    /// </summary>
    public IEnumerable<Person> FindAllChildrenOf(string name)
    {
        var children = relations.Where(x=>x.Item1.Name == name && x.Item2 == RelationShip.Parent).Select(z=> new Person(z.Item3.Name)).ToList();
        return children;
    }
}
public class Research 
{
    /// <summary>
    /// here is the the high level class depending on a concrete low level class
    /// which is the relations, and this violates the Dependency inversion principle 
    /// as it not only depends on a lower class, but also it forces us to adhere to expose
    /// the list inside of the low level class and also if we ever decide to change our data store
    /// to be a database or something, the whole logic has to be changed
    /// </summary>
    /// 
    //public Research(RelationShips relations) 
    //{
    //   var allRelations =  relations.GetRelations();
    //    var filteredRelations = allRelations.Where(x => x.Item1.Name == "Johnny" && x.Item2 == RelationShip.Parent);
    //    foreach (var r in filteredRelations) 
    //    {
    //        Console.WriteLine($"john has child {r.Item3.Name}");
    //    }
    //}

    /// <summary>
    /// Now any class that can implement the IDataStoreBrowser
    /// interface can be passed to the research constructor and then 
    /// we can use the implementation of the class passed in to access
    /// its datastore and get all the children of the parent name provided
    /// </summary>

    public Research(IDataStoreBrowser<Person> personBrowser ,string parentName) 
    {
        foreach (var item in personBrowser.FindAllChildrenOf(parentName))
        {
            Console.WriteLine($"{parentName} has {item.Name} as a child");
        }
    }
}

/// <summary>
/// what we can do which is better is to make a browsing interface
/// with a FindAllChildren method that the low level class implements
/// and then in the Research class we can then use that interface
/// so that any class that implements the browsing interface
/// we can then access its data store without actually depending on it
/// </summary>
public interface IDataStoreBrowser<T>
{
    IEnumerable<T> FindAllChildrenOf(string name);
}