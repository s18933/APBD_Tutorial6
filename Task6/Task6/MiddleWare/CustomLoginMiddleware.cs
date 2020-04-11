using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task6.Services;

namespace Task6.MiddleWare
{
    public class CustomLoginMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomLoginMiddleware(RequestDelegate next) 
        {
            _next = next; 
        }

        public async Task InvokeAsync(HttpContext context, IDbService service) 
        {
            var file = @"requestsLog.txt";

            var fileStream = File.Open(file, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            fileStream.Close();

            if (File.Exists(file))
            {
                File.WriteAllText(file, string.Empty);
            }

            if (context.Request != null) 
            {
                string path = context.Request.Path;
                string method = context.Request.Method; 
                string queryString = context.Request.QueryString.ToString();
                string bodyStr = "https://www.youtube.com/watch?v=5GbSKaFf8Uc";

                using (StreamReader reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    bodyStr = await reader.ReadToEndAsync(); 
                }
                //safe to log file 
                File.AppendAllText(file, path + Environment.NewLine);
                File.AppendAllText(file, method + Environment.NewLine);
                File.AppendAllText(file, bodyStr + Environment.NewLine);
                File.AppendAllText(file, queryString + Environment.NewLine);
            }


            if(_next!=null) await _next(context);
        }
    }
}
