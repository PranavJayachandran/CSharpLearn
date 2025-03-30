namespace SkipList;


public class Run{
  public static void Main(){
    Console.WriteLine("Worlked");
    SkipList skipList = new SkipList(4);
    skipList.Insert(1, "one");
    skipList.Print();
    skipList.Insert(3,"three");
    skipList.Insert(2,"two");
    skipList.Insert(4,"two");
    skipList.Insert(5,"two");
    skipList.Insert(10,"two");
    skipList.Print();
    skipList.Insert(6,"two");
    skipList.Insert(11,"two");
    skipList.Print();
    skipList.Delete(5);
    skipList.Insert(6,"three");
    skipList.Print();
  }
}
public class SkipList{
  private int maxLevel;
  private List<Node>heads;
  private static readonly Random rand = new Random();

  public SkipList(int maxLevel){
    this.maxLevel = maxLevel;
    this.Init();
  }
  public void Insert(int key, string val){
    List<Node>update = null; 
    Node node = Search(key,out update);
    if(node.Forward[0]?.key != key){
      int level = GenerateRandomLevel();
      Node newNode = new Node(key,val,level);
      for(int i = 0;  i <= level; i++){
        newNode.Forward[i] = update[i].Forward[i];
        update[i].Forward[i] = newNode;
      }
    }
    else{
      Console.WriteLine("HERE");
      node.Forward[0].Val = val;
    }
  }
  public void Delete(int key){
    Node node = Search(key, out List<Node> update);
    node = node.Forward[0];
    if(node.key == key){
      for(int i = 0; i < update.Count ; i++){
        if(update[i].Forward[i]?.key == key){
        update[i].Forward[i] = node.Forward[i];
        }
      }
    }
  }
  public void Print(){
    int level = maxLevel-1;
    foreach(var head in heads){
      var node = head;      
      while(node != null){
        if(node.key == -1){
          Console.Write("HEAD");
        }
        else
          Console.Write("->"+ node.key.ToString() + ":" + node.Val);
        node = node.Forward[level];
      }
      Console.WriteLine();
      level--;
    }
    Console.WriteLine();
  }
  private void Init(){
  heads = Enumerable.Range(0, maxLevel)
                 .Select(_ => new Node(-1, "HEAD", maxLevel - 1))
                 .ToList();
}
  private Node Search(int key, out List<Node> update){
    update = Enumerable.Repeat<Node>(null,maxLevel).ToList();
    var node = heads.First();
    for(int i = maxLevel - 1;i>=0;i--){
      while(node!=null && node.Forward[i]?.key < key){
        node = node.Forward[i];
      }
      update[i] = node;
    }
    return node;
  }
  private int GenerateRandomLevel(){
    return rand.Next(maxLevel);
  }
}
