using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace CeasarCipher
{ 
    class Program
    {
        const string defFileName = "message.bin";
        static void Main(string[] args)
        {
           if (args.Length == 0) {   // Режим без параметров командной строки
                int keyCipher;
                Console.WriteLine("\t Шифр Цезаря");
                EncryptionCeasar encrypt = new EncryptionCeasar();
                Console.Write("Введите ключ шифрования (целое положительное число): ");  // Может быть отриц и полож, если 0 - отсут. шифрование
                try {
                    if (int.TryParse(Console.ReadLine(), out keyCipher)) {
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
                Console.WriteLine("Строка сохранена в файле: " + defFileName);
                SaveFile(defFileName,encrypt.EditableString);

                Console.WriteLine("И расшифруем: ");
                var tempEncrypt = new EncryptionCeasar {
                    EditableString = OpenFile(defFileName),
                    KeyCipher = keyCipher,
                    Cipher = EncryptionCeasar.Direction.Decrypt
                };
                tempEncrypt.StartMechanism();
                        Console.WriteLine(tempEncrypt.EditableString);
            } else { // C параметрами командной строки
                int keyCipher;
                Console.WriteLine("\t Режим пакетной обработки файла");
                if (args[0] == "?") { Console.WriteLine("[файл] [decrypt|сrypt] [ключ]" ); return; } // Справка по параметрам командной строки
                try {
                    var file = new FileInfo(args[0]);
                    if (file.Exists && (args[1]=="decrypt"|| args[1] == "crypt") && int.TryParse(args[2], out keyCipher)) {       
                        var tempEncrypt = new EncryptionCeasar {
                            EditableString = OpenFile(file.FullName),
                            KeyCipher = keyCipher,
                            Cipher = (args[1] == "decrypt") ? EncryptionCeasar.Direction.Decrypt: EncryptionCeasar.Direction.Crypt
                        };
                        tempEncrypt.StartMechanism();
                        SaveFile(file.FullName, tempEncrypt.EditableString); // Перезаписываем файл
                        Console.WriteLine("Файл: {0} обработан", file.FullName);
                    } else {
                        throw new ArgumentException(@"Некорректныe параметры командной строки! Для справки введите ""?""");
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                    return;
                }

            }
        }

        static void SaveFile(string fileName, string message) {
            using (var fs = new FileStream(fileName, FileMode.Create)) {
                var data = Encoding.Default.GetBytes(message);
                fs.Write(data, 0, data.Length);
            }
        }
        static string OpenFile(string fileName) {
            using (var fs = new FileStream(fileName, FileMode.Open)) {
                var data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
                return Encoding.Default.GetString(data);
            }          
        }
    }
}
