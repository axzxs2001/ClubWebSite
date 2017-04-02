using System;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace UEditorNetCore.Handlers
{
    public abstract class Handler
    {
        public HttpRequest Request { get; private set; }
        public HttpResponse Response { get; private set; }
        public HttpContext Context { get; private set; }

        public Handler(HttpContext context)
        {
            this.Request = context.Request;
            this.Response = context.Response;
            this.Context = context;
            //this.Server = context.Server;
        }

        public abstract void Process();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        protected void WriteJson(object response)
        {
            string jsonpCallback = Context.Request.Query["callback"],
                json = JsonConvert.SerializeObject(response);
            if (String.IsNullOrWhiteSpace(jsonpCallback))
            {
                //Context.Response.OnStarting(async () =>
                //{
                //    Context.Response.Headers.Add("Content-Type", "text/plain");
                //    await Context.Response.WriteAsync(json);
                //});
                Response.Headers.Add("Content-Type", "text/plain");
                Response.WriteAsync(json);
            }
            else
            {
                //Context.Response.OnStarting(async () =>
                //{
                //    Context.Response.Headers.Add("Content-Type", "application/javascript");
                //    await Context.Response.WriteAsync($"{jsonpCallback}({json});");
                //});
                Response.Headers.Add("Content-Type", "application/javascript");
                Response.WriteAsync(String.Format("{0}({1});", jsonpCallback, json));
            }


        }
    }
}