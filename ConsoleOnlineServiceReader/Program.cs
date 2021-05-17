using ConsoleOnlineServiceReader.APIData;
using System;


Console.WriteLine("Hello World!");

ClientInitializer.InitializeClient();

var sunInfo = await SunApiProcessor.GetDataForKaliningradAsync();

string textResult = $"Sunrise in Kaliningrad ABB office at {sunInfo.Sunrise.ToLocalTime().ToShortTimeString()}" +
                $"\nSunset in Kaliningrad ABB office at {sunInfo.Sunset.ToLocalTime().ToShortTimeString()}";
Console.WriteLine(textResult);
