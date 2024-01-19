
public class Document 
{
    public int Id { get; set; }
    public string Data { get; set; }
    public string Name { get; set; }
    //......
}

/// <summary>
/// this interface represents a modern printing machine that takes in a document and
/// prints and scans and faxs it. so we add those behaviours to it, and the modern
/// printing machine class implemts it but its a bulky interface, and this will lead to problems
/// when older printing machines or more specified machines that only print or fax or scans try to implement
/// that bulky interface and here, we realize that making a bulky interface with all that varies
/// violates the Interface egregation Principle. What we should do instead is make smaller interfaces, each for what varies
/// </summary>
public interface IModernMachine
{

    //void print(Document d);
    //void scan(Document d);
    //void fax(Document d);
}

public interface IModernMachineV2 : IPrint, IScan, IFax
{
    // what is specific to a modern machine that doesn't vary from one machine to another
    // because what varies gets seperated to another interface
}

/// <summary>
/// this is the new way of seperating interfaces
/// </summary>
public interface IPrint {void print(Document d);}
public interface IScan { void scan(Document d);}
public interface IFax {void fax(Document d);}
/// <summary>
/// we can see that we can implemenent each interface seperatly and reach the same result
/// in a more clean and understandable and seperated way
/// </summary>
public class ModernPrintingMachine : /*IModernMachine*/IPrint,IFax,IScan
{
    public void fax(Document d)
    {
        Console.WriteLine("Faxes");
    }

    public void print(Document d)
    {
        Console.WriteLine("Prints");
    }

    public void scan(Document d)
    {
        Console.WriteLine("Scans");
    }
}

/// <summary>
/// here we see the old way of making a bulky interface, which makes us implement
/// those that we don't need, and thats what we also notice when we use the new 
/// way and comment the old one
/// </summary>

public class OlderPrintingMachine : /*IModernMachine*/ IPrint
{
    //public void fax(Document d)
    //{
    //    Console.WriteLine("Doesn't Fax");
    //}

    public void print(Document d)
    {
        Console.WriteLine("Only Prints");
    }

    //public void scan(Document d)
    //{
    //    Console.WriteLine("Doesn't Scan");
    //}
}
public class OlderScanningMachine : /*IModernMachine*/ IScan
{
    //public void fax(Document d)
    //{
    //    Console.WriteLine("Doesn't Fax");
    //}

    //public void print(Document d)
    //{
    //    Console.WriteLine("Doesn't Prints");
    //}

    public void scan(Document d)
    {
        Console.WriteLine("Only Scan");
    }
}

/// <summary>
/// we can also use the decorater design pattern and make it so that each morder printer has a printer,a scanner and a fax
/// (the concrete classes which implement each of their slim interfaces) and then make those members implement their respective methods
/// in other words saying that that modern machine is simply the joinning of a printer, a fax and a scanner
/// </summary>
public class ModernPrintingMachineDecorated : IModernMachineV2
{
    private IPrint _printer;
    private IScan _scanner;
    private IFax _fax;
    public ModernPrintingMachineDecorated(IPrint printer, IScan scanner, IFax fax)
    {
        _printer = printer == null ? throw new ArgumentNullException() : printer;
        _scanner = scanner == null ? throw new ArgumentNullException() : scanner;
        _fax = fax == null ? throw new ArgumentNullException() : fax;
    }

    public void print(Document d)
    {
        _printer.print(d);
    }

    public void scan(Document d)
    {
        _scanner.scan(d);
    }

    void IFax.fax(Document d)
    {
        _fax.fax(d);
    }
}