using System;
using System.Security;

namespace CeasarCipher
{

   
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t Шифр Цезаря");
            EncryptionCeasar encrypt = new EncryptionCeasar();
            Console.Write("Введите ключ шифрования (целое число): ");  // Может быть отриц и полож, если 0 - отсут. шифрование
            try {
                if (int.TryParse(Console.ReadLine(), out int keyCipher)) {
                    encrypt.KeyCipher = keyCipher;
                } else {
                    throw new Exception(@"Некорректный ввод!");
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Введит выражение для шифрования:");
            encrypt.OpenString = Console.ReadLine();
            Console.WriteLine("Зашифрованное выражение: ");
            Console.WriteLine(encrypt.CloseString);
            Console.WriteLine("И снова расшифруем: ");
            encrypt.CloseString = encrypt.CloseString;
            Console.WriteLine(encrypt.OpenString);

        }
    }
}
