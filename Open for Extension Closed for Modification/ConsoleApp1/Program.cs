
public enum Color { 
   RED,Green, Blue, GreenYellow, BlueGreenYellow
}
public enum Size { 
    Small, Medium, Large,
}

public class Product {
    public string Name;
    public Color Color;
    public Size Size;
    public Product(string name,Color color,Size size) {
        if (name == null)
        {
            throw  new ArgumentNullException(name);
        }
        Name = name;
        Color = color;
        Size = size;
    }
} 

public class ProductFilder{

    ///summary  
    ///every time i want a different filtering criteria
    ///i will have to edit this class itself and add a new filtering method
    ///which violates the Open for extension closed for modification principle
    ///what we can do instead is implement interfaces and inherit in order to avoid that
    ///we can also implement a design pattern called the specification pattern (wasn't introduced by the gang of 4 pattern)
    ///summary

    public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size filterSize)
    {
        foreach (Product product in products)
        {
            if (product.Size == filterSize)
            {
                yield return product;
            }
        }
    }
    public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color filterColor)
    {
        foreach (Product product in products)
        {
            if (product.Color == filterColor)
            {
                yield return product;
            }
        }
    }
    public IEnumerable<Product> FilterByColorAndSize(IEnumerable<Product> products, Color filterColor, Size filterSize)
    {
        foreach (Product product in products)
        {
            if (product.Color == filterColor && product.Size == filterSize)
            {
                yield return product;
            }
        }
    }
}

/// <summary>
/// This is the Specification pattern, we create a specification interface that we then implement and specifiy our satisfaction condition
/// and then when we want to filter by anything, we just make a class that implements the filter interface.
/// </summary>
/// <typeparam name="T">in this case it is of type Product, but we can use it on any other type we want, (Cars,Employees,.....)</typeparam>

public interface ISpecification<T>
{
    bool isSatisfied(T t);
}
/// <summary>
/// This is the filter interface, when implements, it takes a List of items of any type, and any class that implements the Ispecification interface 
/// and then return a list of the filter items.
/// </summary>
/// <typeparam name="T">in this case it is of type Product, but we can use it on any other type we want, (Cars,Employees,.....)</typeparam>

public interface IFilter<T>
{
    IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
}

public class ColorSpecification : ISpecification<Product>
{
    private Color _c;
    public ColorSpecification(Color c) 
    {
        this._c= c;
    }
    public bool isSatisfied(Product p)
    {
        return p.Color == _c;
    }
}
public class SizeSpecification : ISpecification<Product> 
{
    private Size _size;
    public SizeSpecification(Size size) 
    {
        _size = size;
    }
    public bool isSatisfied(Product t)
    {
        return t.Size == _size;
    }
}

/// <summary>
/// This is a combinator, which can combine mutiple specifications togther
/// </summary>
/// <typeparam name="T">in this case it is of type Product, but we can use it on any other type we want, (Cars,Employees,.....)</typeparam>
/// 
public class AndSpecification<T> : ISpecification<T>
{
    private ISpecification<T> _firstCriteria;
    private ISpecification<T> _secondCriteria;
    public AndSpecification(ISpecification<T> first, ISpecification<T> second) 
    {
       _firstCriteria = first ?? throw new ArgumentNullException(paramName:nameof(first));
       _secondCriteria = second ?? throw new ArgumentNullException(paramName: nameof(second));
    }
    public bool isSatisfied(T t)
    {
        return _firstCriteria.isSatisfied(t) && _secondCriteria.isSatisfied(t);
    }
}

public class BetterFiltering : IFilter<Product>
{
    public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
    {
        foreach (var item in items)
        {
            if (spec.isSatisfied(item))
            {
                yield return item;
            }
        }
    }
}
public class Program 
{
    public static void Main() 
    {
        var apple = new Product("Apple", Color.RED, Size.Small);
        var peach = new Product("Peach", Color.RED, Size.Large);
        var house = new Product("House", Color.Green, Size.Medium);
        var tree = new Product("Tree", Color.Green, Size.Medium);
        var car = new Product("Car", Color.BlueGreenYellow, Size.Small);

        Product[] products = { apple, peach, house, car, tree };

        // old filtering way that violates Open for extension clsoed for modification principle

        var oldFilter = new ProductFilder();
        var filteredBySize = oldFilter.FilterBySize(products,Size.Medium);
        Console.WriteLine("Old Filter By Size");
        foreach (var item in filteredBySize)
        {
            Console.WriteLine($"{item.Name}.........{item.Color}..........{item.Size}");
        }
        var filteredByColor = oldFilter.FilterByColor(products,Color.RED);
        Console.WriteLine("Old Filter By Color");
        foreach (var item in filteredByColor)
        {
            Console.WriteLine($"{item.Name}.........{item.Color}..........{item.Size}");
        }
        var filteredByBoth = oldFilter.FilterByColorAndSize(products,Color.RED,Size.Large);
        Console.WriteLine("Old Filter By Size and Color");
        foreach (var item in filteredByBoth)
        {
            Console.WriteLine($"{item.Name}.........{item.Color}..........{item.Size}");
        }
        // new filtering way that doesn't violates Open for extension clsoed for modification principle

        var betterFilter = new BetterFiltering();
        var newFilterByColor = betterFilter.Filter(products, new ColorSpecification(Color.Green));
        Console.WriteLine("New Filter By Color");
        foreach (var item in newFilterByColor)
        { 
            Console.WriteLine($"{item.Name}.........{item.Color}..........{item.Size}");
        }
        var newFilterBySize = betterFilter.Filter(products,new SizeSpecification(Size.Medium));
        Console.WriteLine("New Filter By Size");
        foreach (var item in newFilterBySize)
        {
            Console.WriteLine($"{item.Name}.........{item.Color}..........{item.Size}");
        }
        var newFilterByBoth = betterFilter.Filter(products, new AndSpecification<Product>(new SizeSpecification(Size.Large),new ColorSpecification(Color.RED)));
        Console.WriteLine("New Filter By Size and Color");
        foreach (var item in newFilterByBoth)
        {
            Console.WriteLine($"{item.Name}.........{item.Color}..........{item.Size}");
        }

    }
}