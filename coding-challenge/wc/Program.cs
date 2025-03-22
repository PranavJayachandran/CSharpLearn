using System.Text.RegularExpressions;

class WC
{
  public static void Main(string []args)
  {
    string s;
    using(StreamReader sr = File.OpenText("test.txt"))
    {
       s = sr.ReadToEnd();
    }
    string val = Console.ReadLine();
    val = val.Trim();
    var input = val.Split(" ");
    List<string> validArgs = ["-c","-l", "-w", "-m"];

    if(input[0] != "wc")
    {
      Console.WriteLine($"Unkown command {input[0]}");
    }
    else if (input.Length > 1 && !validArgs.Contains(input[1]))
    {
      Console.WriteLine($"Invalid arg {input[1]}");
    }
    else if(input.Length > 2)
    {
      Console.WriteLine("Invalid format for the command");
    }

    if(input.Length == 1 || input[1] == "-c")
      Console.WriteLine(s.Length);
    if(input.Length == 1 || input[1] == "-l")
      Console.WriteLine(s.Count(c => c == '\n' ));
    if(input.Length == 1 || input[1] == "-w")
    {
      var words = s.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
      Console.WriteLine(words);
    }
    if(input.Length == 1 || input[1] == "-m"){
      Console.WriteLine(s.Length);
    }
  }
}
