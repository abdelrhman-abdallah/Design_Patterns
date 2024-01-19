# 📐 Liskov Substitution Principle (LSP) in C#

## 📖 Overview

Principle: LSP is a core principle in SOLID, emphasizing that objects of a superclass should be replaceable with objects of its subclasses without altering the program's functionality.
Goal: Ensure subclasses enhance, not alter, the behavior and contracts of their base classes.

## 🧩 Implementation in Code

Rectangle Class:
public class Rectangle {
public /_virtual_/ int Width { get; set; } // without the virtual keyword, and overriding the behaviours in the children classes, a change in  
public /_virtual_/ int Height { get; set; } // behaviour will take place when susbtituting the child refrences with the parent which violates LSP

public Rectangle() { }

public Rectangle(int width, int height) {
Width = width;
Height = height;
}
}

Square Class:
Derived class potentially violating LSP.
public class Square : Rectangle {
public /_override_/ int Width {
set { base.Width = base.Height = value; }
}

public /_override_/ int Height {
set { base.Height = base.Width = value; }
}
}

### LSP Violation and Keywords Explanation:

- LSP Violation: Changing Width or Height in Square changes both, leading to incorrect behavior when treated as a Rectangle.
- Keywords:
  - virtual in Rectangle allows methods/properties to be overridden.
  - override in Square changes the behavior of these properties.
  - Using new instead of override would hide the base class member, not override it, leading to different behavior based on the type of reference (base or derived) used to access the object.

### Area Calculation and Main Method:

Illustrates unexpected behavior due to LSP violation.

public static int Area(Rectangle r) {
return r.Width \* r.Height;
}
public static void Main() {
Rectangle rect = new Rectangle(10, 2);
Square sq = new Square();
sq.Width = 4;
Rectangle sqAsRect = new Square();
sqAsRect.Width = 4;

    Console.WriteLine(Area(rect));     // Expected area for Rectangle
    Console.WriteLine(Area(sq));       // Expected area for Square
    Console.WriteLine(Area(sqAsRect)); // Demonstrates LSP violation when no override to the behaviour takes place

}

## 🚀 Applying LSP

Issue: Square subclass, when treated as a Rectangle, does not maintain the rectangle's behavior, violating LSP.
Resolution: Design subclasses to fully adhere to their base class's contract, enhancing rather than altering functionality.
meaning, that we can design for an abstract class or for an interface, and leave the implementation for those who implement it.

## 📚 Conclusion

The example illustrates a classic violation of LSP in object-oriented design. Understanding and applying LSP correctly, along with appropriate use of virtual, override, and new keywords, is crucial for creating robust, maintainable, and substitutable class hierarchies.
