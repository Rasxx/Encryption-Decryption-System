using System;
using System.Text.RegularExpressions;

class Program
{
    private static int[] PBoxMapping = { 2, 1, 4, 3, 6, 5, 8, 7 };

    private static string ConvertPlaintextToBinary(string text)
    {
        string binary = "";
        foreach (char c in text)
        {
            binary += Convert.ToString(c, 2).PadLeft(8, '0');
        }
        return binary;
    }

    private static string ConvertBinaryToPlaintext(string binary)
    {
        string text = "";
        for (int i = 0; i < binary.Length; i += 8)
        {
            string binaryByte = binary.Substring(i, 8);
            int asciiValue = Convert.ToInt32(binaryByte, 2);
            text += (char)asciiValue;
        }
        return text;
    }

    private static string Shift(string input, int shiftAmount)
    {
        if (shiftAmount > input.Length)
        {
            shiftAmount = shiftAmount % input.Length;
        }
        return input.Substring(shiftAmount) + input.Substring(0, shiftAmount);
    }

    private static string XOR(string a, string b)
    {
        string result = "";
        for (int i = 0; i < a.Length; i++)
        {
            result += a[i] == b[i] ? '0' : '1';
        }
        return result;
    }

    private static string EncryptPlainText(string plaintext, string key)
    {
        // convert the Plaintext To Binary and convert the Key to Binary
        string binaryPlaintext = ConvertPlaintextToBinary(plaintext);
        string binaryKey = ConvertPlaintextToBinary(key);

        // Generate a new key by shifting the key left by 2 and applying the P-box cipher
        string shiftedKey = Shift(binaryKey, 2);
        string newKey = pbox(shiftedKey);

        // Split binary plaintext into two parts
        string part1 = binaryPlaintext.Substring(0, binaryPlaintext.Length / 2);
        string part2 = binaryPlaintext.Substring(binaryPlaintext.Length / 2);

        // Shift each part left by 2
        part1 = Shift(part1, 2);
        part2 = Shift(part2, 2);

        // Combine the shifted parts into a single string
        string combinedParts = part1 + part2;

        // XOR the combinedParts with the newkey
        string ciphertext = XOR(combinedParts, newKey);

        return ciphertext;
    }

    private static string Decryptciphertext(string ciphertext, string key)
    {    // Convert the the Key to Binary
        string binaryKey = ConvertPlaintextToBinary(key);

        // Generate a new key by shifting the key left by 2 and applying the P-box cipher
        string shiftedKey = Shift(binaryKey, 2);
        string newKey = pbox(shiftedKey);

        // XOR the ciphertext with the new key
        string decryptedText = XOR(ciphertext, newKey);

        // Split the decrypted binary text into two parts
        string part1 = decryptedText.Substring(0, decryptedText.Length / 2);
        string part2 = decryptedText.Substring(decryptedText.Length / 2);

        // Shift each part right by 2
        part1 = Shift(part1, decryptedText.Length - 2);
        part2 = Shift(part2, decryptedText.Length - 2);

        // Combine the shifted parts into a single string
        string combinedParts = part1 + part2;

        // Convert the combined string to text
        string plaintext = ConvertBinaryToPlaintext(combinedParts);

        return plaintext;
    }

    private static string pbox(string input)
    {
        string output = "";
        for (int i = 0; i < input.Length; i++)
        {
            int newIndex = PBoxMapping[i % PBoxMapping.Length] - 1;
            output += input[newIndex];
        }
        return output;
    }

    static void Main(string[] args)
    {
        bool continueLoop = true;
        while (continueLoop)
        {
            Console.Write("Enter your name (or type 'stop' to exit): ");
            string plaintext = Console.ReadLine();
            if (plaintext.ToLower() == "stop")
            {
                continueLoop = false;

                break;
            }

            Console.Write("Enter your key: ");
            string key = Console.ReadLine();

            string binaryKey = ConvertPlaintextToBinary(key);
            string shiftedKey = Shift(binaryKey, 2);
            string newKey = pbox(shiftedKey);

            //Console.WriteLine("The generated key: " + newKey);

            string ciphertext = EncryptPlainText(plaintext, key);
            string decryptedText = Decryptciphertext(ciphertext, key);

            Console.WriteLine("\nThe encrypted ciphertext is: " + ConvertBinaryToPlaintext(ciphertext));
            Console.WriteLine("\nThe decrypted plaintext is: " + decryptedText);
        }

        Console.WriteLine("Exiting the program.");
        Console.ReadKey();
    }
}