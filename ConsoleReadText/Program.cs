using System;

namespace ConsoleReadText
{
    class Program
    {
        static void Main(string[] args)
        {
            //working with .txt
            TextFile.Read();
            TextFile.AddData();
            TextFile.Write();

            TextFile.WriteMotorData();
            TextFile.ReadMotordata();
            
            //working with Json
            JsonFile.WriteJson();
            JsonFile.ReadJson();
        }
    }
}
