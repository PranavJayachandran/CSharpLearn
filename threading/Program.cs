
public class C
{
    static readonly object locker = new object();
    public static void Work(object val)
    {
    }

    public static void Main()
    {
        Task<string> val= Task.Factory.StartNew<string>(()=>{
            Thread.Sleep(3000);
            return "Hello";
        });
    Thread.Sleep(4000);
        Console.WriteLine("something");
        Console.WriteLine(val.Result);
    }

}
