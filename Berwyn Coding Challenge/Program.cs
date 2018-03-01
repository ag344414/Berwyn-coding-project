using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Berwyn_Coding_Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            //Getting the Filename
            Console.WriteLine("Enter the name of your file.");
            string fileName;
            string output = "output.txt";
            string currentLine;
            fileName = Console.ReadLine();
            //Opening the file
            /*if(File.Exists(output)){
                File.Delete(output);
            }
            File.Create(output);*/
            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            var file = new System.IO.StreamReader(fileStream, System.Text.Encoding.UTF8, true, 128);
            FileStream fileStream2 = new FileStream(output,FileMode.Append, FileAccess.Write);
            var outfile = new StreamWriter(fileStream2, System.Text.Encoding.UTF8, 128, true);
            //string currentLine;
            //Get rid of 1st line
            currentLine = file.ReadLine();

            outfile.WriteLine("GUID, Val1+Val2, isDuplicateGuid(Y or N), Is Val3 length greater than the average length of Val3 (Y or N)");
            



            //Lists

            List<string> Guids = new List<string>(); 
            List<int> Sum = new List<int>(); 
            List<string> Val3 = new List<string>();
            List<string> Dups = new List<string>();
            List<string> isDup = new List<string>();
            List<string> isLarger = new List<string>();

            //Variables for output
            float numFiles = 0;
            int largestVal = 0;
            string largestGuid = "";
            float totalVal3length = 0;

            while ((currentLine = file.ReadLine()) != null)
            {
                //Remove unwanted characters
                var charsToRemove = new string[] {" ","\""};
                foreach (var c in charsToRemove)
                {
                    currentLine = currentLine.Replace(c, string.Empty);
                }
                //Split on commas
                char[] delimiterChars = {','};
                string[] words = currentLine.Split(delimiterChars);

                int x = Int32.Parse(words[1]);
                int y = Int32.Parse(words[2]);
                int sum = x + y;
                Sum.Add(sum);

                //outfile.Write(words[0] + ", " + sum + ", ");

                if (sum > largestVal)
                {
                    largestVal = sum;
                    largestGuid = words[0];
                }
                if (Guids.Contains(words[0])){
                    Dups.Add(words[0]);
                    isDup.Add("Y");
                    //outfile.Write("Y, ");
                }
                else
                {
                    Guids.Add(words[0]);
                    isDup.Add("N");
                    //outfile.Write("N, ");
                }
                Sum.Add(sum);
                Val3.Add(words[3]);
                totalVal3length = totalVal3length + words[3].ToCharArray().Length - 1;
               // Console.WriteLine(words[0] + ", " + sum );
                numFiles = numFiles + 1;
               // outfile.WriteLine();
            }

            float averageLength = totalVal3length / numFiles;

            for (int i=0; i<Val3.Count; i++)
            {
                if(Val3[i].ToCharArray().Length - 1 > averageLength)
                {
                    isLarger.Add("Y");
                }
                else
                {
                    isLarger.Add("N");
                }
            }
            for(int i=0; i<Guids.Count; i++)
            {
                outfile.WriteLine(Guids[i] + ", " + Sum[i] + ", " + isDup[i] + ", " + isLarger[i]);
            }
            outfile.Close();

            Console.WriteLine("Total number of files in record are: " + numFiles);
            Console.WriteLine("Largest sum of Val1 + Val2 is " + largestVal + " at GUID " + largestGuid);
            Console.WriteLine("Duplicated GUIDs are: ");
            Dups.ForEach(Console.WriteLine);
            Console.WriteLine("Average length of Val3 is " + averageLength);
        }
    }
}
