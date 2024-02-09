using Microsoft.Extensions.Options;

namespace CS_MySerVice
{
    public class MyServiceOptions {
        public string? data1 {get; set;}
        public int data2 {get; set;}
    }

    public class MyService {
        public string? data1 {get; set;}
        public int data2 {get; set;}

        public MyService(IOptions<MyServiceOptions> options) {
            MyServiceOptions _options = options.Value;

            data1 = _options.data1;
            data2 = _options.data2;
        }

        public void PrintData() => Console.WriteLine($"{data1} / {data2}");

    }
}