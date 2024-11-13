using System;
using System.IO;
namespace Terminator;
class Program
{
    public static void Main()
    {
        string dirName = @"E:\\test";
        Terminate(dirName);

        Console.ReadKey();
    }

    static void Terminate(string dirName)
    {
        if (Directory.Exists(dirName))
        {
            string[] files = Directory.GetFiles(dirName);
            foreach (string file in files) {
                try { if (DateTime.Now - File.GetLastWriteTime(file) > TimeSpan.FromMinutes(30)) { File.Delete(file); } }
                catch (Exception e) { Console.WriteLine($"Error: {e}"); }
            }
            string[] dirs = Directory.GetDirectories(dirName);
            foreach (string dir in dirs) {
                if (DateTime.Now - Directory.GetLastWriteTime(dir) > TimeSpan.FromMinutes(30))
                {
                    try { Terminate(dir); } catch (Exception e) { Console.WriteLine($"Error: {e}"); };
                    try { Directory.Delete(dir); } catch { }
                }
            }
        }

    }
}