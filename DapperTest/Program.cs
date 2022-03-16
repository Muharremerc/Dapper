using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace DapperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                                              .AddSingleton<DapperContext>()
                                              .BuildServiceProvider();
            Console.WriteLine("Hello World!");
        }

    }
}
