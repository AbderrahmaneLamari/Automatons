using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.model
{


    class Automaton
    {
        private bool objectIsInit = false;
        private int initialState;
        private List<int> finalStates;
        private List<int> allStates;
        private List<string> alphabet;
        private List<int> t = new List<int>();

        public List<int>[,] transitions; // 2d array
        public Automaton()
        {
            alphabet = new List<string>();
            allStates = new List<int>();
            finalStates = new List<int>();
        }  //The identity constructor
        public Automaton(int initialState, List<int> finalStates, List<int> allStates, List<string> alphabet) : this()
        {
            this.initialState = initialState;
            this.finalStates = finalStates;
            this.allStates = allStates;
            this.alphabet = alphabet;
            objectIsInit = true;

            transitions = new List<int>[allStates.Count, alphabet.Count];


        }


        public List<int>[,] Transitions
        {
            get { return transitions; }
        } // public property

        public void RefreshAutomaton(List<Transition> transitionList)
        {

            this.transitions = new List<int>[allStates.Count, alphabet.Count];
            foreach(Transition tr in transitionList)
            {
                try
                {
                    AssignTransition(tr);
                }
                catch(IndexOutOfRangeException)
                {
                    continue;
                }
                
            }
        }
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

            if (objectIsInit)
            {
                transitions = new List<int>[allStates.Count, alphabet.Count]; // The transition table, just like school!  
                objectIsInit = false;

                for (int i = 0; i < allStates.Count; i++)
                {
                    for (int j = 0; j < alphabet.Count; j++)
                    {
                        transitions[i, j] = new List<int>(); // Initialization of all cells with a List<int>
                    }
                }
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
            
            if (0 == transitions[stateIndex, charIndex].Count)
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
        }

        public List<Transition> GetTransitions()
        {
            List<Transition> transList = new List<Transition>();
            Transition trans;

            
            for (int i = 0; i < allStates.Count; i++)
            {
                for (int j = 0; j < alphabet.Count; j++)
                {
                    if (0 == transitions[i, j].Count) { continue; } // skipping the empty cells

                    for (int k = 0; k < transitions[i, j].Count; k++)
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
        } // assinging words one by one
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
            
        }// assiging the whole language at once.

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

        }
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
        }

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

            finalStates.Add(state);
            return true;
        }

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

            initialState = state;
            return true;
        }

        //The only important method in the whole class.
        public bool ValidateWord(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                throw new ArgumentException("Input word cannot be null or empty.");
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


    }
}