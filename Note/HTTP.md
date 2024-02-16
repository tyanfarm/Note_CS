<div style="text-align: center; background-color: salmon; font-family: 'Trebuchet MS', Arial, sans-serif; color:  #0D0907; padding: 5px; font-size: 32px; padding:20px; font-weight: bold; border-radius: 0 0 0 0; box-shadow: 0px 6px 8px rgba(0, 0, 0, 0.2);margin-bottom: 20px;">
<div>HTTP</div>
<div>(HyperText Transfer Protocol)</div>
</div>

<br/>

# System.Uri
- Giúp cho nhanh chóng lấy thông tin các thành phần của URL như host, path, query, ...

- ```
    string url = "https://tyanfarm.github.io/MusicPlayer/";

    var uri = new Uri(url);
    var uritype = typeof(Uri);

    uritype.GetProperties().ToList().ForEach(property => {
        Console.WriteLine($"{property.Name, 15} {property.GetValue(uri)}");
    });

    Console.WriteLine($"Segments: {string.Join(",", uri.Segments)}");
- Console:
    ```
    AbsolutePath /lap-trinh/csharp/
    AbsoluteUri https://xuanthulab.net/lap-trinh/csharp/?page=3#acff
      LocalPath /lap-trinh/csharp/
      Authority xuanthulab.net
   HostNameType Dns
  IsDefaultPort True
         IsFile False
     IsLoopback False
   PathAndQuery /lap-trinh/csharp/?page=3
       Segments System.String[]
          IsUnc False
           Host xuanthulab.net
           Port 443
          Query ?page=3
       Fragment #acff
         Scheme https
    OriginalString https://xuanthulab.net/lap-trinh/csharp/?page=3#acff
        DnsSafeHost xuanthulab.net
            IdnHost xuanthulab.net
    IsAbsoluteUri True
        UserEscaped False
        UserInfo
    Segments: /,lap-trinh/,csharp/
    PS D:\Tyan\ASPNET\Note_CS\HTTP> dotnet run
    AbsolutePath /MusicPlayer/
        AbsoluteUri https://tyanfarm.github.io/MusicPlayer/
        LocalPath /MusicPlayer/
        Authority tyanfarm.github.io
    HostNameType Dns
    IsDefaultPort True
            IsFile False
        IsLoopback False
    PathAndQuery /MusicPlayer/
        Segments System.String[]   
            IsUnc False
            Host tyanfarm.github.io
            Port 443
            Query
        Fragment
            Scheme https
    OriginalString https://tyanfarm.github.io/MusicPlayer/
        DnsSafeHost tyanfarm.github.io
            IdnHost tyanfarm.github.io
    IsAbsoluteUri True
        UserEscaped False
        UserInfo
    Segments: /,MusicPlayer/
<br/>

# System.Dns
- `.GetHostName()`: lấy hostname của máy local

- `.GetHostEnTry()`: trả về 1 `IPHostEntry` chứa thông tin địa chỉ của Host
    + ```
        IPHostEntry iphostentry = Dns.GetHostEntry(uri.Host);
        Console.WriteLine(iphostentry.HostName);

        iphostentry.AddressList.ToList().ForEach(ip => Console.WriteLine(ip));
    + Console:
        ```
        tyanfarm.github.io
        185.199.108.153
        185.199.109.153
        185.199.110.153
        185.199.111.153
<br/>

# System.Net.NetworkInformation.Ping
- Xác định một máy từ xa (như server, máy trong mạng ...) có phản hồi không.

- ```
    var ping = new Ping();
    var pingReply = ping.Send("google.com.vn");
    Console.WriteLine(pingReply.Status);
    if (pingReply.Status == IPStatus.Success)
    {
        Console.WriteLine(pingReply.RoundtripTime);
        Console.WriteLine(pingReply.Address);
    }
- Console:
    ```
    Success
    40
    142.250.204.99

# HTTP Client send request to HTTP Server 
- Lấy html từ web về:

- ```
    namespace CS_GetWebContent {
        class GetHttp {
            public static async Task<string> GetWebContent(string url) {
                using var httpclient = new HttpClient();

                try {
                    HttpResponseMessage httpResponseMessage = await httpclient.GetAsync(url);

                    GetWebHeaders(httpResponseMessage.Headers);
                    
                    string html = await httpResponseMessage.Content.ReadAsStringAsync();
                    
                    return html;
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);

                    return "ERROR";
                }
                
            }
        }
    }
- ```
    var url = "https://xuanthulab.net/networking-su-dung-httpclient-trong-c-tao-cac-truy-van-http.html";

    var task = GetHttp.GetWebContent(url);
    await task;

    var html = task.Result;

    Console.WriteLine(html);
<br/>
- In các headers:

- ```
    public static void GetWebHeaders(HttpResponseHeaders headers) {
            foreach(var header in headers) {
                Console.WriteLine($"{header.Key} - {header.Value}");
            }
        }



