using System;

namespace CeasarCipher
{ 
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t Шифр Цезаря");
            EncryptionCeasar encrypt = new EncryptionCeasar();
            Console.Write("Введите ключ шифрования (целое положительное число): ");  // Может быть отриц и полож, если 0 - отсут. шифрование
            try {
                if (int.TryParse(Console.ReadLine(), out int keyCipher)) {
                    encrypt.KeyCipher = keyCipher;
                } else {
                    throw new ArgumentException(@"Некорректный ввод!");
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Введите выражение для шифрования:");
            encrypt.EditableString = Console.ReadLine();
            encrypt.Cipher = EncryptionCeasar.Direction.Crypt;
            encrypt.StartMechanism();
            Console.WriteLine("Зашифрованное выражение: ");
            Console.WriteLine(encrypt.EditableString);
            Console.WriteLine("И расшифруем: ");
            encrypt.Cipher = EncryptionCeasar.Direction.Decrypt;
            encrypt.StartMechanism();
            Console.WriteLine(encrypt.EditableString);
        }
    }
}
