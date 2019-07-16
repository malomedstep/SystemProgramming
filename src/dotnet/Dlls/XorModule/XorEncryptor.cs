using System;
using System.Linq;
using ContractDll;

namespace XorModule {
    [Serializable]
    public class XorEncryptor : IPlugin {
        public string Name => "XOR";

        public string Encode(string text, byte param) {
            var chars = text.ToArray();
            for (int i = 0; i < chars.Length; ++i) {
                chars[i] = (char)(chars[i] ^ param);
            }
            return new string(chars);
        }
    }
}
