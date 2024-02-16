using System.Net.NetworkInformation;
using CS_Request;

var url = "https://xuanthulab.net/networking-su-dung-httpclient-trong-c-tao-cac-truy-van-http.html";

var task = HttpRequest.GetWebContent(url);
await task;

var html = task.Result;

// Console.WriteLine(html);
