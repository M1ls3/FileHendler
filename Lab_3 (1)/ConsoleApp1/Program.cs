using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    internal class Program
    {
        static void CheckAllFiles()
        {
            for (int i = 10; i < 30; i++)
            {
                Console.WriteLine($"File {i}: {IsFileExist(Convert.ToString(i))}");
            }
        }

        static bool IsFileExist(string indexORname)
        {
            try
            {
                FileStream fileStream = new FileStream($"{indexORname}.txt", FileMode.Open, FileAccess.Read);
                fileStream.Close();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        static void FileCreator()
        {
            Random rnd = new Random();

            for (int i = 10; i < 30; i++)
            {
                FileStream fileStream = new FileStream($"{i}.txt", FileMode.Create, FileAccess.Write);
                StreamWriter streamWriter = new StreamWriter(fileStream);

                streamWriter.WriteLine(rnd.Next(-100, 101));
                streamWriter.WriteLine(rnd.Next(-100, 101));

                streamWriter.Close();
                fileStream.Close();
            }
            Console.WriteLine($"\nFileCreator Completed!!!");
        }

        static void FileDeleterAll()
        {
            for (int i = 10; i < 30; i++)
            {
                File.Delete($"{i}.txt");
            }
            Console.WriteLine($"\nFileDeleterAll Completed!!!");
        }

        static void FileBreaker()
        {
            Random random = new Random();
            FileStream fileStream;
            int index = random.Next(10, 30);
            try
            {
                try 
                { 
                    fileStream = new FileStream($"{index}.txt", FileMode.Open, FileAccess.ReadWrite);
                    fileStream.Dispose();
                    fileStream = new FileStream($"{index}.txt", FileMode.Create, FileAccess.ReadWrite);
                    StreamWriter streamWriter = new StreamWriter(fileStream);
                    streamWriter.WriteLine("DataIsBreakenLOL");
                    streamWriter.Close();
                    fileStream.Close();
                }
                catch(FileNotFoundException) { FileBreaker(); }    
            }
            catch(Exception ex) { Console.WriteLine($"\nERROR FileBreaker (Data breaker): {ex}"); }

            index = random.Next(10, 30);
            try
            { 
                try
                {
                    File.Delete($"{index}.txt");
                }
                catch (FileNotFoundException) { FileBreaker(); }
            }
            catch (Exception ex) { Console.WriteLine($"\nERROR FileBreaker (File breaker): {ex}"); }
            Console.WriteLine($"\nFileBreaker Complited!!!");
        }

        static void FileHendler()
        {
            FileStream fileStream;
            int counter = 0;
            int sum = 0;
            try
            {
                Console.WriteLine('\n');
                for (int i = 10; i < 30; i++)
                {

                    try
                    {
                        fileStream = new FileStream($"{i}.txt", FileMode.Open, FileAccess.ReadWrite);
                        StreamReader streamReader = new StreamReader(fileStream);
                        int f_num = Int32.Parse(streamReader.ReadLine());
                        int s_num = Int32.Parse(streamReader.ReadLine());
                        int returns;
                        streamReader.Close();
                        fileStream.Close();
                        try
                        {
                            returns = f_num * s_num;
                            Console.WriteLine($"Mult {i}.txt => {f_num} * {s_num} = {returns}");
                            counter++;
                            sum += returns;
                        }
                        catch (OverflowException)
                        {
                            FileStream overflow = new FileStream($"overflow.txt", FileMode.OpenOrCreate, FileAccess.Write);
                            StreamWriter streamWriter = new StreamWriter(overflow);
                            streamWriter.WriteLine($"{i}.txt");
                            streamWriter.Close();
                            Console.WriteLine($"The multiply of file's {i}.txt is out of range Int32");
                        }
                        
                    }
                    catch (FileNotFoundException)
                    {
                        FileStream noFile = new FileStream($"no_file.txt", FileMode.OpenOrCreate, FileAccess.Write);
                        StreamWriter streamWriter = new StreamWriter(noFile);
                        streamWriter.WriteLine($"{i}.txt");
                        streamWriter.Close();
                        Console.WriteLine($"File {i}.txt doesn't exist");
                    }
                    catch (FormatException)
                    {
                        FileStream badData = new FileStream($"bad_data.txt", FileMode.OpenOrCreate, FileAccess.Write);
                        StreamWriter streamWriter = new StreamWriter(badData);
                        streamWriter.WriteLine($"{i}.txt");
                        streamWriter.Close();
                        badData.Close();
                        Console.WriteLine($"File {i}.txt is broken");
                       
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine($"\nERROR Multiply: {ex}"); }
            Console.WriteLine($"\nSum of numbers: {sum}");
            Console.WriteLine($"\n Avarage: {sum / counter}");

        }

        static void Main(string[] args)
        {
            Console.WriteLine("All files:\n");
            CheckAllFiles();

            FileCreator();
            Console.WriteLine("\nAll files:\n");
            CheckAllFiles();

            FileBreaker();
            Console.WriteLine("\nAll files:\n");
            CheckAllFiles();

            FileHendler();
            
            //FileDeleterAll();
            //Console.WriteLine("\nAll files:\n");
            //CheckAllFiles();
        }
    }
}
