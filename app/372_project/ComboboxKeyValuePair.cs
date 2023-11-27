using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _372_project
{
    class ComboboxKeyValuePair
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public ComboboxKeyValuePair(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return Key;
        }
    }
}
