using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProjectIS
{
    internal class Program
    { 
     static string part1 , part2 ;
        static void Main(string[] args)
        {

            // input from user name
            Console.Write("please enter your Name : ");
            string plainText = Console.ReadLine();

            // input key 
            Console.Write("please enter your key :");
            string Key = Console.ReadLine();

            //Convert Plaintext To Binary
            string binarytext = ConvertPlaintextToBinary(plainText);

            // generate the new key 
            string Binary_key = ConvertPlaintextToBinary(Key);
            string NewKey = Generation_New_Key(Binary_key);
            Console.WriteLine("\nThe generated key : " + NewKey);
            Console.WriteLine();


            // Encyption 

            string CipherText = Encryption(binarytext, NewKey);
            string cipherText2 = ConvertBinaryToPlaintext(CipherText); 
            Console.WriteLine("\nThe Encrypted name : " + CipherText);
            Console.WriteLine();

            // Decryption
            string DecryptText = Decryption(CipherText, NewKey);
            string DecryptText2 = ConvertBinaryToPlaintext(DecryptText);

            Console.WriteLine("\nThe Decrypted name : " + DecryptText2);
            Console.WriteLine();

            Console.ReadKey();

        } // End main 

        static string ConvertPlaintextToBinary(string plaintext)
        {
            string binarytext = "";
            foreach (char c in plaintext)
            {
                binarytext += Convert.ToString(c, 2).PadLeft(8, '0');
            }
            return binarytext;
        }

        static void SplitBinaryText(string binarytext, out string part1, out string part2)
        {
            int length = binarytext.Length / 2;
            part1 = binarytext.Substring(0, length);
            part2 = binarytext.Substring(length);
        }

        static string Shift_Left(string binarytext)
        {
            return binarytext.Substring(2) + binarytext.Substring(0, 2);
        }

        static string CombineBinaryText(string part1, string part2)
        {
            return part1 + part2;
        }

        static string XOR(string Text, string key)
        {
            StringBuilder xorText = new StringBuilder();

            for (int i = 0; i < Text.Length; i++)
                xorText.Append((char)(Text[i] ^ key[(i % key.Length)]));

            String result = xorText.ToString();

            return result;
        }

        static string Shift_Right(string binarytext)
        {
            return binarytext.Substring(binarytext.Length - 2) + binarytext.Substring(0, binarytext.Length - 2);
        }

        static string PBox(string text)
        {
            // P-box value ( key ) 
            int[] pbox = { 18,  23, 24, 25, 26, 27,  32, 33, 41, 42, 43, 44, 45,34, 46, 47, 28, 29, 30, 31,48,     // 64 bit 
                           49, 50, 51, 52, 53,19, 20, 21, 22, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63,0, 1, 2,
                           3,4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17,35, 36, 37, 38, 39, 40 };

            // convert text to character
            char[] Text = text.ToCharArray();                                                          // 64 bit = 8 character

            char[] NewArr = new char[pbox.Length];                                                      // 64 bit 

            for (int i = 0; i < pbox.Length; i++)
            {
                NewArr[i] = Text[pbox[i]];
            }

            string combineText = "";
            foreach (var item in NewArr)
            {
                combineText += item;
            }

            return combineText;
        }

        static string ConvertBinaryToPlaintext(string binarytext)
        {
            string plaintext = "";
            for (int i = 0; i < binarytext.Length; i += 8)
            {
                string byteBinary = binarytext.Substring(i, 8);
                char c = (char)Convert.ToInt32(byteBinary, 2);
                plaintext += c;
            }
            return plaintext;
        }

        
        // ------------------------------

        static string Encryption(String plainText, string New_key)
        {
            // Step 1 : split the plain text 
            SplitBinaryText(plainText,out part1, out part2);

            // Step 2 : shift left by (2) for each part
            string Part1_Shift = Shift_Left(part1);
            string Part2_Shift = Shift_Left(part2);

            // Step 3 : combine the two parts togethor
            string full_parts = CombineBinaryText(Part1_Shift,Part2_Shift);

            // step 4 : XOR full_parts with new key 
            string XOR_text = XOR (full_parts ,New_key) ;

            // step 5 : P-box 
            string CipherText = PBox(XOR_text);


            return CipherText;
        }

        static string Decryption(string cipherText, string New_key)
        {
            // step 1 : go to p-box ( out. XOR )
            string Step1_XOR = PBox(cipherText);

            // step 2 : full_parts XOR New_key  (out. full_text )
            string step2_full_parts = XOR(Step1_XOR,New_key) ;

            //step 3 : split step2_full_parts to part1 and part2
            SplitBinaryText(step2_full_parts, out part1, out part2);

            // step 4 : shift right by ( 2 ) to each part  
            string  part1_shift = Shift_Right(part1);
            string part2_shift = Shift_Right(part2);

            // step 5 : combine the parts 
            string Dec_plainText = CombineBinaryText(part1_shift, part2_shift);

            return Dec_plainText;

        }

        static string Generation_New_Key(string key)
        {
            string Key_1 = Shift_Left(key);
            string NewKey = PBox(Key_1);

            return NewKey;
        }

    } // End class
}// End nameSpace 



