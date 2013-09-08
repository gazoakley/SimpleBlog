using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.Hosting.Self;

namespace SimpleBlog
{
    class Program
    {
        static void Main(string[] args)
        {
            var bootstrapper = new Bootstrapper();
            var nancyHost = new NancyHost(new Uri("http://localhost:1234/"), bootstrapper);
            nancyHost.Start();
            Console.Read();
            nancyHost.Stop();
        }
    }
}
