using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ConsoleReadText
{
    public static class TextFile
    {
        //1. Reading/writing text files
        
        //reading file
        public static void Read()
        {
            VisualSugar.DecorateTitle("Reading text file", ConsoleColor.Green);

            try
            {
                using (StreamReader sr = new StreamReader("data.txt", Encoding.Default))
                {
                    //Console.WriteLine(sr.ReadToEnd());//reading entire file

                    string line;
                    while ((line = sr.ReadLine()) != null) //reading line by line
                    {
                        Console.WriteLine(line);
                    }
                }
            }

            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        //adding data to file
        public static void AddData()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("data.txt", append: true))
                {
                    sw.WriteLine("Measure4 : 20");
                }
                Read();
            }

            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        //writing file
        public static void Write()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("data.txt", append: false))
                {
                    string[] info = new string[] 
                    {
                        "Motor name: motor1",
                        "Serial number: 101",
                        "Operator: Matt Fraser",
                        "Measure1: 5",
                        "Measure2: 10",
                        "Measure3: 15"
                    };

                    foreach (string s in info)
                    sw.WriteLine(s);
                }
                Read();
            }

            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }


        //In some cases, when txt files are big enough:
        async static void ReadAsync()
        {
            try
            {
                using (StreamReader sr = new StreamReader("data.txt", Encoding.Default))
                {
                    string filedata = await sr.ReadToEndAsync();

                    //or
                    string line;
                    while ((line = await sr.ReadLineAsync()) != null) //reading line by line
                    {
                        Console.WriteLine(line);
                    }
                }
            }

            catch { }
        }

        async static void AddDataAsync()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("data.txt", append: true))
                {
                    await sw.WriteLineAsync("Measure4 : 20");
                }
            }

            catch { }
        }

        //2. Serialization
        //in this case serializable class should be used (Motor)

        //serialization
        public static void WriteMotorData()
        {
            VisualSugar.DecorateTitle("Serialization to file", ConsoleColor.Magenta);
           
            Motor m1 = new Motor("Motor1", 102, "Rich Froning");
            BinaryFormatter formatter = new BinaryFormatter();

            //or for array
            Motor[] motors = new Motor[2];
            motors[0] = m1;
            motors[1] = new Motor("Motor2", 103, "Ben Smith");

            try
            {
                using (FileStream fs = new FileStream("motor.dat", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, m1);
                    //formatter.Serialize(fs, motors);//for array
                    Console.WriteLine("Serialization completed");
                    
                }
            }

            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        //deserialization
        public static void ReadMotordata()
        {
            VisualSugar.DecorateTitle("Deserialization", ConsoleColor.Magenta);
           
            var formatter = new BinaryFormatter();

            try
            {
                using (FileStream fs = new FileStream("motor.dat", FileMode.OpenOrCreate))
                {
                    Motor motor = (Motor)formatter.Deserialize(fs);
                    Console.WriteLine($"Name: {motor.Name}\nSN: {motor.SerialNumber}");
                }
            }

            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }

    [Serializable]
    class Motor
    {
        public string Name { set; get; }
        public int SerialNumber { set; get; }

        [NonSerialized]
        public string User;

        public Motor(string name, int sn, string user)
        {
            Name = name;
            SerialNumber = sn;
            User = user;
        }
    }
}
