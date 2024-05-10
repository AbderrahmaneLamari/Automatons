using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GraphicalAutomaton
{
    public class Node
    {
        public Point? location;
        public int state;

        public Node()
        {
            
            state = -1;
        }
        public Node(Point p, int state)
        {
            this.location = p;
            this.state = state;
        }
    }
}
