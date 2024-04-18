using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Automaton
{
    class Automaton
    {
        private int initialState;
        private int[] finalStates;
        private int[] allStates;
        private List<string> alphabet;

        private LinkedList<int>[][] transitions;
        public Automaton()
        {
            alphabet = new List<string>();
        }  //The identity constructor
        public Automaton(int initialState, int[] finalStates, int[] allStates, List<string> alphabet) : this()
        { 
            this.initialState = initialState;
            this.finalStates = finalStates;
            this.allStates = allStates;
            this.alphabet = alphabet;
        }

        

        public void assignTransitions(int stateNumber, int characterNumber, int resulatantState)
        {
            transitions[stateNumber][characterNumber].AddLast(resulatantState);
        }
       
        public void assignLanguage(string word) => this.alphabet.Add(word); // assinging words one by one

        public void assignLanguage(List<string> language) => this.alphabet = language; // assiging the whole language at once.


        //The only important method in the whole class.
        public bool validateWord(string word)
        {
            if(null == word)
            {
                throw new ArgumentNullException();
            }

            if("" == word)
            {
                // If the word was empty, there is nothing to validate.
                throw new ArgumentException();
            }



            return false; // remove this line and comment once you finish the implementation.
        }
        
    }

}
