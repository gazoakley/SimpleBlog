using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;

namespace SimpleBlog
{
    public class PostModule : NancyModule
    {
        public PostModule()
        {
            Get["/"] = _ =>
            {
                return "Hello world";
            };
        }
    }
}
