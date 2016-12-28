using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeScraper
{
    public class Parameter
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public Parameter(string Key, string Value)
        {
            this.Key = Key;
            this.Value = Value;
        }
    }
}
