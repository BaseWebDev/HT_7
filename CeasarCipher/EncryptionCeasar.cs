using System;
using System.Security;

namespace CeasarCipher
{
    /// <summary>
    /// https://ru.wikipedia.org/wiki/%D0%A8%D0%B8%D1%84%D1%80_%D0%A6%D0%B5%D0%B7%D0%B0%D1%80%D1%8F
    /// </summary>
    class EncryptionCeasar
    {
        private string [] AlfaBets = { "ABCDEFGHIJKLMNOPQRSTUVWXYZ", "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ" };
        private string openString;
        private string closeString;
        private int keyCipher;

        public string OpenString {
            get {
                return openString;
            }
            set {
                openString = value;
                Crypt();
            }
        }
        public string CloseString {
            get {
                return closeString;
            }
            set {
                closeString = value;
                Decrypt();
            }
        }

        public int KeyCipher {
            private get {
                return keyCipher;
            }
            set {
                if (value == 0) {
                    throw new Exception (@"Шифрование отсутствует!!!");
                } else if (value < 0) {
                    throw new Exception(@"Неверный ключ шифрования!");
                } else {
                    keyCipher = value;
                }
            }
        }

        private void Crypt() {
            string bufString = "";   
                for (int i = 0; i < openString.Length; i++) {
                    char curSimbol = openString[i];
                    if (char.IsLetter(openString[i])) {
                        curSimbol = char.ToUpper(openString[i]);
                        foreach (string alfaBet in AlfaBets) {
                            for (int j = 0; j < alfaBet.Length; j++) {
                                if (curSimbol == alfaBet[j]) {
                                    curSimbol = alfaBet[(j + keyCipher) % alfaBet.Length];
                                    break;
                                }
                            }
                        }
                        curSimbol = char.IsLower(openString[i]) ? char.ToLower(curSimbol) : curSimbol;
                    }
                    bufString += curSimbol;
                }            
            openString = String.Empty;
            closeString = bufString;
        }

        private void Decrypt() {
            string bufString = "";
            for (int i = 0; i < closeString.Length; i++) {
                char curSimbol = closeString[i];
                if (char.IsLetter(closeString[i])) {
                    curSimbol = char.ToUpper(closeString[i]);
                    foreach (string alfaBet in AlfaBets) {
                        for (int j = 0; j < alfaBet.Length; j++) {
                            if (curSimbol == alfaBet[j]) {
                                curSimbol = alfaBet[(j - keyCipher + alfaBet.Length) % alfaBet.Length];
                                break;
                            }
                        }
                    }
                    curSimbol = char.IsLower(closeString[i]) ? char.ToLower(curSimbol) : curSimbol;
                }
                bufString += curSimbol;
            }
            closeString = String.Empty;
            openString = bufString;
        }

    }
}