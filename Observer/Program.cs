using System;
using System.Collections.Generic;

public interface IObserver
{
    void Update(string message);
}

public class WeatherStation
{
    private List<IObserver> observers = new List<IObserver>();
    private int temperature;

    public int Temperature
    {
        get { return temperature; }
        set
        {
            temperature = value;
            NotifyObservers();
        }
    }

    public void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        observers.Remove(observer);
    }

    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.Update($"Temperature has been updated: {Temperature}°C");
        }
    }
}

public class Display : IObserver
{
    public void Update(string message)
    {
        Console.WriteLine("Display: " + message);
    }
}

public class Logger : IObserver
{
    public void Update(string message)
    {
        Console.WriteLine("Logger: " + message);
    }
}

class Program
{
    static void Main(string[] args)
    {
        WeatherStation weatherStation = new WeatherStation();

        Display display = new Display();
        Logger logger = new Logger();

        weatherStation.Attach(display);
        weatherStation.Attach(logger);

        weatherStation.Temperature = 25;

        weatherStation.Detach(logger);
        weatherStation.Temperature = 30;
    }
}