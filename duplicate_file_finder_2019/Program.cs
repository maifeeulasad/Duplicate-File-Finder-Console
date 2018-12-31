using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace duplicate_file_finder_2019
{
    class Program

    {

        public static string myCurrentKey = "";




        static Dictionary<String, List<String>> hashes = new Dictionary<string, List<String>> { };


        static void Main(String[] args)
        {


            //Console.ForegroundColor(new ConsoleColor(10));

            travarse("D:\\models");

            for(int i=0;i<3;i++)
                Console.Beep();




            foreach (KeyValuePair<String, List<String> > kvp in hashes)
            {
                
                
                if(kvp.Value.Count>1)
                {

                    List<String> locations = kvp.Value;
                    Console.WriteLine("\n" + kvp.Key + "\n");

                    foreach (String location in locations)
                    {

                        Console.WriteLine("          -------------   " + location);

                    }
                }




            }

            for (int i = 0; i < 3; i++)
                Console.Beep();



            Console.ReadKey();
            //Console.Clear();




            myCurrentKey = hashes.Keys.First();

            ConsoleKeyInfo cki;
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.LeftArrow)
                {
                    MoveIndex(-1);

                    //Console.Clear();

                    List<String> data = hashes[myCurrentKey];

                    Console.WriteLine("---------------------------------");

                    foreach (String datum in data)
                    {
                        Console.WriteLine(datum);
                    }

                    Console.WriteLine("---------------------------------");

                    //Console.ReadKey();
                }
                else if (cki.Key == ConsoleKey.RightArrow)
                {
                    MoveIndex(1);
                    //Console.Clear();
                    Console.WriteLine("---------------------------------");

                    List<String> data = hashes[myCurrentKey];


                    foreach (String datum in data)
                    {
                        Console.WriteLine(datum);
                    }

                    Console.WriteLine("---------------------------------");

                    //Console.ReadKey();
                }
            }


            



        }


        private static void MoveIndex(int dir)
        { 

            List<string> keys = new List<string>(hashes.Keys);
            int newIndex = keys.IndexOf(myCurrentKey) - dir;
            if (newIndex < 0)
            {
                newIndex = hashes.Count - 1;
            }
            else if (newIndex > hashes.Count - 1)
            {
                newIndex = 0;
            }

            myCurrentKey = keys[newIndex];
        }




        static void travarse(String workingDirectory)
        {
            try
            {

                String[] files = Directory.GetFiles(workingDirectory);
                String[] subDirectories = Directory.GetDirectories(workingDirectory);


                foreach(String file in files)
                {
                    Console.WriteLine(" -------------------- " + file);

                    //Console.WriteLine("              " + CalculateMD5(file)+"\n\n");

                    CalculateMD5(file);


                }

        
                foreach (String subDirectory in subDirectories)
                {

                    

                    Console.WriteLine(" +++++++++ " + subDirectory);

                    travarse(subDirectory);
                }
            }
            catch(UnauthorizedAccessException uae)
            {
                Console.WriteLine("Unauthorized Access --- " + workingDirectory);
            }
            
            
        }


        static String CalculateMD5(String filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);


                    //Console.Beep();



                    String hashString = BitConverter.ToString(hash);

                    
                    try
                    {
                        hashes[hashString].Add(filename);


                    }
                    catch(NullReferenceException nre)
                    {
                        hashes[hashString] = new List<String> { };
                        hashes[hashString].Add(filename);
                    }
                    catch(KeyNotFoundException knfe)
                    {

                        hashes[hashString] = new List<String> { };
                        hashes[hashString].Add(filename);
                    }

                    return hashString;


                }
            }
        }


        



    }
}
