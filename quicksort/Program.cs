using System.Diagnostics;

public class Do{
  public static void Main(string []args){
    Console.WriteLine(args[0]);
    List<int> val = [];
    List<int> val2 = [];
    int size;
    if(!int.TryParse(args[0], out size)){
      size = 10;
    }
    var random = new Random();
    for(int i=0;i<size;i++){
      var element = random.Next();
      val.Add(element);
      val2.Add(element);
    }
    var stopwatch = Stopwatch.StartNew();
    quickSort(val, 0 , val.Count-1);
    stopwatch.Stop();

    Console.WriteLine($"normal took {stopwatch.ElapsedMilliseconds}ms");
    stopwatch.Restart();
    quickSortParallel(val2, 0, val.Count-1);
    stopwatch.Stop();
    Console.WriteLine($"parallel took {stopwatch.ElapsedMilliseconds}ms");
  }

  private static void quickSort(List<int>val, int start, int end){
    if(start >= end)
      return;

    int part = partition(val, start, end);
    quickSort(val,start,part-1);
    quickSort(val,part + 1, end);
  }
  private static void quickSortParallel(List<int>val, int start, int end){
    if(start >= end)
      return;

    int part = partition(val, start, end);
    if(end - start > 100_000)
    Parallel.Invoke(() => quickSortParallel(val,start,part-1), () => quickSortParallel(val,part + 1, end));

    else{
      quickSort(val, start,part -1);
      quickSort(val, part + 1, end);
    }
  }

  private static int partition(List<int>val, int start, int end){
    int i = start;
    int pivot = val[end];
    for(int j = start; j < end; j++){
      if(val[j] < pivot){
        int temp2 = val[i];
        val[i] = val[j];
        val[j] = temp2;
        i++;
      }
    }
    int temp = val[i];
    val[i] = val[end];
    val[end] = temp;

    return i;
  }

}


