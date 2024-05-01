using System.Text.RegularExpressions;
using UnityEngine;

namespace Protobot.UI.Forms {
    public class EmailTextValidator : MonoBehaviour, ITextValidator {
        public bool IsValid(string text) {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(text);
        }

        [SerializeField] private string invalidMessage;
        public string InvalidMessage => invalidMessage;
    }
}
