
using System.Diagnostics;

public class Journal 
{
    private readonly List<string> entries = new List<string>();
    private static int count = 0;
    public int AddEntry(string text) 
    {
        entries.Add($"{++count}: {text}");
        return count;
    }
    public void removeEntry(int index) 
    {
        entries.RemoveAt( index );
    }
    public override string ToString()
    {
        return string.Join( "\n", entries );
    }

    /// <summary>
    /// this has to do with the presistence of the journal
    /// which violates the single responsibility principle
    /// each class should be responsible for one thing and it does it well
    /// so this class should be responsible for keeping a bunch of entrier on the jounral
    /// and another class should be responsible for presistence of the journal
    /// </summary>

    //public void Save(string fileName) 
    //{
    //    File.WriteAllText(fileName,ToString());
    //}
    //public void Load(string fileName) 
    //{
    //    //implementation
    //}
    //public static Journal Load(string URI) 
    //{
    //    //implementation
    //}
}

//new class to handle the persistence of all sorts of things not only jounrals.

public class Persistence 
{
    public void SaveToFile(Journal j, string fileName, bool overwrite = false) 
    {
        if (overwrite || File.Exists(fileName))
        {
            File.WriteAllText(fileName,j.ToString());
        }
    }
}


public class Program 
{
    public static void Main() 
    {
        Journal j = new Journal();
        j.AddEntry("I Went Out Yesterday");
        j.AddEntry("I Had Fun Today");
        Console.WriteLine(j.ToString());

        var p = new Persistence();
        string fileName = @"D:\Design Patterns Repo\journal.txt";
        p.SaveToFile(j,fileName,true);
        Process.Start("notepad.exe", fileName);
    }
}