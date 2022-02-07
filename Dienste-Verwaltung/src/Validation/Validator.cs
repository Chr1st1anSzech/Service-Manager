using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dienste_Verwaltung.src.Validation
{
    public class Validator
    {
        public static readonly string OnlyCharsAndDash = "^[a-zA-Z0-9]([a-zA-Z0-9_\\- ]*[a-zA-Z0-9]+)?$";

        private readonly string pattern;
        private readonly string[] forbiddenInputs;

        public Validator(string pattern, string[] forbiddenInputs)
        {
            this.pattern = pattern;
            this.forbiddenInputs = forbiddenInputs;
        }

        public bool Validate(string inputText) => 
            Regex.IsMatch(inputText, pattern) && !forbiddenInputs.Contains(inputText);
        
    }
}
