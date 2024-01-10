# 📘 Single Responsibility Principle (SRP)

## 💡 Introduction to SRP

What is SRP? The Single Responsibility Principle is a key concept in object-oriented design, emphasizing that a class should have only one reason to change - essentially, a single job or responsibility.
Goal: The principle aims to enhance software maintainability, scalability, and readability by segmenting complex systems into smaller, focused parts.

## 🔍 Understanding SRP

Responsibility: In SRP, a 'responsibility' refers to a single reason for a class to change.
Benefit: Adherence to SRP leads to less coupled, more modular classes, simplifying maintenance and future development.

## 💻 Code Demonstration

### 📓 Journal Class

Role: Manages journal entries.
Functions:
AddEntry: Adds a new entry.
RemoveEntry: Removes an entry.
ToString: Returns all entries in a string format.

public class Journal
{
private readonly List<string> entries = new List<string>();
private static int count = 0;

    public int AddEntry(string text)
    {
        entries.Add($"{++count}: {text}");
        return count; // Entry count
    }

    public void RemoveEntry(int index)
    {
        entries.RemoveAt(index);
    }

    public override string ToString()
    {
        return string.Join("\n", entries);
    }

}

### 💾 Persistence Class

Role: Handles saving objects like Journal.
Functions:
SaveToFile: Saves the journal to a file.

public class Persistence
{
public void SaveToFile(Journal j, string fileName, bool overwrite = false)
{
if (overwrite || !File.Exists(fileName))
{
File.WriteAllText(fileName, j.ToString());
}
}
}

## ⚙️ Main Program

Illustrates using Journal and Persistence to add entries and save them.

public class Program
{
public static void Main()
{
Journal j = new Journal();
j.AddEntry("I Went Out Yesterday");
j.AddEntry("I Had Fun Today");

        Console.WriteLine(j.ToString());

        var p = new Persistence();
        // The @ symbol here allows for a literal string, making the file path easier to write and read.
        string fileName = @"D:\Design Patterns Repo\journal.txt";
        p.SaveToFile(j, fileName, true);

        Process.Start("notepad.exe", fileName);
    }

}

## 🏁 Conclusion

SRP Benefits: This implementation showcases the Single Responsibility Principle by dividing journal management and data persistence into separate classes for improved modularity and maintainability.
