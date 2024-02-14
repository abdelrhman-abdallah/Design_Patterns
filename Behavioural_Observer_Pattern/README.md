# 🦆 Strategy Design Pattern in C#

## 📖 Overview

-Pattern: The Strategy design pattern is a behavioral design pattern that enables selecting an algorithm's runtime behavior among a family of algorithms, encapsulating each one, and making them interchangeable.
Goal: Enhance flexibility and reusability by decoupling algorithmic behavior from the context class.

### 🧩 Implementation in Code

Duck Classes and Behavior Interfaces:
Abstract behaviors and duck types demonstrating the Strategy pattern.

public interface IFlyBehavior {
void Fly();
}

public interface IQuackBehavior {
void Quack();
}

public class CityDuck : Duck {
public CityDuck() {
SetFlyStrategy(new FlyingWithWings());
SetQuackStrategy(new Quacking());
}
// Display method and other implementations
}

public class WoodenDuck : Duck {
// WoodenDuck specific implementations
}

public class RubberDuck : Duck {
// RubberDuck specific implementations
}

Strategy Implementation and Usage:
Implementing and dynamically changing duck behaviors.

var cityDuck = new CityDuck();
cityDuck.Display(); // Display city duck
cityDuck.performFly(); // Perform initial flying behavior
cityDuck.SetFlyStrategy(new NonFlyingBehaviour()); // Change flying behavior
cityDuck.performFly(); // Perform new flying behavior

## 🚀 Applying the Strategy Pattern

- Flexibility: The pattern allows for the dynamic change of behavior (e.g., flying, quacking) of duck instances at runtime.
- Decoupling: Behaviors are encapsulated in separate strategy classes, promoting loose coupling between the behavior and the context (duck classes).

## 🚀 Design Principles in Action

- Favoring Composition Over Inheritance:
  Duck behaviors are composed using strategy objects rather than being inherited, allowing for more flexible and dynamic behavior changes.
- Separating What Varies:
  By encapsulating the varying parts (flying and quacking behaviors) into separate classes, the design isolates the aspects of the application that change from those that stay the same.
- Coding to an Interface:
  Ducks interact with behavior strategies through interfaces (IFlyBehavior, IQuackBehavior), not concrete implementations. This approach reduces dependency on specific behavior classes and enhances flexibility.

## 📚 Conclusion

The Strategy design pattern, as illustrated with duck behaviors, showcases the power of runtime behavior interchangeability and algorithmic decoupling. This approach leads to more maintainable and flexible code, allowing easy extension and modification of behaviors without altering the context classes.
