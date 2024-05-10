using System.Windows.Forms;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using RecognitionSystems;
using System.Drawing;



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

        float nodeRadius = 10;
        private Node selectedNode = new Node();
        private PointF? mouseDownLocation = null;
        private List<Node> nodes = new List<Node>();
        private List<Tuple<Node, Node>> arcs = new List<Tuple<Node, Node>>();
        


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
            if (selectedNode.location != null && mouseDownLocation.HasValue)
            {
                // Move the selected node
                int deltaX = Convert.ToInt32(e.X - mouseDownLocation.Value.X);
                int deltaY = Convert.ToInt32(e.Y - mouseDownLocation.Value.Y);
                nodes[nodes.IndexOf(selectedNode)].location = new Point(selectedNode.location.Value.X + deltaX, selectedNode.location.Value.Y + deltaY);
                mouseDownLocation = e.Location;
                canvas.Invalidate();
            }

        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            // selcted node is always null.
            selectedNode.location = null;
            mouseDownLocation = null;
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach(var arc in arcs)
            {
                g.DrawLine(Pens.Black, (PointF)arc.Item1.location, (PointF)arc.Item2.location);
            }

            foreach(var node in nodes)
            {
                g.FillEllipse(Brushes.Blue, node.location.Value.X - nodeRadius, node.location.Value.Y - nodeRadius, 2 * nodeRadius, 2 * nodeRadius);
            }

        }
        private void canvas_Click(object sender, EventArgs e)
        {
            Point mouseLocation = canvas.PointToClient(Cursor.Position);
            Node node = new Node();
            node.location = mouseLocation;
            nodes.Add(node);
            canvas.Invalidate();

        }

        private Node FindNode(PointF location)
        {
            foreach (var node in nodes)
            {
                if (Math.Sqrt(Math.Pow(location.X - node.location.Value.X, 2) + Math.Pow(location.Y - node.location.Value.Y, 2))<= nodeRadius)
                {
                    return node;
                }
            }

            return null;
        }
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


    }
}
