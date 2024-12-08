using System;
using System.IO;

namespace PasswordManagerDotNet
{
    class Program
    {
        Random random = new Random();
        static void Main(string[] args)
        {
            //Console.WriteLine($"Hello World!, this is password generator\npassword is {PasswordGenerator(8)}");
            Console.WriteLine($"Hi!, this is password generator\nEnter strength: ");
            int _strength = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Hi!, this is password generator\nYour password is: {PasswordGenerator(_strength)}");
        }
        static string RandomSmall()
        {
            Random random = new Random();
            string[] smallLetters = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            return (smallLetters[random.Next(smallLetters.Length)]);
        }
        static string RandomCapital()
        {
            Random random = new Random();
            string[] capitalLetters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            return (capitalLetters[random.Next(capitalLetters.Length)]);
        }
        static string RandomSpecial()
        {
            Random random = new Random();
            string[] specialCharacters = new string[] { "!", "@", "#", "$", "%", "^", "&", "*" };
            return (specialCharacters[random.Next(specialCharacters.Length)]);
        }
        static string RandomNumbers()
        {
            Random random = new Random();
            string[] numeralCharacters = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            return (numeralCharacters[random.Next(numeralCharacters.Length)]);
        }
        static string RandomCharacterGenerator(int randomKey)
        {
            switch (randomKey)
            {
                case 0:
                    return (RandomSmall());
                    break;
                case 1:
                    return (RandomCapital());
                    break;
                case 2:
                    return (RandomSpecial());
                    break;
                default:
                    return (RandomNumbers());
                    break;
            }
        }
        static string PasswordGenerator(int passwordStrength)
        {
            Random random = new Random();
            int i = 0;
            string password = "";
            while (i < passwordStrength)
            {
                password += RandomCharacterGenerator(random.Next(4));
                i++;
            }
            return (password);
        }
    }
}
