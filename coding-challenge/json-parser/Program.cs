struct Node{
  public object Val {get; init;}
  public NodeType Type {get; init;}
}

class JsonParser{
  public static void Main(string[] args){
    string s;
    using(StreamReader str = File.OpenText("step1/valid.json"))
    {
      s = str.ReadToEnd();
    }
    Console.WriteLine(s);
  }
}

class Lexer{
  string s = "";
  int index = 0;
  public Lexer(string s){
    this.s = s;
  }
  public Node GetNext(){
    if(s.Length == index){
      return new Node{
        Val = "",
        Type = NodeType.End
      };
    }

    if(s[index] == '"'){
      return new Node{
        Val = "\"",
        Type = NodeType.DoubleQuotes
      };
    }
    else if(s[index] == '\''){
      return new Node{
        Val = "'",
        Type = NodeType.SingleQuotes
      };
    }
    else if(char.IsAsciiLetterOrDigit(s[index])){
      string val = "";
      while(index < s.Length && char.IsAsciiLetterOrDigit(s[index])){
        val += s[index];
        index++;
      }
      return new Node{
        Val = val,
        Type = NodeType.String
      };
    }
    return new Node{};
  }
}


enum NodeType{
  DoubleQuotes,
  SingleQuotes,
  String,
  Colon,
  Number,
  Float,
  SquareOpen,
  SquareClose,
  CurlyOpen,
  CurlyClose,
  End,
}
