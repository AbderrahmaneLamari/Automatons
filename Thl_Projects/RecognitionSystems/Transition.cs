using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognitionSystems
{
    public class Transition
    {
        private int startState;
        private int endState;
        private string transitionCharacter;

        public Transition(int startState, int endState, string transitionCharacter)
        {
            if (endState < -1 || 0 == endState)
            {
                throw new ArgumentException("end state can't be zero or less than -1.");
            }
            if(startState <= 0  || null == transitionCharacter || "" == transitionCharacter)
            {
                throw new ArgumentException("Parameters a not valid.");

            }
            this.startState = startState;
            this.endState = endState;
            this.transitionCharacter = transitionCharacter;
        }

        public int StartState
        {
            get { return startState; }
            set { if (value >= 0) { startState = value; } }
        }

        public int EndState
        {
            get { return endState; }
            set { if (value >= 0) { endState = value; } }
        }

        public string TransitionCharacter
        {
            get { return transitionCharacter; }
            set { if(value == null || value == "") { /*Do nothing */} else { transitionCharacter = value; } }
        }


    }
}
