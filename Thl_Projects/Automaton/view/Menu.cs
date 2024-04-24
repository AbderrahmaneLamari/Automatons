using System;
using System.Collections.Generic;


namespace Compiler.view
{
    class Menu
    {
        private List<Option> options;
        public List<Option> Options
        {
            get { return options; }
        }

       public  Menu()
        {
            options = new List<Option>();
        }

        public Menu(List<Option> optionList)
        {
            this.options = optionList;
        }

        public void AddOption(Option op)
        {
            this.options.Add(op);
        }
        public void AddOption(string content, int number)
        {
            this.options.Add(new Option(content, number));
        }

        
    }
}
