namespace CompressionTool;
public class CompressionTool{
  private static string outputFile = "output.txt";
  private static string inputFile = "input.txt";
  static Dictionary<string, string> codeMap = new();
  private static string rawString = "";
  public static void Inorder(Node root, string code){
    if(root.IsLeaf){
      codeMap.Add(root.Value, code);
    }
    if(root.Left != null)
      Inorder(root.Left, code + "0");
    if(root.Right != null)
      Inorder(root.Right, code + "1");
  }

  public static void BFS(Node root){
    Queue<Node>q = new Queue<Node>();
    q.Enqueue(root);
    while(q.TryDequeue(out Node node)){
      Console.WriteLine($"vale : {node.Value}");
      if(node.Left != null)
        q.Enqueue(node.Left);
      if(node.Right != null)
        q.Enqueue(node.Right);
    }
  }
  public static void Main(){

    File.WriteAllText(outputFile, "");

    string s;
    using (StreamReader str = new StreamReader(inputFile))
    {
      s = str.ReadToEnd();
    }
    rawString = s;
    Dictionary<char,int> freq = new Dictionary<char, int>();
    foreach(char el in s){
      if(!freq.TryAdd(el ,1))
        freq[el]++;
    }
    PriorityQueue<Node,int> pq = new();
    foreach(var item in freq){
      pq.Enqueue(new Node(item.Value, item.Key.ToString(), true, null,null), item.Value);
    }
    Node root = null;
    while(pq.Count > 1){
      pq.TryDequeue(out Node node1, out int p1);
      pq.TryDequeue(out Node node2, out int p2);

      Node newNode = new Node(node1.Weight + node2.Weight, node1.Value + node2.Value, false, node1, node2);
      root = newNode;
      pq.Enqueue(root, node1.Weight + node2.Weight);
    }

    Inorder(root, "");
//    BFS(root);

    foreach(var val in codeMap){
      if(val.Key == "\n"){
        Write($"{"\\n"}:{val.Value}");
        continue;
      }
      Write($"{val.Key}:{val.Value}");
    }
    string decode = "";
    foreach(char el in s){
      decode += codeMap[el.ToString()];
    }

    Write(decode);

    Decode();
  }

  private static void Decode(){
    string s;

    var encodeMap = new Dictionary<string,string>();
    using (StreamReader str = File.OpenText(outputFile)){
      s = str.ReadToEnd().TrimEnd();
    }

    var entries = s.Split("\n", StringSplitOptions.RemoveEmptyEntries);
    for(int i=0;i<entries.Length - 1; i++){
      var t = entries[i].Split(":");
      if(t[0] == "\\n")
        t[0] = "\n";
      encodeMap.Add(t[1],t[0]);
    }

    string encodedString = entries.Last();

    string currentString = "";
    string decodedString = "";
    foreach(var v in encodedString){
      currentString += v;
      if(encodeMap.TryGetValue(currentString,out string d)){
        decodedString += d;
        currentString = "";
      }
    }
    Console.WriteLine(rawString == decodedString);
  }

  private static void Write(string s){
    using (StreamWriter writer = new StreamWriter(outputFile, append: true))
    {
      if (new FileInfo(outputFile).Length > 0)
          writer.WriteLine();
      writer.Write(s);
    }
  }
}
