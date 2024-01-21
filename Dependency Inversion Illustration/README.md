# 🔀 Dependency Inversion Principle (DIP) in C#

## 📖 Overview

Principle: DIP, a fundamental principle in SOLID, states that high-level modules should not depend on low-level modules. Both should depend on abstractions. Also, abstractions should not depend on details, but details should depend on abstractions.
Goal: Reduce the coupling between modules to increase flexibility and facilitate code maintainability.

## 🧩 Implementation in Code

- Person Class:

  Represents individuals in relationships.
  public class Person
  {
  public string Name;

        public Person(string name)
        {
            Name = name;
        }

  }

- RelationShips Class :

  Manages relationships and implements IDataStoreBrowser.
  Demonstrates dependency inversion by depending on an abstraction.
  public class RelationShips : IDataStoreBrowser<Person>
  {
  private List<(Person, RelationShip, Person)> relations = new List<(Person, RelationShip, Person)>();

        public void AddParentRelationShip(Person parent, Person child)
        {
            // Method implementation
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
        // Other methods and implementation

  }

- Interface(High Level Module || Absract Module):

  public interface IDataStoreBrowser<T>
  {
  // Interface methods
  }

- Research Class:

  Uses RelationShips through IDataStoreBrowser interface.
  Illustrates dependency on abstraction rather than concrete implementation.
  public class Research
  {
  public Research(IDataStoreBrowser<Person> relationships, string personName)
  {
  // Use relationships to perform research
  }
  }

## 🚀 Applying DIP

Before DIP: Direct dependency on concrete classes leading to tight coupling.
After DIP: Research depends on an interface (IDataStoreBrowser), not a concrete class, promoting loose coupling and flexibility.

## 📚 Conclusion

Implementing DIP enhances the design by reducing direct dependencies on concrete classes, promoting a more modular, maintainable, and adaptable codebase.
