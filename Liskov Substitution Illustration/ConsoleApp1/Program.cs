public class program {
    public static int Area(Rectangle r) 
    {
        return r.Width * r.Height;
    }
    public static void Main() {
        Rectangle rc = new Rectangle(10,2);
        int area = Area(rc);  
        Console.WriteLine(area);
        /// <summary>
        /// This will work fine, and the are will print out to be 16 as we expect
        /// but when try to make a refrence of the parent (recatangle) point to an
        /// object of the child (square), which is valid, the problem becomes aparent
        /// </summary>
        Square sq = new Square();
        sq.Width = 4;
        Console.WriteLine(Area(sq));

        /// <summary>
        /// the problem becomes that the parent refrence uses its own width setter
        /// and does't set the height, so the height remains zero and the area becomes zero
        /// </summary>

        Rectangle sq2 = new Square();
        sq2.Width = 4;
        Console.WriteLine(Area(sq2));
    }
}

public class Rectangle
{
    public virtual int Height { get; set; }
    public virtual int Width { get; set; }

    public Rectangle() { }
    public Rectangle(int height, int width) 
    {
        Height= height;
        Width= width;
    }
    public override string ToString()
    {
        return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}"; 
    }
}
/// <summary>
/// we try to implement the Square class, which gemotrically speaking is indeed a rectangle
/// but we tun into the problem that a square width and height must be equal, so now we have
/// to make a new setter for both the width and the height, and this here violates the Liskov's Substitution principle
/// which states that we can substitute any children with their parent and no change in the behaviour should occur,
/// because when we use a rectangle (parent) refrence to a square (child) object and try to set the width or height, the other edge isn't
/// set and remains with its default value (0)
/// </summary>

/// <summary>
/// the fix is quite easy
/// we just need to make the behvaiours that may change become virtual and override them, so that no change happens whatso ever
/// or make the parent as an interface, and leave the implementation of its bvehaviours to the classes that implement it
/// <summary/>

public class Square : Rectangle 
{
    /// <summary>
    /// we make a new setter for both the width and the height, so that when the
    /// height or the width change with a given value, we set the width and the height to that
    /// same value, this is a terrible idea because ....
    /// </summary>
    public /*new*/ override int Width 
    {
        set { base.Width = base.Height = value; }
    }
    public /*new*/ override int Height 
    {
        set { base.Height = base.Width = value; }
    }
}
