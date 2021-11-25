using DemoConsoleReader;
using DemoConsoleReader.Models;
using DemoConsoleReader.TextFileReaders;
using DemoConsoleReader.XmlFileReaders;
using System;
using System.Collections.Generic;


Console.WriteLine("DemoConsoleReader");

IFileReader jsonReader = new JsonReader(new StreamWrapperJson());
IFileReader txtReader = new TxtReader(new StreamWrapperTxt());
IFileReader xmlFileReader = new XmlFileReader();

//text
string line1 = "some text in line1";
string line2 = "this text added after first line";

txtReader.CreateFile("test.txt", new object[] { line1});
txtReader.AddDataToFile("test.txt", line2);
var txt_list = txtReader.ReadFile<string>("test.txt");

foreach (var item in txt_list)
{
    Console.WriteLine(item);
}

//json
Engine engine1 = new() { Name = "Engine1", SerialNumber = 101, Operator = new() { Name = "Matt Fraser" }, Measurements = new List<int> {21, 15, 9 } };
Engine engine2 = new() { Name = "Engine2", SerialNumber = 201, Operator = new() { Name = "Rich Froning" }, Measurements = new List<int> { 9, 15, 21 } };

jsonReader.CreateFile("test.json", new object[] { engine1});
jsonReader.AddDataToFile("test.json", engine2);
var list = jsonReader.ReadFile<Engine>("test.json");

foreach (Engine item in list)
{
    Console.WriteLine($"Name: {item.Name} S/N:{item.SerialNumber}");
}

//xml
xmlFileReader.CreateFile("text.xml", new object[] { engine1});
xmlFileReader.AddDataToFile("text.xml", engine2);
var xml = xmlFileReader.ReadFile<string>("text.xml");
foreach (var item in xml)
{
    Console.WriteLine(item);
}

