using System.Diagnostics.Tracing;
using System.Net.Security;
using System.Text;

public class Program 
{
    public static void Main() 
    {
        var station = new WeatherStation();
        var phoneDisplay1 = new PhoneDisplay(station);
        var phoneDisplay2 = new PhoneDisplay(station);

        var windowDisplay1 = new WindowDisplay(station);
        var windowDisplay2 = new WindowDisplay(station);

        station.Add(phoneDisplay1);
        station.Add(phoneDisplay2);
        station.Add(windowDisplay1);
        station.Add(windowDisplay2);

        station.SetTemperature(10);
    }
}





/// <summary>
/// The Observer pattern solves the problem of alot of entities listening for updates from a single entity
/// meanning that each one of them has to keep asking if there is an update or not indefenitly, this is 
/// called polling architecture, meaning that the subscribers keep polling for whether or not there are
/// any updates, the Observer pattern instead utilizes the Pushing Architecture, it notifies all of the 
/// publisher's subscribers of any update and pushes the notification to them
/// </summary>




/// <summary>
/// This is The Observer, it acts like a subscriber where it is updates and the new data is displayed
/// whenever the publisher broadcasts an update
/// </summary>

public interface IObserver 
{
    void Update();
}
public class PhoneDisplay: IObserver
{
    private int tempToDisplay;
    public WeatherStation _publisherRef;
    public PhoneDisplay(WeatherStation publisherRef)
    {
        this._publisherRef= publisherRef;
    }
    public void Update()
    {
        tempToDisplay = this._publisherRef.GetTemperature();
        Display(tempToDisplay);
    }
    public void Display(int dataToDisplay)
    {
        Console.WriteLine($"This is The Phone Display and The Temperature IS ****{tempToDisplay}**** !!!!!!!!!");
    }
}

public class WindowDisplay : IObserver
{
    private WeatherStation _publisherRef;
    private int tempToDisplay;
    public WindowDisplay(WeatherStation publisherRef)
    {
        this._publisherRef = publisherRef;
    }
    public void Update() 
    {
        tempToDisplay = this._publisherRef.GetTemperature();
        Display(tempToDisplay);
    }
    public void Display(int dataToDisplay) 
    {
        Console.WriteLine($"This is The Window Display and The Temperature IS ****{ tempToDisplay }**** !!!!!!!!!");
    }
}

/// <summary>
/// This is The Observable entity, its like a publisher that pushes the notification to all
/// its subscribers as soon as there exists a new update, we utilize association with a 1 to many 
/// relation between publisher and subscribers, meaning that each publisher can have many subscribers
/// </summary>
public interface IObservable
{
    void Notify();
    void Add(IObserver subscriber);
    void Remove(IObserver Subscriber);
}
public class WeatherStation : IObservable
{
    private int _temperature;
    private List<IObserver> _subscriberList = new List<IObserver>();
    public void Add(IObserver subscriber)
    {
        _subscriberList.Add(subscriber);
    }
    public void Remove(IObserver Subscriber)
    {
        if (_subscriberList.Contains(Subscriber))
        {
            _subscriberList.Remove(Subscriber);
        }
        else
        {
            return;
        }
    }
    public void Notify()
    {
        foreach (var sub in _subscriberList)
        {
            sub.Update();
        }
    }
    public int GetTemperature() 
    {
        return _temperature;
    }
    public void SetTemperature(int temperature) {
        _temperature = temperature;
        Notify();
    }

}
