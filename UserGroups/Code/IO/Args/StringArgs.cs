using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UserGroups.Code.IO
{
    public class StringArgs : ArgsTemplate
    {
        public Regex StringArgument;
        public List<string> Args;
        public StringArgs(ArgsController controller) : base(controller)
        {
            StringArgument = new Regex("\".+?\"+");
        }

        public override void Handle() => Args = GetAllArguments();
        
        public string this[int index]
        {
            get
            {
                return ((Args.Count - 1) < index) ? "" : Args[index];
            }
        }

        private List<string> GetAllArguments()
        {
            try
            {
                MatchCollection matches = StringArgument.Matches(CONTROLLER.ROWArguments);
                List<string> array = new List<string>();

                for (int i = 0; i < matches.Count; i++)
                    array.Add(matches[i].Groups[0].Value.Replace("\"", ""));

                return array;
            }
            catch { return null; }
        }
        //Retrieving string argument by group index | no matches
    }
}
