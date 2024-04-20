using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton
{


    class Automaton
    {
        private bool objectIsInit = false;
        private int initialState;
        private List<int> finalStates;
        private List<int> allStates;
        private List<string> alphabet;
        private List<int> t = new List<int>();

        public List<int>[,] transitions;
        public Automaton()
        {
            alphabet = new List<string>();
        }  //The identity constructor
        public Automaton(int initialState, List<int> finalStates, List<int> allStates, List<string> alphabet) : this()
        {
            this.initialState = initialState;
            this.finalStates = finalStates;
            this.allStates = allStates;
            this.alphabet = alphabet;
            objectIsInit = true;
        }




        public void AssignTransitions(int state, string character, int resulatantState)
        {
            int stateIndex = allStates.IndexOf(state);
            int characterIndex = alphabet.IndexOf(character);


            if (objectIsInit)
            {
                transitions = new List<int>[allStates.Count, alphabet.Count]; // The transition table, just like school!  
                objectIsInit = false;
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

        public bool AssignLanguage(string word)
        {
            if(this.alphabet.Exists(x => x == word)){
                return false;
            }
            else
            {
                this.alphabet.Add(word);
                return true;
            }
        } // assinging words one by one

        public bool AssignLanguage(List<string> language) { 
            
            if(null == language)
            {
                return false;
            }
            else
            {
                this.alphabet = language;
                return true;
            }
            
        }// assiging the whole language at once.

        //The only important method in the whole class.
        public bool ValidateWord(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                throw new ArgumentException("Input word cannot be null or empty.");
            }

            return ValidateWordRecursive(word, 0, initialState);
        }

        private bool ValidateWordRecursive(string word, int index, int currentState)
        {
            if (index == word.Length)
            {
                // Check if the current state is one of the final states
                return finalStates.Contains(currentState);
            }

            char c = word[index];
            int stateIndex = allStates.IndexOf(currentState);
            int charIndex = alphabet.IndexOf(c.ToString());

            if (stateIndex == -1 || charIndex == -1 || transitions[stateIndex, charIndex] == null)
            {
                return false; // Transition not found for current state and character
            }

            List<int> nextStates = transitions[stateIndex, charIndex];
            foreach (int nextState in nextStates)
            {
                if (nextState != -1)
                {
                    // Recursively explore each possible transition
                    if (ValidateWordRecursive(word, index + 1, nextState))
                    {
                        return true; // Found a path leading to an accepting state
                    }
                }
            }

            return false; // No valid transition found for this character
        }


    }
}