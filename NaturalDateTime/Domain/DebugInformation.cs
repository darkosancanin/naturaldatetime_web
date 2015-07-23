using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalDateTime.Domain
{
    public class DebugInformation
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public DebugInformation(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
