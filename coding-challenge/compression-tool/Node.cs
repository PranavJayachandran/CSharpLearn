namespace CompressionTool; 
public class Node{
  public int Weight;
  public string Value;
  public Node Left, Right;
  public bool IsLeaf;
  public Node(int weight,string val, bool isLeaf, Node left, Node right){
    Weight = weight;
    Value = val;
    IsLeaf = isLeaf;
    Left = left;
    Right = right;
  }
}
