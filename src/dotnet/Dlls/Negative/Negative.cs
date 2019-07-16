using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContractDll;

namespace Negative {
    public class Negative : IPlugin {
        public string Name => "Neg";

        public string Encode(string text, byte param) {
            var chars = text.ToArray();
            for (int i = 0; i < chars.Length; ++i) {
                chars[i] = (char)~chars[i];
            }
            return new string(chars);
        }
    }
}
