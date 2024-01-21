using System.Diagnostics.Tracing;
using System.Net.Security;
using System.Text;

public class Program 
{
    public static void Main() 
    {
        ///<summary>
        /// we will use the built in string builder to try and build an html element
        /// without an html builder and then see how hard it is, and then we will make a builder
        /// and see the difference
        ///</summary>

        var str = "Hello";
        var sb = new StringBuilder();
        sb.Append("<p>");
        sb.Append(str);
        sb.Append("</p>");
        Console.WriteLine(sb.ToString());

        ///<summary>
        ///Now if we want to add multiple html elements
        ///maybe a list and inside of it a couple of li tags
        ///then we notice how tiresome, repititive and inscalable it is
        ///so now we will implement the html builder class and see the difference
        ///</summary>
        ///
        string[] words = { "Hello", "World" }; 

        sb.Clear();
        sb.Append("<ul>");
        foreach (var word in words)
        {
            sb.Append("<li>");
            sb.Append(word);
            sb.Append("</li>");
        }
        sb.Append("</ul>");
        Console.WriteLine(sb);

        HtmlBuilder builder = new HtmlBuilder("ul");
        builder.AddElement("li","hello");
        builder.AddElement("li", "world");
        Console.WriteLine(builder);
    }
}

public class HtmlElement 
{
    public string Name;
    public string Text;
    public List<HtmlElement> Elements = new List<HtmlElement>();
    private const int identSize = 2;
    public HtmlElement() 
    {

    }

    public HtmlElement(string name, string text) 
    {
        Name = name == null ? throw new ArgumentNullException(paramName:nameof(name)): name;
        Text = text == null ? throw new ArgumentNullException(paramName: nameof(text)) : text;
    }

    ///<summary>
    ///we will keep using the string builder because its a good builder for strings
    ///</summary>

    public string ToStringImpl(int indent) 
    {
        var sb = new StringBuilder();
        var i = new string (' ',identSize * indent);
        sb.AppendLine($"{i}<{Name}>");
        if (!string.IsNullOrWhiteSpace(Text))
        {
            sb.Append($"{new string (' ', identSize *(indent +1 ))}");
            sb.AppendLine(Text);
        }
        
        foreach (var item in Elements)
        {
            sb.Append(item.ToStringImpl(indent + 1));
        }
        sb.AppendLine($"{i}</{Name}>");
        return sb.ToString();
    }
    public override string ToString()
    {
        return ToStringImpl(0);
    }
}

public class HtmlBuilder 
{
    private readonly string rootName;
    public HtmlElement root = new HtmlElement();

    public HtmlBuilder(string rootName) 
    {
        this.rootName = rootName;
        root.Name = rootName;
    }
    public void AddElement(string name,string text) 
    {
        root.Elements.Add(new HtmlElement(name,text));
        
    }
    public override string ToString()
    {
        return root.ToString();
    }
    public void Clear() 
    {
        root = new HtmlElement {Name = rootName };
    }
}