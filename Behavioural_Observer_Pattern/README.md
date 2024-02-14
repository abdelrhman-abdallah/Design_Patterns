# 🌦 Observer Design Pattern in C#

## 📖 Overview

- Pattern: The Observer design pattern is a behavioral design pattern that defines a one-to-many dependency between objects so that when one object changes state, all its dependents are notified and updated automatically.
- Goal: Eliminate the need for observers to poll the subject for changes, instead using a push-based notification mechanism.

## 🧩 Implementation in Code

WeatherStation (Subject) and Observer Interface:

The WeatherStation acts as the subject, notifying observers about temperature changes.
Observers implement a common interface to receive updates.

public class WeatherStation {
private List<IObserver> observers = new List<IObserver>();
private int temperature;

    public void Add(IObserver observer) {
        observers.Add(observer);
    }

    public void Remove(IObserver observer) {
        observers.Remove(observer);
    }

    public void Notify() {
        foreach (var observer in observers) {
            observer.Update(temperature);
        }
    }

    public void SetTemperature(int temp) {
        temperature = temp;
        Notify();
    }

}

public interface IObserver {
void Update(int temperature);
}
Display Classes (Observers):

PhoneDisplay and WindowDisplay act as observers, updating their display based on the subject's state.

public class PhoneDisplay : IObserver {
private WeatherStation station;

    public PhoneDisplay(WeatherStation station) {
        this.station = station;
    }

    public void Update(int temperature) {
        // Update the display with the new temperature
    }

}

public class WindowDisplay : IObserver {
// Similar to PhoneDisplay but for a window-based display
}

## 🚀 Applying the Observer Pattern

- Decoupling: The pattern decouples the WeatherStation from its displays, allowing for independent addition, removal, or modification of observers.
- Push-Based Notification: The WeatherStation pushes updates to all registered displays, eliminating the need for polling.

## 📚 Conclusion

The Observer design pattern, as illustrated with the WeatherStation and display classes, showcases an efficient way to manage state changes and notifications in a system. This pattern is especially useful in scenarios where multiple entities need to stay updated with changes in another entity, promoting a clean separation of concerns and reducing dependencies.
