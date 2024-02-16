using System.Net.Http.Headers;

namespace CS_Request {
    class HttpRequest {
        public static void GetWebHeaders(HttpResponseHeaders headers) {
            foreach(var header in headers) {
                Console.WriteLine($"{header.Key} - {header.Value}");
            }
        }

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