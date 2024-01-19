# 🖨️ Interface Segregation Principle (ISP) in C#

## 📖 Overview

Principle: ISP, an essential principle in SOLID, advocates that clients should not be forced to depend on interfaces they do not use.
Goal: Promote the creation of narrowly scoped interfaces that are tailored to the needs of individual clients.

## 🧩 Implementation in Code

### Problematic Bulky Interface:

- Initially, a bulky interface that violates ISP.
  // Bulky interface example that includes too many responsibilities
  public interface IModernMachine {

  // Methods for printing, scanning, faxing
  // Violates ISP as it forces implementation of all methods

  void Print(Document d);
  void Scan(Document d);
  void Fax(Document d);
  }

### Segregated Interfaces (ISP Compliant):

Smaller interfaces focused on specific functionalities.

- // Focused interface for printing
  public interface IPrint {
  void Print(Document d); // Method to print a document
  }

- // Focused interface for scanning
  public interface IScan {
  void Scan(Document d); // Method to scan a document
  }

- // Focused interface for faxing
  public interface IFax {
  void Fax(Document d); // Method to fax a document
  }

- // Combined interface for modern machines
  public interface IModernMachineV2 : IPrint, IScan, IFax {
  // Additional methods op properties that doesn't vary between modern machines
  // Complies with ISP by segregating responsibilities
  }

### Comments on Compliance with ISP:

- The refactored interfaces demonstrate adherence to ISP by ensuring that classes implementing these interfaces are not forced to implement unnecessary methods.
- This segregation allows for more modular, maintainable, and scalable design.

## 🚀 Applying ISP

Before ISP: Implementing the IModernMachine interface required unnecessary method implementations.
After ISP: The introduction of IPrint, IScan, IFax allows classes to implement only the functionalities they require.

## 📚 Conclusion

Adhering to ISP enhances the modularity and maintainability of software designs. It ensures that classes have only the most relevant methods they need, promoting cleaner and more efficient codebases.
