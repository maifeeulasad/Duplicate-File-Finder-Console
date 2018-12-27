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


      


        static Dictionary<String, List<String>> hashes = new Dictionary<string, List<String>> { };


        static void Main(String[] args)
        {


            //Console.ForegroundColor(new ConsoleColor(10));

            travarse("D:\\");

            for(int i=0;i<3;i++)
                Console.Beep();




            foreach (KeyValuePair<String, List<String> > kvp in hashes)
            {
                Console.WriteLine("\n" + kvp.Key + "\n");

                List<String> locations = kvp.Value;

                foreach(String location in locations)
                {

                    Console.WriteLine("          -------------   " + location);

                }




            }

            for (int i = 0; i < 3; i++)
                Console.Beep();



            Console.ReadKey();

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
