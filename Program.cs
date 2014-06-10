using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


class Program
{
    static void Main(string[] args)
    {
        // Convert binary to gainspan hex
        Console.WriteLine("Converts binary or Base64-encoded file to colon-separated hex format");
        Console.WriteLine("Use it to convert EAP certificates to ASCII format to import it to Gainspan");
            
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: bin2hex.exe <input file> <output file>");
            return;
        }

        string strSrc = args[0];
        string strDst = args[1];

        if (File.Exists(strSrc))
        {
            Console.WriteLine("Input file does not exist");
            return;
        }

        // Maybe it's base64 encoded certificate?
        try
        {
            Console.Write("Checking base64 format..");
            string strIn = File.ReadAllText(strSrc);
            byte[] byDataBase54 = Convert.FromBase64String(strIn);
            if (byDataBase54.Length > 0)
            {
                // Apaprently we got avalid base64 string
                Console.WriteLine("..Ok");
                string strDataBase64 = BitConverter.ToString(byDataBase54).Replace("-", ":");
                File.WriteAllText(strDst, strDataBase64);
                Console.WriteLine("Converted. Size: " + strDataBase64.Length.ToString());

                return;
            }
        }
        catch (Exception)
        {
            Console.WriteLine("..Failed");
        }

        // It's not base64, assume binary format
        Console.WriteLine("Doesn't look like base64, assume its binary");
        byte[] byDataBinary = File.ReadAllBytes(strSrc);
        //string strDataBinary = BitConverter.ToString(byDataBinary).Replace("-", ":");
        string strDataBinary = BitConverter.ToString(byDataBinary).Replace("-", "");
        File.WriteAllText(strDst, strDataBinary);
        Console.WriteLine("Converted. Size: " + strDataBinary.Length.ToString());
    }
}
