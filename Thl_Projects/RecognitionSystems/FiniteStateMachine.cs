using System;
using System.Collections.Generic;


namespace RecognitionSystems
{


    public class FiniteStateMachine
    {
        private bool objectIsInit = false;

        private int initialState;
        private List<int> finalStates;
        private List<int> allStates;
        private List<string> alphabet;
        private List<int> t = new List<int>();

        public List<int>[,] transitions; // 2d array
        public FiniteStateMachine()
        {
            alphabet = new List<string>();
            allStates = new List<int>();
            finalStates = new List<int>();

        }  //The identity constructor
        public FiniteStateMachine(int initialState, List<int> finalStates, List<int> allStates, List<string> alphabet) : this()
        {
            this.initialState = initialState;
            this.finalStates = finalStates;
            this.allStates = allStates;
            this.alphabet = alphabet;
            objectIsInit = true;

            


            transitions = new List<int>[allStates.Count, alphabet.Count];

            for (int i = 0; i < allStates.Count; i++)
            {
                for (int j = 0; j < alphabet.Count; j++)
                {
                    transitions[i, j] = new List<int>(); // Initialization of all cells with a List<int>
                }
            }
        }


        public List<int>[,] Transitions
        {
            get { return transitions; }
        } // public property

        

        public void AssignTransition(int state, string character, int resulatantState)
        {

            int stateIndex = allStates.IndexOf(state);
            int characterIndex = alphabet.IndexOf(character);


            if (objectIsInit)
            {
                transitions = new List<int>[allStates.Count, alphabet.Count]; // The transition table, just like school!  
                objectIsInit = false;

                for (int i = 0; i < allStates.Count; i++)
                {
                    for (int j = 0; j < alphabet.Count; j++)
                    {
                        transitions[i, j] = new List<int>();
                    }
                }
            }


            if (transitions[stateIndex, characterIndex] is null)
            {
                transitions[stateIndex, characterIndex] = new List<int>();
                transitions[stateIndex, characterIndex].Add(resulatantState);
            }
            else
            {
                transitions[stateIndex, characterIndex].Add(resulatantState);
            }

        }
        public void AssignTransition(Transition transition) 
        {
            if(transition == null) { return; }
            int stateIndex = allStates.IndexOf(transition.StartState);
            int characterIndex = alphabet.IndexOf(transition.TransitionCharacter);

            if(-1 == stateIndex)
            {
                throw new ArgumentOutOfRangeException("startstate", "start state is not defined.");
            }

            if(-1 == characterIndex)
            {
                throw new ArgumentOutOfRangeException("character", "character is not defined.");
            }

                transitions[stateIndex, characterIndex].Add(transition.EndState);
            
        }
        
        public bool RemoveTransition(Transition transition)
        {
            if (null == transition)
            {
                return false;
            }

            int stateIndex = allStates.IndexOf(transition.StartState);
            int charIndex = alphabet.IndexOf(transition.TransitionCharacter);

            if (-1 == stateIndex || -1 == charIndex) // no such char or state.
            {
                return false;
            }

            if (null == transitions[stateIndex, charIndex]) // no such transition for such input
            {
                return false;
            }

            if (!transitions[stateIndex, charIndex].Contains(transition.EndState))
            {
                return false; // transition non existatnt
            }
            
            if (1 == transitions[stateIndex, charIndex].Count)
            {
                // Only one state, removing it, and replacing it with null transition.
                transitions[stateIndex, charIndex].Remove(transition.EndState);
                transitions[stateIndex, charIndex].Add(-1);
                return true;
            }
            else
            {
                // simple removal of the transition.
                transitions[stateIndex, charIndex].Remove(transition.EndState);
                return true;
            }
        } // NOT USED
        public bool RemoveState(int state)
        {
            if(0 >= state) { return false; }

            if (!allStates.Contains(state)) { return false; }

            

            List<Transition> tList = GetTransitions();

            tList.RemoveAll( t => t.StartState == state  &&  t.EndState == state );
            RefreshTransitions(tList);
            allStates.Remove(state);

            return true;
        }

        public bool RemoveCharacter(string c)
        {

            if("" == c) { return false; }
            if(null == c) { return false; }

            if (!alphabet.Contains(c)) { return false; }

            
            List<Transition> tList = GetTransitions();

            tList.RemoveAll(t => t.TransitionCharacter.Equals(c));
            RefreshTransitions(tList);
            alphabet.Remove(c);
            return true;

        }
        public List<Transition> GetTransitions()
        {
            List<Transition> transList = new List<Transition>();
            Transition trans;

            
            for (int i = 0; i < transitions.GetLength(0); i++)
            {
                for (int j = 0; j < transitions.GetLength(1); j++)
                {
                    if (0 == this.transitions[i, j].Count) { continue; } // skipping the empty cells

                    for (int k = 0; k < this.transitions[i, j].Count; k++)
                    {
                        
                        trans = new Transition(allStates[i], transitions[i, j][k], alphabet[j]); // This is nasty to say the least, O(n^3), eww.
                        transList.Add(trans);
                    }

                }
            }
            
            return transList;
        }
        public bool AssignAlphabet(string symbol)
        {
            if(null == symbol || "" == symbol)
            {
                return false;
            }

            if (1 < symbol.Length)
            {
                return false;
            }

            if(this.alphabet.Exists(x => x == symbol)){
                return false;
            }
            else
            {
                this.alphabet.Add(symbol);
                return true;
            }
        } // NOT USED
        public bool AssignAlphabet(List<string> alphabet) { 
            
            if(null == alphabet)
            {
                return false;
            }
            else
            {
                this.alphabet = alphabet;
                return true;
            }
            
        }// NOT USED

        public bool AssignInitialState(int state)
        {

            if (state <= 0)
            {
                return false;
            }

            if (!allStates.Contains(state))
            {
                return false;
            }

            initialState = state;
            return true;

        } // NOT USED
        public bool AddState(int state)
        {
            if ( 0 >= state)
            {
                return false;
            }

            if (allStates.Contains(state))
            {
                return false;
            }

            allStates.Add(state);
            return true;
        } // NOT USED

        public bool AddFinalState(int state)
        {
            if(0 >= state)
            {
                return false;
            }

            if (finalStates.Contains(state))
            {
                return false;
            }

            if (!allStates.Contains(state))
            {
                allStates.Add(state);
            }

            finalStates.Add(state);
            return true;
        } //  NOT USED

        public bool ReplaceInitialState(int state)
        {
            if (0 >= state)
            {
                return false;
            }

            if (!allStates.Contains(state))
            {
                return false;
            }

            if (allStates.Contains(state))
            {
                initialState = state;
            }
            else
            {
                allStates.Add(state);
                initialState = state;
            }
            return true;
        }// NOT USED

        //The only important method in the whole class.
        public bool ValidateWord(string word)
        {
            if (word == null)
            {
                throw new ArgumentException("Input word cannot be null.");
            }

            return ValidateWordRecursive(word, 0, initialState);
        }
        // The only imprtant recursive method in the whole class
        private bool ValidateWordRecursive(string word, int index, int currentState)
        {
            if (index == word.Length)
            {
               
                return finalStates.Contains(currentState);
            }

            char c = word[index];
            int stateIndex = allStates.IndexOf(currentState);
            int charIndex = alphabet.IndexOf(c.ToString());

            if (stateIndex == -1 || charIndex == -1 || transitions[stateIndex, charIndex] == null)
            {
                return false; // no transition for the character and state. invalid transition.
            }

            List<int> nextStates = transitions[stateIndex, charIndex]; // pointer to the next states.
            foreach (int nextState in nextStates)
            {
                if (nextState != -1)
                {
                    // parcour profondeur!
                    if (ValidateWordRecursive(word, index + 1, nextState))
                    {
                        return true; // found a path
                    }
                }
            }

            return false; // no path and the word is invalid
        }
        private void RefreshTransitions(List<Transition> t)
        {
            this.transitions = new List<int>[allStates.Count, alphabet.Count];

            int iLength = transitions.GetLength(0);
            int jLength = transitions.GetLength(1);

            for(int i = 0; i < iLength; i++)
            {
                for(int j = 0; j < jLength; j++)
                {
                    transitions[i, j] = new List<int>();
                }
            }

            foreach(Transition z in t)
            {
                AssignTransition(z);
            }
        }

    }
}