using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _372_project
{
    public class ComboboxKeyValuePair
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Value2 { get; set; }
        public string Command { get; set; }

        public ComboboxKeyValuePair(string key, string value, string command)
        {
            Key = key;
            Value = value;
            Value2 = "";
            Command = command;
        }

        public ComboboxKeyValuePair(string key, string value, string value2, string command)
        {
            Key = key;
            Value = value;
            Value2 = value2;
            Command = command;
        }

        public override string ToString()
        {
            return Key;
        }
    }
}
