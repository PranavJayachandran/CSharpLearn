    if()
namespace BloomFilters;

public class Run{
  public static void Main(){
    List<Func<string,int>> hashFunctions = new List<Func<string,int>>{
      t => hash(37,t),
      t => hash(31,t),
      t => hash(29,t)
    };
    BloomFilter<string> bloomFilter = new BloomFilter<string>(10, hashFunctions);
    using (StreamReader str = new StreamReader("messages.txt")){
      string line;
      while((line = str.ReadLine()) != null){
        bloomFilter.Add(line);
      }
    }


    using (StreamReader str = new StreamReader("test.txt")){
      string line;
      while((line = str.ReadLine()) != null){
        Console.WriteLine($"{line} : {bloomFilter.Exists(line)}");
      }
    }
    
  }
  public static int hash(int seed, string val){
    int hash = 0;
    int prod = 1;
    for(int i = val.Length - 1; i >= 0; i--){
      hash = hash + val[i] * prod;
      prod *= 10;
    }
    return hash%seed;
  }
} 

public class BloomFilter<TValue>{
  private readonly int size;
  private readonly bool[] bits = [];
  private readonly List<Func<TValue, int>> hashFunctions = [];

  public BloomFilter(int size, List<Func<TValue, int>> hashFunctions){
    this.hashFunctions = hashFunctions;
    this.size = size;
    bits = new bool[size];
  }
  public void Add(TValue message){
    hashFunctions.Select(func => func(message)).ToList().ForEach(Mark);
  }
  public bool Exists(TValue message){
    return hashFunctions.Select(func => func(message)).All(IsMarked);
  }
  private void Mark(int pos){
    bits[pos%size] = true;
  }
  private bool IsMarked(int pos){
    return bits[pos%size];
  }
}
