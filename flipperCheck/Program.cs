using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace flipperCheck
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;

            HtmlWeb web = new HtmlWeb();

            string prevNum = "";

            int amount = 0;

            int prevAmount = 0;
            int newAmount = 0;

            while (true)
            {
                HtmlDocument document = web.Load("https://shop.flipperzero.one/");
                var html = document.DocumentNode.OuterHtml;

                Match m = Regex.Match(html, "((\\d+) left)", RegexOptions.IgnoreCase);

                if (m.Success)
                {
                    if (prevNum != m.Value) 
                    {
                        Console.Write(m.Value);

                        newAmount = Convert.ToInt32(Regex.Match(m.Value, @"\d+").Value);
                        
                        Console.WriteLine($" ({newAmount - prevAmount} Change)");
                        
                    }
                    prevAmount = newAmount;
                    prevNum = m.Value;
                }

                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
