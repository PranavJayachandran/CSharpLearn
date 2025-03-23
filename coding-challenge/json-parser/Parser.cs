namespace JsonParser;

public class Parser(Lexer lexer){
  
  public void JsonParse(){
    var val = lexer.GetNext();
    if(val.Type != NodeType.CurlyOpen){
      RaiseException();
    }
    val = lexer.GetNext();
    if(val.Type == NodeType.CurlyClose){
      return ;
    }
    lexer.PeekBackIndex();
    Check();
    lexer.PeekBackIndex();
    val = lexer.GetNext();
    if(val.Type != NodeType.CurlyClose){
      RaiseException();
    }
  }

  private void Check(){
 
    CheckKey();
    CheckColon();
    CheckValue();

    var val = lexer.GetNext();
    if(val.Type == NodeType.Comma){
      Check();
    }
  }

  public void CheckKey(){
    var val = lexer.GetNext();
    while(val.Type == NodeType.Space){
      val = lexer.GetNext();
    }

    if(val.Type != NodeType.DoubleQuotes){
      RaiseException();
    }

    string key = "";
    val = lexer.GetNext();
    while(val.Type != NodeType.DoubleQuotes){
      if(val.Type != NodeType.Space && val.Type != NodeType.Char && val.Type != NodeType.Number){
        Console.WriteLine(val.Val);
        RaiseException();
      }
      key += val.Val;
      val = lexer.GetNext();
    }
    Console.WriteLine($"Key {key}");
  }

  public void CheckColon(){
    var val = lexer.GetNext();
    while(val.Type == NodeType.Space){
      val = lexer.GetNext();
    }

    if(val.Type != NodeType.Colon){
      RaiseException();
    }
    Console.WriteLine(":");
  }

  public void CheckValue(){
    var val = lexer.GetNext();
    while(val.Type == NodeType.Space){
      val = lexer.GetNext();
    }
    if(val.Type == NodeType.Number){
      lexer.PeekBackIndex();
      GetNumber();
    }
    else if(val.Type == NodeType.DoubleQuotes){
      GetString();
    }
    else if(val.Type == NodeType.SingleQuotes){
      GetCharacter();
    }
    else if(val.Type == NodeType.SquareOpen){
      CheckValue();
      string s = "";
      while(val.Type == NodeType.Space){
        s += val.Val;
        val = lexer.GetNext();
      }
      if(val.Type == NodeType.Comma){
        CheckValue();
      }
      Console.WriteLine(s);
      val = lexer.GetNext();
      if(val.Type != NodeType.SquareClose)
        RaiseException();
    }
    else if(val.Type == NodeType.SquareClose){
      lexer.PeekBackIndex();
    }
    else if(val.Type == NodeType.CurlyOpen){
      lexer.PeekBackIndex();
      JsonParse();
    }
    else if(val.Type == NodeType.CurlyClose){
      lexer.PeekBackIndex();
    }
    else{
      Console.WriteLine(val.Val);
      lexer.PeekBackIndex();
      GetKeyword();
    }
  }

  private void GetNumber(){
    int number = 0;
    var val = lexer.GetNext();
    while(val.Type == NodeType.Number){
      number *= 10 + (int)val.Val;
      val = lexer.GetNext();
    }
    lexer.PeekBackIndex();
  }

  private void GetString(){
    string s = "";
    var val = lexer.GetNext();
    while(val.Type != NodeType.DoubleQuotes){
      s += val.Val;
      val = lexer.GetNext();
    }
    Console.WriteLine(s);
  } 

  private void GetCharacter(){
    var val = lexer.GetNext();
    if(val.Type != NodeType.Char)
      RaiseException();
    val = lexer.GetNext();
    if(val.Type != NodeType.SingleQuotes)
      RaiseException();
  } 


  private void GetKeyword(){
    int i = 0;
    string s = "";
    var val = lexer.GetNext();
    while(i < 5 && val.Type == NodeType.Char){
      s += val.Val.ToString();
      i++;
      if(i == 4 && (s == "true" || s == "null")){
        Console.WriteLine(s);
        return;
      }
      val = lexer.GetNext();
    }
    if(i!=5){
      RaiseException();
    }
    if(s == "false"){
      lexer.PeekBackIndex();
      Console.WriteLine(s);
      return;
    }
    RaiseException();
  }
  public void RaiseException(){
    throw new Exception($"Invalid at {lexer.GetIndex()}");
  }
}

