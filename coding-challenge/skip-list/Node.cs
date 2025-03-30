namespace SkipList;

public class Node{
  public readonly int key;
  public string Val {get; set;}
  public List<Node> Forward {get; set;}
  public int CurrentLevel {get; set;}
  private readonly int level;

  public Node(int key, string val, int level){
    this.key = key;
    Val = val;
    this.level = level;
    CurrentLevel = level;
    Forward = Enumerable.Repeat<Node>(null,level+1).ToList();
  }

  public void Reset(){
    CurrentLevel = level;
  }

  public Node GetNext(){
    var node = Forward[CurrentLevel];
    if(node != null)
      node.Reset();
    return node;
  }

  public void MoveToNextLevel(){
    CurrentLevel--;
  }
}
