using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Tweetinvi;
using System.IO;
using System.Text;

namespace BlockTwitterAccouts
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var file = File.ReadAllText("./ngWordText/test.csv", Encoding.UTF8);
            var ngWordList = file.Split(',');
            Console.WriteLine("**********************");
            foreach(var ngWord in ngWordList){
                Console.WriteLine(ngWord);
            }
            Console.WriteLine("**********************");
        }
    }
}