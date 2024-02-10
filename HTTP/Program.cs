using System.Net.NetworkInformation;

var ping = new Ping();
var pingReply = ping.Send("google.com.vn");
Console.WriteLine(pingReply.Status);
if (pingReply.Status == IPStatus.Success)
{
    Console.WriteLine(pingReply.RoundtripTime);
    Console.WriteLine(pingReply.Address);
}
