using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json; // added package

namespace ConsoleReadText
{
    public static class JsonFile
    {
        public static void WriteJson()
        {
            VisualSugar.DecorateTitle("Writing json file", ConsoleColor.Blue);
          
            //class for serialization to Json file
            Engine engine = new Engine
            { 
                Name = "motor1", 
                SerialNumber = 101, 
                user = new User{ UserName = "Matt" },
                Measures = new List<int> { 5, 15, 25 }
            };

            try
            {
                using (StreamWriter sw = new StreamWriter("data.json"))
                {
                    var engineToText = JsonConvert.SerializeObject(engine);
                    sw.Write(engineToText);
                }

                Console.WriteLine("File saved");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }


        public static void ReadJson()
        {
            VisualSugar.DecorateTitle("Reading json file", ConsoleColor.Blue);
            
            Engine dsEngine;

            try
            {
                using (StreamReader sr = new StreamReader("data.json"))
                    dsEngine = JsonConvert.DeserializeObject<Engine>(sr.ReadLine());

                Console.WriteLine($"Engine name: {dsEngine.Name}\nOperator: {dsEngine.user.UserName}\netc...");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
