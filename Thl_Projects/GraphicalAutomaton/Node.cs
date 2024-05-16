using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace GraphicalAutomaton
{
    public class Node
    {
        public Point? location;
        public int state;
        public string name;
        public Label statename;

        public Node()
        {
            name = "";
            state = -1;
        }
        public Node(Point p, int state)
        {
            this.location = p;
            this.state = state;
            name = "";
        }

        public Node(Point p, int state, string name) : this(p, state)
        {
            this.name = name;
            this.statename = new Label();
            this.statename.Text = name;

           
        }
        public void SetLabelLocation()
        {
            if (location.HasValue && statename != null)
            {
                statename.Location = new Point(location.Value.X - statename.Width / 2, location.Value.Y - statename.Height / 2);
            }
        }
    }
}
