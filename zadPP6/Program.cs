using System;
using System.Security.Cryptography;

class PasswordGenerator
{
    static void Main()
    {
        Console.WriteLine("Введите длину пароля:");
        int passwordLength;
        while (!int.TryParse(Console.ReadLine(), out passwordLength) || passwordLength <= 0)
        {
            Console.WriteLine("Пожалуйста, введите положительное целое число для длины пароля:");
        }

        string password = GeneratePassword(passwordLength);
        Console.WriteLine($"Сгенерированный пароль: {password}");
    }

    static string GeneratePassword(int length)
    {
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+-=[]{}|;:,.<>?";
        byte[] randomBytes = new byte[length * 4]; // каждый символ пароля занимает 4 байта
        char[] password = new char[length];

        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(randomBytes);

            for (int i = 0; i < length; i++)
            {
                uint randomInt = BitConverter.ToUInt32(randomBytes, i * 4);
                password[i] = validChars[(int)(randomInt % validChars.Length)];
            }
        }

        return new string(password);
    }
}
