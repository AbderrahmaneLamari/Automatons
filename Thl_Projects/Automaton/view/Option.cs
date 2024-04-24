using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.view
{
    class Option
    {
        private string optionContent;
        private int optionNumber;

        public string OptionContent
        {
            get { return optionContent; }
            set {

                if (value != null || value == "")
                {
                    optionContent = value;
                }
            }
        }

        public int OptionNumber {
            get { return optionNumber; }
            set { optionNumber = (value < 0) ? 0 : value ; }
            }

        public Option()
        {
            // Identity Constructor
        }

        public Option(string content, int number) : this()
        {
            this.optionNumber = number;
            this.optionContent = content;

        }
    }
}
