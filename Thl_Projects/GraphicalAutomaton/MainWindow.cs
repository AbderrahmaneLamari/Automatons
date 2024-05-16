using System.Windows.Forms;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using RecognitionSystems;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;



namespace GraphicalAutomaton
{
    public partial class MainWindow : Form
    {

        private FiniteStateMachine automaton;
        private List<Transition> tList;
        private int _initialState;
        private List<int> _states;
        private List<int> _finalStates;
        private List<string> _alphabet;

        readonly float nodeRadius = 25;
        private Node selectedNode = new Node();
        private Point? mouseDownLocation = null;
        private List<Node> nodes = new List<Node>();
        private List<Tuple<Node, Node>> arcs = new List<Tuple<Node, Node>>();
        private const int OFFSET = 5;

        public MainWindow()
        {
            InitializeComponent();
            initializeCanvas();

            
        }

        private void initializeCanvas()
        {
            canvas.BackColor = Color.White;
            canvas.BorderStyle = BorderStyle.FixedSingle;
            canvas.MouseDown += canvas_MouseDown;
            canvas.MouseMove += canvas_MouseMove;
            canvas.MouseUp += canvas_MouseUp;
            canvas.Paint += canvas_Paint;

        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            
            selectedNode = FindNode(e.Location);
            mouseDownLocation = e.Location;
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
           
            if (selectedNode != null && mouseDownLocation.HasValue)
            {
                // Move the selected node
                int deltaX = Convert.ToInt32(e.X - mouseDownLocation.Value.X);
                int deltaY = Convert.ToInt32(e.Y - mouseDownLocation.Value.Y);
                int nodeIndex = nodes.IndexOf(selectedNode);
                if(-1 != nodeIndex)
                {
                    nodes[nodeIndex].location = new Point(selectedNode.location.Value.X + deltaX, selectedNode.location.Value.Y + deltaY);
                    mouseDownLocation = e.Location;
                    canvas.Invalidate();
                }
                
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            // selcted node is always null.
            selectedNode = null;
            mouseDownLocation = null;
            
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            float loopSize = 2.5f * nodeRadius;
            SizeF textSize;
            PointF nameposition;
            string name;
            foreach (var arc in arcs)
            {
                Node start_node = arc.Item1, end_node = arc.Item2;
                Point start, end;

                if (start_node.state == end_node.state)
                {
                    
                    PointF[] loopPoints = {
                        new PointF(start_node.location.Value.X, start_node.location.Value.Y),
                        new PointF(start_node.location.Value.X - loopSize / 2, start_node.location.Value.Y - loopSize / 2),
                        new PointF(start_node.location.Value.X, start_node.location.Value.Y - loopSize),
                        new PointF(start_node.location.Value.X + loopSize / 2, start_node.location.Value.Y - loopSize / 2),
                        new PointF(start_node.location.Value.X, start_node.location.Value.Y)
                    };

                    start = new Point((int)loopPoints[1].X, (int)loopPoints[1].Y);
                    end = new Point((int)loopPoints[0].X, (int)loopPoints[0].Y);
                    g.DrawCurve(Pens.Black, loopPoints);
                    DrawArrow(g, start, end);
                    continue;
                }
                g.DrawLine(Pens.Black, arc.Item1.location.Value, arc.Item2.location.Value);

            }

            foreach(Node node in nodes)
            {
                if(node.state == _initialState)
                {
                    g.FillEllipse(Brushes.Orange, node.location.Value.X - nodeRadius, node.location.Value.Y - nodeRadius, 2 * nodeRadius, 2 * nodeRadius);

                    name = node.state.ToString();
                    textSize = g.MeasureString(name, Font);
                    nameposition = new PointF(node.location.Value.X - textSize.Width / 2, node.location.Value.Y - textSize.Height / 2);
                    g.DrawString(name, Font, Brushes.Black, nameposition);
                }
                else if(_finalStates.Contains(node.state))
                {
                    // drawing a double circle to indicate a final state

                    int doubleRadius = (int)(2 * nodeRadius);
                    int doubleRadiusPlus2 = doubleRadius + 2;

                    int boundingBoxX = node.location.Value.X - doubleRadiusPlus2 / 2;
                    int boundingBoxY = node.location.Value.Y - doubleRadiusPlus2 / 2;

                    g.FillEllipse(Brushes.LightGreen, boundingBoxX, boundingBoxY, doubleRadiusPlus2, doubleRadiusPlus2);
                    g.DrawEllipse(Pens.Black, boundingBoxX, boundingBoxY, doubleRadiusPlus2, doubleRadiusPlus2);
                    g.DrawEllipse(Pens.Black, boundingBoxX + 2, boundingBoxY + 2, doubleRadius, doubleRadius);


                    name = node.state.ToString();
                    textSize = g.MeasureString(name, Font);
                    nameposition = new PointF(node.location.Value.X - textSize.Width / 2, node.location.Value.Y - textSize.Height / 2);
                    g.DrawString(name, Font, Brushes.Black, nameposition);
                }
                else
                {
                    g.FillEllipse(Brushes.White, node.location.Value.X - nodeRadius, node.location.Value.Y - nodeRadius, 2 * nodeRadius, 2 * nodeRadius);
                    g.DrawEllipse(Pens.Black, node.location.Value.X - nodeRadius, node.location.Value.Y - nodeRadius, 2 * nodeRadius , 2 * nodeRadius );
                    name = node.state.ToString();
                    textSize = g.MeasureString(name, Font);
                    nameposition = new PointF(node.location.Value.X - textSize.Width / 2, node.location.Value.Y - textSize.Height / 2);
                    g.DrawString(name, Font, Brushes.Black, nameposition);
                }
                
            }

            foreach (var arc in arcs)
            {

                Node referenceNode = arc.Item2;

                 name = tList.Find(t => t.StartState == arc.Item1.state && t.EndState == arc.Item2.state).TransitionCharacter;
                 textSize = g.MeasureString(name, Font);
                

                switch (arc.Item1.state == arc.Item2.state)
                {
                    case true:

                        nameposition = new PointF(referenceNode.location.Value.X - textSize.Width, referenceNode.location.Value.Y - loopSize - textSize.Height);
                        g.DrawString(name, Font, Brushes.Black, nameposition);
                        continue;

                }
                
                name = tList.Find(t => t.StartState == arc.Item1.state && t.EndState == arc.Item2.state).TransitionCharacter;

                int dX = arc.Item1.location.Value.X - arc.Item2.location.Value.X;
                int dY = arc.Item1.location.Value.Y - arc.Item2.location.Value.Y;
                double angle = Math.Atan2(dY, dX);

                double halfDistance = Math.Sqrt(dX * dX + dY * dY) / 2;
                int xDiplacement = (int)(Math.Cos(angle) * halfDistance);
                int yDiplacement = (int)(Math.Sin(angle) * halfDistance);

                

                int arcMiddlePointX = referenceNode.location.Value.X + xDiplacement;
                int arcMiddlePointY = referenceNode.location.Value.Y + yDiplacement;


                nameposition = new PointF(arcMiddlePointX, arcMiddlePointY);

                g.DrawString(name, Font, Brushes.Black, nameposition);


                DrawArrow(g, arc.Item1.location.Value, arc.Item2.location.Value);
            }



        }


        private Node FindNode(Point location)
        {
            foreach (var node in nodes)
            {
                if (Math.Sqrt(Math.Pow(location.X - node.location.Value.X, 2) + Math.Pow(location.Y - node.location.Value.Y, 2))<= nodeRadius)
                {
                    return node;
                }
            }

            return new Node();
        }

        private void SetNodes()
        { 
            Random rndmGenerator = new Random();
            int posX = rndmGenerator.Next(20, canvas.Width - (int)nodeRadius);
            int posY = rndmGenerator.Next(20, canvas.Height - (int)nodeRadius);
            Node newNode;
            foreach(int state in _states)
            {
                posX = rndmGenerator.Next(20, 600);
                posY = rndmGenerator.Next(20, 400);
                newNode =  new Node(  new Point(posX, posY), state, state.ToString() );
                nodes.Add(newNode);

            }

            foreach(Node node in nodes)
            {
                Controls.Add(node.statename);
                node.SetLabelLocation();
            }


            
        }
        private void SetArcs()
        {
            Tuple<Node, Node> nodePair;
            Predicate<Node> start_state;
            Predicate<Node> end_state;

            Node start_node, end_node;
            foreach(Transition tr in tList)
            {
                start_state = node => node.state == tr.StartState;
                end_state = node => node.state == tr.EndState;
                start_node = nodes.Find(start_state);
                end_node = nodes.Find(end_state);

                nodePair = new Tuple<Node, Node>(start_node, end_node);
                arcs.Add(nodePair);
            }
        }
        private void DrawArrow(Graphics g, Point start, Point end)
        {
            // Calculate the angle of the arrow
            double angle = Math.Atan2(end.Y - start.Y, end.X - start.X);

            // Define arrow size
            int arrowSize = 10;
            int deltaX = end.X - start.X;
            int deltaY = end.Y - start.Y;
            double distance = Math.Sqrt(deltaY * deltaY + deltaX * deltaX);
            // Calculate arrow points
            Point arrowPoint1 = new Point((int)(end.X - arrowSize * Math.Cos(angle - Math.PI / 6)), (int)(end.Y - arrowSize * Math.Sin(angle - Math.PI / 6)));
            Point arrowPoint2 = new Point((int)(end.X - arrowSize * Math.Cos(angle + Math.PI / 6)), (int)(end.Y - arrowSize * Math.Sin(angle + Math.PI / 6)));

            int decX = (int)(Math.Cos(angle) * nodeRadius);
            int decY = (int)(Math.Sin(angle) * nodeRadius);



            // Draw arrow
            g.DrawLine(Pens.Black, end.X - decX, end.Y - decY, arrowPoint1.X - decX, arrowPoint1.Y - decY);
            g.DrawLine(Pens.Black, end.X - decX, end.Y - decY, arrowPoint2.X - decX, arrowPoint2.Y - decY);
        }

        


        

    //================================ Auotmaton Logic ============================================ 
    private void MainWindow_Load(object sender, System.EventArgs e)
        {

        }



        private void setTransitions_Click(object sender, EventArgs e)
        {
            

            tList = new List<Transition>();
            Transition t;
            int s_state;
            int e_state;
            string t_char;
            for (int i = 0; i < transitionTable.Rows.Count - 1; i++)
            {
                try
                {
                    s_state = Convert.ToInt32(transitionTable.Rows[i].Cells["start_state"].Value);
                    e_state = Convert.ToInt32(transitionTable.Rows[i].Cells["end_state"].Value);
                    t_char = transitionTable.Rows[i].Cells["transition_char"].Value.ToString();

                }catch(Exception)
                {
                    showError("Error in the transitions, verify them carefully", "Error");
                    return;
                }

                t = new Transition(s_state, e_state, t_char); // t is a transition object

                tList.Add(t);
            }

            for(int i = 0; i < tList.Count; i++)
            {
                automaton.AssignTransition(tList[i]);
            }

            SetNodes();
            SetArcs();
            canvas.Invalidate();


        }


        private void setStates_Click(object sender, EventArgs e)
        {
            Regex finalStateParser = new Regex("\\d+");
            Regex statesParser = new Regex("\\d{1}");
            Regex alphabetParser = new Regex("\\w{1}");

            try
            {
                _initialState = Convert.ToInt32(initalStateTxt.Text);
            }
            catch (FormatException)
            {
                initalStateTxt.Text = "INVALID VALUE!";
                initalStateTxt.BackColor = Color.Red;
                return;
            }


            MatchCollection alphabetColl = alphabetParser.Matches(alphabetTxt.Text);
            MatchCollection finalStatesColl = finalStateParser.Matches(finalStatesTxt.Text);



            _states = new List<int>();
            _alphabet = new List<string>();
            _finalStates = new List<int>();
            // inputing the alphabet characters
            foreach (var thing in alphabetColl)
            {
                _alphabet.Add(thing.ToString());
            }

            // inputing the final states
            foreach (var state in finalStatesColl)
            {
                _finalStates.Add(Convert.ToInt32(state.ToString()));
            }

            // inputting the states
            int numberOfStates;
            try
            {
                numberOfStates = Convert.ToInt32(statesParser.Matches(statesNumberTxt.Text)[0].ToString());

            }
            catch (Exception)
            {
                statesNumberTxt.Text = "INVALID VALUE!";
                statesNumberTxt.BackColor = Color.Red;
                return;
            }

            for (int i = 1; i <= numberOfStates; i++)
            {
                _states.Add(i);
            }

            automaton = new FiniteStateMachine(_initialState, _finalStates, _states, _alphabet);
            this.setTransitionsBtn.Enabled = true;
            this.transitionTable.Enabled = true;
        }

        private void showError(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void validateWordBtn_Click(object sender, EventArgs e)
        {
            if(automaton.ValidateWord(inputWordTxt.Text))
            {
                MessageBox.Show("Word is valid");

            }
            else
            {
                MessageBox.Show("Word is not valid");
            }
        }

        private void refreshScreen_Click(object sender, EventArgs e)
        {
            canvas.Invalidate();
        }

        private void saveCanvas_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string pictureLoc = saveCanvas.FileName;
            StreamWriter pictureWriter = new StreamWriter(pictureLoc);

            pictureWriter.Write(canvas);
        }

        private void saveCanvasOption_Click(object sender, EventArgs e)
        {


            

        }
    }
}
