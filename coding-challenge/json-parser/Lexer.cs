namespace JsonParser;

public class Lexer{

  private string s = "";
  private int index = 0;

  public int GetIndex(){
    return index;
  }
  public Lexer(string s){
    this.s = s;
  }
  public void PeekBackIndex(){
    index--;
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
        Val = s[index++],
        Type = NodeType.DoubleQuotes
      };
    }
    else if(s[index] == '\''){
      return new Node{
        Val = s[index++],
        Type = NodeType.SingleQuotes
      };
    }
    else if(s[index] == ':'){
      return new Node{
        Val = s[index++],
        Type = NodeType.Colon
      };
    }
    else if(int.TryParse(s[index].ToString(), out int result)){
      index++;
      return new Node{
        Val = result,
        Type = NodeType.Number
      };
    }
    else if(s[index] == '['){
      return new Node{
        Val = s[index++],
        Type = NodeType.SquareOpen
      };
    }
    else if(s[index] == ']'){
      return new Node{
        Val = s[index++],
        Type = NodeType.SquareClose
      };
    }
    else if(s[index] == '{'){
      return new Node{
        Val = s[index++],
        Type = NodeType.CurlyOpen
      };
    }
    else if(s[index] == '}'){
      return new Node{
    Val = s[index++],
        Type = NodeType.CurlyClose
      };
    }
    else if(s[index] == ' '){
      return new Node{
        Val = s[index++],
        Type = NodeType.Space
      };
    }
    else if(s[index] == ','){
      return new Node{
        Val = s[index++],
        Type = NodeType.Comma
      };
    }
    return new Node{
      Val = s[index++],
      Type = NodeType.Char
    };
  }
}


public enum NodeType{
  DoubleQuotes,
  SingleQuotes,
  Char,
  Colon,
  Number,
  SquareOpen,
  SquareClose,
  CurlyOpen,
  CurlyClose,
  Space,
  Comma,
  End,
}
