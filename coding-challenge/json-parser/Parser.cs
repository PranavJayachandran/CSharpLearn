namespace JsonParser;

public class Parser(Lexer lexer){
  public void Run(){
    var val = lexer.GetNext();
    if(val.Type != NodeType.CurlyOpen){
      RaiseException();
    }
    Check();
  }

  private void Check(){
    CheckKey();

    CheckColon();

    CheckValue();

    var val = lexer.GetNext();
    if(val.Type == NodeType.Comma){
      Check();
    }
    lexer.PeekBackIndex();
    val = lexer.GetNext();
    if(val.Type != NodeType.CurlyClose){
      RaiseException();
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

  public void RaiseException(){
    throw new Exception($"Invalid at {lexer.GetIndex()}");
  }
}

