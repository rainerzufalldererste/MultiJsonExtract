using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MultiJsonExtract
{
    class Program
    {
        static void Main(string[] args)
        {
            var contents = from x in Directory.EnumerateFiles(args[0], args[1]).OrderBy(q => q) select File.ReadAllText(x).Trim(' ', '\n', '\r', '\t');

            foreach (var x in contents)
            {
                try
                {
                    JToken current;

                    try
                    {
                        current = JObject.Parse(x);
                    }
                    catch
                    {
                        current = JArray.Parse(x);
                    }

                    for (int i = 2; i < args.Length; i++)
                    {
                        if (args[i] == "[")
                        {
                            i++;

                            int index = int.Parse(args[i]);

                            current = ((JArray)current)[index];
                        }
                        else
                        {
                            current = ((JObject)current)[args[i]];
                        }
                    }

                    Console.Write(current.ToString() + "; ");
                }
                catch
                {
                    Console.Write("ERR0R; ");
                }
            }
        }
    }
}
