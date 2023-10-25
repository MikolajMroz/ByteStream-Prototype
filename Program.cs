/*
  
!!! IMPORTANT !!!

Before running this program, make sure to change DOCXfilepath, JPGfilepath, and newFilePath
to filepaths that exist on your device.

The program also requires a JPG and DOCX file to exist in the specified directories, otherwise an Invalid Filepath error will appear.
A sample JPG and DOCX file have been provided.

This program takes in a JPG file and DOCX file, processes its raw data to determine if it matches with the structure of a JPG file, and outputs the results.
This is done by looking for the appropriate header and footer, as well as validating the existence of JPG Quantization Tables and the Huffman Marker,
Covering the semantic analysis requirement of this demonstration.

 */
public static class Globals
{
    public static byte[] byteArray;                                             //Stores raw binary data of the files
    public static string byteString;                                            //Stores HEX version of data
    public static string JPGfilePath = @"C:\Users\mikol\Desktop\test.jpg";      //Filepath to a JPG file
    public static string DOCXfilePath = @"C:\Users\mikol\Desktop\test.docx";    //Filepath to a DOCX file
    public static string newFilePath = @"C:\Users\mikol\Desktop";               //Output directory (will output NEWJPG.jpg)
}

internal class Program
{


    //Writes all bytes out to specified location. Throws error if the filepath is wrong. 
    static void Export(byte[] byteArray)
    {
        //Checks if the directory is valid...
        if (Directory.Exists(Globals.newFilePath)) {                                                    
            File.WriteAllBytes(Globals.newFilePath + @"\NEWJPG.jpg", Globals.byteArray);
            Console.WriteLine($@"[OK] File Reconstruction completed! Location: {Globals.newFilePath}\NEWJPG.jpg");
        } else
        {
            Console.WriteLine("[X] Directory does not exist.");
        }
    }

    //Locates and validates the JPG header (FF-D8-FF) in the HEX string.
    static bool FindJPGHeader(byte[] byteArray)
    {
        //Converts byte array into HEX to determine header and footer locations
        string byteString = BitConverter.ToString(byteArray);   
        int headerIndex;
        string header = "FF-D8-FF";

        //If the correct header is present, display its location.
        if (byteString.Contains(header))    
        {
            headerIndex = byteString.IndexOf(header);
            Console.WriteLine($"\n[OK] Header index ({header}) found at: location {headerIndex}");
            return true;
        }
        else
        {
            Console.WriteLine($"\n[X] Header value ({header}) not found in this file");
            return false;
        }
    }

    //Locates and validates the JPG trailer (FF-D9) in the HEX string.
    static bool FindJPGFooter(byte[] byteArray)
    {
        int trailerEndIndex;
        string byteString = BitConverter.ToString(byteArray);
        string trailer = "FF-D9";

        if (byteString.Contains(trailer))
        {
            trailerEndIndex = byteString.IndexOf(trailer);
            Console.WriteLine($"[OK] File end index ({trailer}) found at: location {trailerEndIndex}" + 4);
            return true;
        }
        else
        {
            Console.WriteLine($"[X] Trailer value ({trailer}) not found in this file");
            return false;
        }

    }

    //Locates and validates JPG Huffman Marker
    static bool FindHuffman(byte[] byteArray)
    {
        string byteString = BitConverter.ToString(byteArray);
        string huffmanMarker = "FF-C4";

        if (byteString.Contains(huffmanMarker))
        {
            int huffmanIndex = byteString.IndexOf(huffmanMarker);
            Console.WriteLine($"[OK] Huffman Marker ({huffmanMarker}) found at: location {huffmanIndex}");
            return true;
        }
        else
        {
            Console.WriteLine("[X] The string does not have a Huffman Marker.");
            return false;
        }
    }

    //Locates and validates JPG Quantization Table
    static bool FindQuantTable(byte[] byteArray) {
        string byteString = BitConverter.ToString(byteArray);
        string quantMarker = "FF-DB";

        if (byteString.Contains(quantMarker))
        {
            int quantIndex = byteString.IndexOf(quantMarker);
            Console.WriteLine($"[OK] Quantization Table index ({quantMarker}) found at: location {quantIndex}");
            return true;
        } else
        {
            Console.WriteLine("[X] The string does not have a Quantization Table.");
            return false;
        }
    }

    //Reads in the data from given files into byteArray then displays the first few chars.
    static void Import(string filepath)
    {
        Console.WriteLine($"file location received as {filepath}");
        Globals.byteArray = File.ReadAllBytes(filepath);

        Console.Write("RAW DATA: ");
        for (int i = 0; i < 10; i++)
        {
            Console.Write(Globals.byteArray[i]);
        }
        Console.Write("...");

        Console.Write("\nHEX DATA: ");
        string hexArray = BitConverter.ToString(Globals.byteArray);
        for (int i = 0; i < 14; i++)
        {
            Console.Write(hexArray[i]);
        }
        Console.Write("...");
    }

    //Validates the existence of the header, footer etc. and determines if this is a legitimate JPG file.
    static void verifyJPG(string Filepath)
    {
        if (File.Exists(Filepath))
        {
            Import(Filepath);

            bool JPGHeaderExists = FindJPGHeader(Globals.byteArray);
            bool JPGFooterExists = FindJPGFooter(Globals.byteArray);
            bool HuffmanMarkerExists = FindHuffman(Globals.byteArray);
            bool QuantTableExists = FindQuantTable(Globals.byteArray);

            if (JPGHeaderExists && JPGFooterExists && HuffmanMarkerExists && QuantTableExists) //If the file meets the criteria of a JPG, it is exported.
            {
                Console.WriteLine("[OK] This is a valid JPG file. Attempting export...");
                Export(Globals.byteArray);
            }
            else
            {
                Console.WriteLine("[X] This is not a valid JPG file. No data exported.");       //If any of the above boolean returns are false, it cannot be a JPG
            }                                                                                   //and is therefore not exported.
        }
        else { Console.WriteLine("Directory location invalid."); }
    }

    private static void Main(string[] args)
    {

        Console.WriteLine($"Press any key to validate a JPG file");
        Console.ReadKey();
        verifyJPG(Globals.JPGfilePath);
        Console.WriteLine($"\nPress any key to validate a DOCX file");
        Console.ReadKey();
        verifyJPG(Globals.DOCXfilePath);
        Console.Read();

    }
}
