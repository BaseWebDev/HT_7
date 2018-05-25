using System;
using System.Linq;

namespace CeasarCipher
{
    /// <summary>
    /// https://ru.wikipedia.org/wiki/%D0%A8%D0%B8%D1%84%D1%80_%D0%A6%D0%B5%D0%B7%D0%B0%D1%80%D1%8F
    /// </summary>
    class EncryptionCeasar
    {
        private int keyCipher;
        public enum Direction {
            Crypt,
            Decrypt
        }
        public byte[] MessageByte { get;set;}

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
            // Сложно
            //MessageByte = MessageByte.Select(x=>(byte)((Math.Abs(x + key)) % (byte.MaxValue + 1))).ToArray();
            byte[] buffer = new byte[MessageByte.Length];
            for (int i = 0; i < MessageByte.Length; i++) {
                buffer[i] = (byte)((Math.Abs(MessageByte[i] + key)) % (byte.MaxValue + 1));
            }
            MessageByte = buffer;
        }
    }
}