using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UserGroups.Code.IO
{
    public class NumberArgs : ArgsTemplate
    {
        public Regex NumberArgument;
        public List<double> IntArgs;
        public NumberArgs(ArgsController controller) : base(controller)
        {
            NumberArgument = new Regex("(-?\\d+(?:\\.\\d+)?)|(\\\"-?\\d+(?:\\.\\d+)?\\\")");
        }

        public override void Handle() => IntArgs = GetAllNumbersArguments();

        public double this[int index]
        {
            get
            {
                return (!HasNumbers() && (IntArgs.Count - 1) < index) ? 0 : IntArgs[index];
            }
        }

        public bool HasNumbers() => IntArgs.Count > 0;
        //Retrieving all numeric arguments
        public List<double> GetAllNumbersArguments()
        {
            try
            {
                MatchCollection matches = NumberArgument.Matches(CONTROLLER.ROWArguments);
                List<double> array = new List<double>();

                for (int i = 0; i < matches.Count; i++)
                {
                    if (matches[i].Groups[0].Value.Contains("\""))
                        //throw new NumberArgumentException("Unknown symbol '\"'");
                        continue;
                    array.Add(double.Parse(matches[i].Groups[0].Value.Replace(".", ",")));
                }

                return array;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); return null; }
        }
    }
}
