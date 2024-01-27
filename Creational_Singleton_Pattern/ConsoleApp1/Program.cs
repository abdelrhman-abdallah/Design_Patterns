using System.Diagnostics.Tracing;
using System.Net.Security;
using System.Text;

public class Program 
{
    public static void Main() 
    {
        string baseCurr = "USA";
        string targetCurr = "EGP";
        decimal amountToConv = 2123.5m;
        decimal exchangedAmount = CurrencyConvertor.Instance.Convert(baseCurr,targetCurr,amountToConv);
        Console.WriteLine($"The {amountToConv} in {baseCurr} = {exchangedAmount} in {targetCurr} ");

    }
}

public class CurrencyConvertor
{

    private IEnumerable<ExchangeRate> _exchangeRates;

    // Now we make a private static attribute and encapsualte it
    // with its getter, and this encapsulated member is what 
    // we will use the make our singelton valid, it checks
    // if the instance is null, then it is the first time this
    // method is called, we then make a new instialization
    // otherwise we return the same instance, we also lock the thread
    // so that in a multi threaded application we make our code and
    // signelton instance thread safe

    private static object threadLock = "aLockToMakeThreadLockOnItAndBeSafe";
    
    private static CurrencyConvertor _instance = null;
    public static CurrencyConvertor Instance
    {
        get
        {
            // Make a double check to make it safer and more secure
            if (_instance == null)
            {
                lock (threadLock)
                {
                    if (_instance == null)
                    {
                        return new CurrencyConvertor();
                    }
                }
            }
            return _instance;

        }
    }


// make the constructor private
// so that only the class can access the
// constructor and make a new instance ofthe class

private CurrencyConvertor() 
    {
        LoadExchangeRates();
    }
    private void LoadExchangeRates() 
    {
        Thread.Sleep(3000);
        _exchangeRates = new[]
        {
            new ExchangeRate ("USA","SAR", 3.7m),
            new ExchangeRate ("USA","EGP", 31.5m),
            new ExchangeRate ("SAR", "EGP", 8.76m),
        };

    }
    public decimal Convert(string baseCurr, string targetCurr,decimal amount) 
    {
        var exchangeRate = _exchangeRates.FirstOrDefault(r=>r.BaseCurrency == baseCurr && r.TargetCurrency == targetCurr);
        if (exchangeRate == null)
        {
            return 0;
        }
        return amount * exchangeRate.Rate;
    }
}

public class ExchangeRate 
{
    public ExchangeRate(string baseCurr,string targetCurr,decimal goingRate) 
    {
        BaseCurrency = baseCurr;
        TargetCurrency = targetCurr;
        Rate = goingRate;
    }
    public string BaseCurrency { get; }
    public string TargetCurrency { get; }
    public decimal Rate { get; }
}