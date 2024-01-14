# 🛠️ Open/Closed Principle in C#

## 📖 Overview

Principle: The Open/Closed Principle (OCP) is a fundamental concept in SOLID principles, which dictates that software entities should be open for extension but closed for modification.
Goal: Enable extension of functionality without altering the existing code, thus enhancing maintainability and scalability.

## 🧩 Implementation in Code

### Key Code Snippets

- ISpecification and IFilter Interfaces:
  Define a generic interface for specifications and a filtering interface.
  public interface ISpecification<T>
  {
  bool IsSatisfied(T item);
  }

public interface IFilter<T>
{
IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
}

- BetterFilter Class:
  Implements the IFilter interface, allowing filtering based on any given specification.
  public class BetterFilter : IFilter<Product>
  {
  public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
  {
  foreach (var item in items)
  {
  if (spec.IsSatisfied(item))
  {
  yield return item;
  }
  }
  }
  }
- ColorSpecification Class:
  A specific implementation of ISpecification to filter products by color.
  public class ColorSpecification : ISpecification<Product>
  {
  private Color color;

      public ColorSpecification(Color color)
      {
          this.color = color;
      }

      public bool IsSatisfied(Product item)
       {
          return item.Color == color;
      }

  }

- AndSpecification Class:
  Combines two specifications, enforcing both.
  public class AndSpecification<T> : ISpecification<T>
  {
  private ISpecification<T> first, second;

      public AndSpecification(ISpecification<T> first, ISpecification<T> second)
      {
          this.first = first;
          this.second = second;
      }

      public bool IsSatisfied(T item)
      {
          return first.IsSatisfied(item) && second.IsSatisfied(item);
      }

  }

## 🚀 Applying OCP

Before OCP: ProductFilter required new methods for different filter criteria.
After OCP: With BetterFilter and specifications, new filters can be added without altering existing code.

## 📚 Conclusion

By using the OCP with interfaces and the specification pattern, the filtering system becomes flexible and extensible. This approach allows for easy addition of new filtering criteria without needing to modify the existing filter class, adhering to the Open/Closed Principle.
