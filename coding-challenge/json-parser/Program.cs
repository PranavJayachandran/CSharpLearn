﻿namespace JsonParser;

class JsonParser{
  public static void Main(){
    string s;
    using(StreamReader str = File.OpenText("step4/valid2.json"))
    {
      s = str.ReadToEnd();
    }
    Console.WriteLine(s);
    Parser parser = new Parser(new Lexer(s));
    parser.JsonParse();
  }
}
