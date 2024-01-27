# 💱 Singleton Design Pattern in C#

## 📖 Overview

- Pattern: The Singleton design pattern is a software design pattern that restricts the instantiation of a class to one "single" instance. This is useful when exactly one object is needed to coordinate actions across the system.
- Goal: Ensure that a class has only one instance, and provide a global point of access to it.

## 🧩 Implementation in Code

CurrencyConvertor Class with Double-Check Locking:
Incorporates double-check locking to ensure thread safety.

public class CurrencyConvertor
{
private static CurrencyConvertor \_instance;
// a refrence type to lock on (can be a string as well)
private static readonly object \_lock = new object();

    // Private constructor to prevent instantiation outside of the class
    private CurrencyConvertor() { }

    // Public static property for accessing the singleton instance
    public static CurrencyConvertor Instance
    {
        get
        {
            // First check to avoid locking each time
            if (_instance == null)
            {
                lock (_lock)
                {
                    // Double-check to ensure only one instance is created
                    if (_instance == null)
                    {
                        _instance = new CurrencyConvertor();
                    }
                }
            }
            return _instance;
        }
    }

    // Method to demonstrate the class's functionality
    public decimal Convert(string baseCurr, string targetCurr, decimal amount)
    {
        // Conversion logic
    }

}

The double-check locking pattern is used to ensure that the singleton remains thread-safe while avoiding the need to lock each time the instance is requested. This pattern significantly improves performance and ensures that only one instance of the singleton is created, even in multi-threaded environments.
Usage Example:

## 🚀 Applying the Singleton Pattern

- Thread Safety: The implementation uses double-check locking to ensure that the singleton is thread-safe, which is crucial for applications running in multi-threaded environments.
- Performance: Double-check locking reduces the overhead of acquiring locks, making the singleton more efficient to use.

## 📚 Conclusion

The Singleton pattern, particularly with the double-check locking mechanism, is vital for ensuring that a class has only one instance across the entire application, while also being thread-safe and efficient. The CurrencyConvertor class demonstrates this pattern, highlighting its importance in software design.
