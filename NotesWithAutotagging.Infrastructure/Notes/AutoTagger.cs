using System.Text.RegularExpressions;

namespace NotesWithAutotagging.Infrastructure.Notes
{
    public class AutoTagger
    {
        private readonly string regexEmail = "(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])";
        private readonly string regexPhone = "([+]?[\\s0-9]+)?(\\d{3}|[(]?[0-9]+[)])?([-]?[\\s]?[0-9])+$";

        public string Note { get; }

        public AutoTagger(string note)
        {
            Note = note;
        }

        public IEnumerable<string> TagNote()
        {
            var tags = new List<string>();
            if (Regex.IsMatch(Note, regexEmail, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(2)))
                tags.Add("EMAIL");
            if (Regex.IsMatch(Note, regexPhone, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(2)))
                tags.Add("PHONE");
            return tags;
        }
    }
}
