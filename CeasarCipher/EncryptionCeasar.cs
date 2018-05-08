using System;

namespace CeasarCipher
{
    /// <summary>
    /// https://ru.wikipedia.org/wiki/%D0%A8%D0%B8%D1%84%D1%80_%D0%A6%D0%B5%D0%B7%D0%B0%D1%80%D1%8F
    /// </summary>
    class EncryptionCeasar
    {
        private string [] AlfaBets = { "ABCDEFGHIJKLMNOPQRSTUVWXYZ", "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ" };
        private int keyCipher;
        public enum Direction {
            Crypt,
            Decrypt
        }
        public string EditableString { get;set;}

        public Direction Cipher { get; set; }

        public int KeyCipher {
            private get {
                return keyCipher;
            }
            set {
                if (value == 0) {
                    throw new ArgumentNullException (@"Шифрование отсутствует!!!");
                } else if (value < 0) {
                    throw new ArgumentOutOfRangeException(@"Неверный ключ шифрования!");
                } else {
                    keyCipher = value;
                }
            }
        }

        public void StartMechanism() {
            int key=0;
            if (Cipher == Direction.Crypt) {
                key = keyCipher;
            } else if (Cipher == Direction.Decrypt) {
                key = -keyCipher;
            }
            string editableString = EditableString;
            string bufString = "";   
                for (int i = 0; i < editableString.Length; i++) {
                    char curSimbol = editableString[i];
                    if (char.IsLetter(editableString[i])) {
                        curSimbol = char.ToUpper(editableString[i]);
                        foreach (string alfaBet in AlfaBets) {
                            for (int j = 0; j < alfaBet.Length; j++) {
                                if (curSimbol == alfaBet[j]) {
                                    curSimbol = alfaBet[Math.Abs(j + key) % alfaBet.Length];
                                    break;
                                }
                            }
                        }
                        curSimbol = char.IsLower(editableString[i]) ? char.ToLower(curSimbol) : curSimbol;
                    }
                    bufString += curSimbol;
                }
            EditableString = bufString;
        }
    }
}