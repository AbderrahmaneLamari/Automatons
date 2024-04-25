using System;
using System.Collections.Generic;
using Compiler.view;
using Compiler.model;

namespace Compiler.controller
{
    class Program
    {
        
        static void Main(string[] args)
        {

            Automaton wordValidator; // it only check if word are in a language. Yeah it's only an analyser.
            Transition transition;
            List<int> finitestates = new List<int>();
            List<int> finalStates = new List<int>();
            List<string> alphabet = new List<string>();
            Menu mainMenu = new Menu();

            List<int> k = new List<int>();


            View.DisplayPrompt("Enter the number of states: ");
            int numberOfstates = View.ReadInt();
            for(int i = 0; i < numberOfstates; i++)
            {
                finitestates.Add(i + 1);
            }

            View.DisplayPrompt("Enter the number of final states: ");
            int numberOfFinalStates = View.ReadInt();
            

            for(int i = 0; i < numberOfFinalStates; i++)
            {
                View.DisplayPrompt("Enter final state: ");
                finalStates.Add(View.ReadInt());
            }

            View.DisplayPrompt("Enter the initial state: ");
            int initialState = View.ReadInt();

            View.DisplayPrompt("Enter the number of characters in the alphabet: ");
            int numberOfCharacters = View.ReadInt();
            

            for(int i = 0; i < numberOfCharacters; i++)
            {
                View.DisplayPrompt("Enter the character: ");
                alphabet.Add(View.ReadString());
            }
            wordValidator = new Automaton(initialState, finalStates, finitestates, alphabet);

            View.DisplayPrompt("Enter the number of transitions: ");

            int numberOftransitions = View.ReadInt();

            View.DisplayPrompt("Enter non-zero or negative start states, non-zero or less than -1 final states, and no epsilon.");
            View.DisplayPrompt("Format: SCF, S: start state, C: transition character, F: End state.");

            int s, e;
            string c;
            for (int i = 0; i < numberOftransitions; i++)
            {
                View.Clear();
                View.DisplayPrompt("S x C -> F : No. " + (i + 1));

                s = View.ReadInt();
                c = View.ReadString();
                e = View.ReadInt();
                transition = new Transition(s,e,c);
                wordValidator.AssignTransition(transition);
                
            }
            

            string userWord;
            int choice;
            List<Transition> removables = new List<Transition>();
            List<Transition> l = new List<Transition>();
            bool didit;
            while (true)
            {
                View.DisplayPrompt("use ~ to modify the automaton, ! to exit");
                View.DisplayPrompt("Enter the word: ");
                userWord = View.ReadString();
                if (userWord.Equals("!")) { break; }
                if (userWord.Equals("~"))
                {
                    while (true)
                    {

                    
                    View.DisplayPrompt("Choose What you want to change: " +
                                       "1: add states\n " +
                                       "2: remove states\n " +
                                       "3: add alphabet\n " +
                                       "4: remove alphabet\n " +
                                       "5: add transition\n " +
                                       "6: remove transition\n " +
                                       "7: finalize \n "
                                       +"8: Print the automaton ");

                    choice = View.ReadInt();

                        switch (choice)
                        {
                            case 1: // Adding a state

                                View.DisplayPrompt("Enter the number of new states: ");
                                numberOfstates = View.ReadInt();

                                for (int i = 0; i < numberOfstates; i++)
                                {
                                    finitestates.Add(View.ReadInt());
                                }
                                break;
                            case 2: // remove states
                                View.DisplayPrompt("Enter the number of states to delete: ");
                                numberOfstates = View.ReadInt();

                                for (int i = 0; i < numberOfstates; i++)
                                {
                                    if (!finitestates.Remove(View.ReadInt()))
                                    {
                                        View.DisplayPrompt("The state doesn't exist.");
                                    }
                                }
                                break;

                            case 3: // add alphabet
                                View.DisplayPrompt("Enter the number of new characters in the alphabet: ");
                                numberOfCharacters = View.ReadInt();

                                for (int i = 0; i < numberOfCharacters; i++)
                                {
                                    alphabet.Add(View.ReadString());
                                }

                                break;
                            case 4: // remove alphabet
                                View.DisplayPrompt("Enter the number of characters to delelte: ");
                                numberOfCharacters = View.ReadInt();

                                for (int i = 0; i < numberOfCharacters; i++)
                                {
                                    if (!alphabet.Remove(View.ReadString()))
                                    {
                                        View.DisplayPrompt("The character doesn't exist. This char is skipped, finish and retry.");
                                    }
                                }
                                break;
                            case 5: // Add transitions

                                l = wordValidator.GetTransitions();
                            
                                View.DisplayPrompt("Enter the number of new transitions: ");
                                numberOftransitions = View.ReadInt();

                                View.DisplayPrompt("Enter non-zero or negative start states, non-zero or less than -1 final states, and no epsilon.");
                                View.DisplayPrompt("Format: SCF, S: start state, C: transition character, F: End state.");


                                for (int i = 0; i < numberOftransitions; i++)
                                {
                                    View.Clear();
                                    View.DisplayPrompt("S x C -> F : No. " + (i+1));

                                    s = View.ReadInt();
                                    c = View.ReadString();
                                    e = View.ReadInt();
                                    transition = new Transition(s, e, c);
                                    l.Add(transition);
                                    
                                }

                                break;
                            case 6: // remove transitions.
                                l = wordValidator.GetTransitions();


                                View.DisplayPrompt("Enter the number of  transitions to delete: ");
                                numberOftransitions = View.ReadInt();

                                View.DisplayPrompt("Enter non-zero or negative start states, non-zero or less than -1 final states, and no epsilon.");
                                View.DisplayPrompt("Format: SCF, S: start state, C: transition character, F: End state.");


                                for (int i = 0; i < numberOftransitions; i++)
                                {
                                    View.DisplayPrompt("SCF " + i);

                                    s = View.ReadInt();
                                    c = View.ReadString();
                                    e = View.ReadInt();
                                    transition = new Transition(s, e, c);

                                    didit = l.Remove(l.Find(x => (x.StartState == s) && (x.EndState == e) || (x.TransitionCharacter == c)));

                                    if (!didit)
                                    {
                                        View.DisplayPrompt("The transisiton does not exist. Finish and retry.");
                                    }

                                    View.Clear();
                                }

                                break;
                            case 7: // finalizing and reinitializing the automaton

                                wordValidator = new Automaton(initialState, finalStates, finitestates, alphabet);

                                for (int i = 0; i < l.Count; i++)
                                {
                                    wordValidator.AssignTransition(l[i]);
                                }
                                
                                break;
                            case 8: // printing the automaton

                                View.Clear();
                                List<int>[,] t = wordValidator.Transitions;

                                Console.Write("     |");
                                for(int i = 0; i < alphabet.Count; i++)
                                {
                                    Console.Write("|  " + alphabet[i] + "  ");
                                }
                                Console.Write("|");
                                Console.WriteLine();

                                for (int n = 0; n < t.GetLength(1) + 1; n++)
                                {
                                    Console.Write("------");
                                }
                                Console.Write("--");
                                Console.WriteLine();

                                for (int i = 0; i < t.GetLength(0); i++)
                                {
                                    if(i+1 == initialState)
                                    {
                                        Console.Write("-> " + (i + 1) + " ");
                                    }
                                    else if(finalStates.Contains(i + 1))
                                    {
                                        Console.Write(" |" + (i + 1) + "| ");
                                    }
                                    else
                                    {
                                        Console.Write("  " + (i + 1) + "  ");
                                    }
                                    Console.Write("|");
                                    
                                    
                                    for(int j = 0; j < t.GetLength(1); j++)
                                    {
                                        if (t[i, j].Count == 0)
                                        {
                                            Console.Write("|  " + "/" + "  ");
                                        }
                                        else
                                        {
                                            Console.Write("|  " + t[i, j][0] + "  ");
                                        }
                                    }

                                    Console.Write("|");
                                    Console.WriteLine();
                                    for (int n = 0; n < t.GetLength(1) + 1; n++)
                                    {
                                        Console.Write("------");
                                    }
                                    Console.Write("-");
                                    Console.WriteLine();
                                }

                                Console.ReadKey();
                                break;
                        }

                        if(7 == choice) { break; }
                    }
                }

                if (wordValidator.ValidateWord(userWord))
                {
                    View.DisplayPrompt("The word is in the language.");
                }
                else
                {
                    View.DisplayPrompt("The word is NOT in the language.");
                }
                Console.ReadKey();
                View.Clear();

            }
            

            //int userChoice;
            //int start, end;
            //string transitionChar;
            //int tempState;

            //mainMenu.AddOption("Enter new Alphabet", 1); // use AssignAlphabet(List<>)
            //mainMenu.AddOption("Add new Symbol", 2);     // use AssignAlphabet(string)
            //mainMenu.AddOption("Add new States", 3);     // Done use AddState(int)

            //mainMenu.AddOption("Enter Initial State", 4); // use AssignInitialState(int)
            //mainMenu.AddOption("Add new final States", 5); // use addFinalState(int)
            //mainMenu.AddOption("Replace Initial State", 6); // use replaceInitialState(int)
            
            //mainMenu.AddOption("Add a Transition", 7); // use assignTransition(Transition)
            //mainMenu.AddOption("Remove a Transition", 8); // use RemoveTransition(Transition)
            //mainMenu.AddOption("Modify Transition", 9); // Remove the old and input the new. 
           
            
            //mainMenu.AddOption("Show Transitions", 10); //
            //mainMenu.AddOption("Initilize/Reinitialize Automaton", 11);
            //mainMenu.AddOption("Close The Application", 14); //

            //View.DisplayPrompt("Welcome to the programmble Compiler!\n");

            

            //wordValidator = new Automaton(); // init of the automaton




            // Application core.
            //while (true)
            //{
            //    View.DispalyOptions(mainMenu);
            //    View.DisplayPrompt("Enter an option  ");
                
            //    userChoice = View.ReadInt();
                
            //    View.Clear();
            //    switch (userChoice)
            //    {
            //        case 1: // add new alphabet

            //            View.DisplayPrompt("Enter the number of characters ");
            //            userChoice = View.ReadInt();
            //            userChoice = userChoice >= 0 ? userChoice : -userChoice;
            //            for(int i = 0; i < userChoice; i++)
            //            {
            //                wordValidator.AssignAlphabet(View.ReadString());
            //            }
            //            break;
            //        case 2: // add new symbol
            //            View.Clear();

            //            View.DisplayPrompt("Enter the new symbol: ");
            //            wordValidator.AssignAlphabet(View.ReadString());

            //            View.PauseScreen();
            //            break;
            //        case 3: // add State

            //            View.Clear();
            //            View.DisplayPrompt("Enter the new State:  ");

            //            if (wordValidator.AddState(View.ReadInt()))
            //            {
            //                View.DisplayPrompt("State Added Succefully!");
            //            }
            //            else
            //            {
            //                View.DisplayPrompt("State Was not Added successfully!\n enter a number greater than zero, which you didn't enter before.");
            //            }

            //            View.PauseScreen();
            //            break;
            //        case 4: // assigning an initial state.

            //            View.DisplayPrompt("Enter the initial state:  ");
            //            if (wordValidator.AssignInitialState(View.ReadInt()))
            //            {
            //                View.DisplayPrompt("State Assigned successfully!");

            //            }
            //            else
            //            {
            //                View.DisplayPrompt("State is either negative, equals zero, or not in states set.");
            //            }

            //            View.PauseScreen();
            //            break;
            //        case 5: // adding final states

            //            View.DisplayPrompt("Enter the new final state:  ");
            //            if (wordValidator.AddFinalState(View.ReadInt()))
            //            {
            //                View.DisplayPrompt("The State was added successfully");
            //            }
            //            else
            //            {
            //                View.DisplayPrompt("There was an error entering the new final state: either it's negative, zero, \nor not in the list of all states.");
            //            }

            //            View.PauseScreen();
            //            break;
            //        case 6: // replace initial state.

            //            View.DisplayPrompt("Enter the new initial state:  ");
            //            tempState = View.ReadInt();

            //            if (wordValidator.ReplaceInitialState(tempState))
            //            {
            //                View.DisplayPrompt("Initial State Replaced successfully!");
            //            }
            //            else
            //            {
            //                View.DisplayPrompt("Error Replacing initial state. Check if you added this state to the states list.");
            //            }

            //            View.PauseScreen();
            //            break;
            //        case 7: // assigning a transition.

            //            View.DisplayPrompt("Enter the start state:  ");
            //            start = View.ReadInt();

            //            View.DisplayPrompt("Enter the distenation state:  ");
            //            end = View.ReadInt();

            //            View.DisplayPrompt("Enter the transition character:  ");
            //            transitionChar = View.ReadString();

                       
            //            try
            //            {
            //                transition = new Transition(start, end, transitionChar);
            //                wordValidator.AssignTransition(transition);
            //            }
            //            catch (ArgumentException e)
            //            {
            //                View.DisplayPrompt(e.Message);
            //                View.DisplayPrompt("Revise the start, end, and transition char, the are either negative, zero, null, or empty.");
            //            }
            //            View.PauseScreen();
            //            break;
            //        case 8:

            //            View.DisplayPrompt("Enter the transition (StartState, EndState, Transition Character: \n");
            //            View.DisplayPrompt("Enter the Start State:  ");
            //            start = View.ReadInt();

            //            View.DisplayPrompt("Enter The End State:  ");
            //            end = View.ReadInt();

            //            View.DisplayPrompt("Enter the transition character:  ");
            //            transitionChar = View.ReadString();

            //            try
            //            {
            //                transition = new Transition(start, end, transitionChar);

            //                if(wordValidator.RemoveTransition(transition)) // attempting to remove transition
            //                {
            //                    View.DisplayPrompt("Transition removed successfully!");
            //                }
            //                else
            //                {
            //                    View.DisplayPrompt("Transition was not removed, might not exist, no such character or state.");

            //                }
            //            }
            //            catch(ArgumentException e)
            //            {
            //                View.DisplayPrompt(e.Message);
            //                View.DisplayPrompt("Verify your transition data, states might be zero or negative; transition character might be null, or empty: \"\"");
            //            }

            //            View.PauseScreen();
            //            break;
            //        case 9: // modify a transition

            //            View.DisplayPrompt("Enter the transition (Start State, End State, Transition Character): \n");
            //            View.DisplayPrompt("Enter the Start State:  ");

            //            start = View.ReadInt();

            //            View.DisplayPrompt("Enter the End State:  ");
            //            end = View.ReadInt();

            //            View.DisplayPrompt("Enter the transition character: ");
            //            transitionChar = View.ReadString();

            //            try
            //            {
            //                transition = new Transition(start, end, transitionChar);

            //                if (!wordValidator.RemoveTransition(transition)) // attempting to remove transition
            //                {
            //                    View.DisplayPrompt("Transition was not removed, might not exist, no such character or state.");
            //                    break;
            //                }


            //                View.DisplayPrompt("Enter the new start state:  ");
            //                start = View.ReadInt();

            //                View.DisplayPrompt("Enter the new end state:  ");
            //                end = View.ReadInt();

            //                View.DisplayPrompt("Enter the new transition character  ");
            //                transitionChar = View.ReadString();

            //                transition = new Transition(start, end, transitionChar);

            //                wordValidator.AssignTransition(transition); // assigning the transition

            //            }
            //            catch (ArgumentException e)
            //            {
            //                View.DisplayPrompt(e.Message);
            //                View.DisplayPrompt("Verify your transition data, states might be zero or negative; transition character might be null, or empty: \"\"");
            //            }

            //            View.PauseScreen();
            //            break; 
            //        case 10: // show transitions

            //            foreach(Transition tr in wordValidator.GetTransitions())
            //            {
            //                View.DisplayPrompt(tr.StartState + " x " + tr.TransitionCharacter + " -> " + tr.EndState);
            //            }

            //            View.PauseScreen();
            //            break;
            //        case 11:

            //            break;
            //        case 12:
            //            List<Transition> l = wordValidator.GetTransitions();
            //            wordValidator.RefreshAutomaton( l ); // tyring to refresh the automaton with less transtions.
            //            break;
            //        case 14:

            //            Environment.Exit(0); // exit with no error.

            //            break;
            //    }
            //}









            Console.WriteLine("End of application. Press any key to close...");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
