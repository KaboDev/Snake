using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake_Game
{
    class UIBuilder
    {
        protected virtual void BuildSelection(string Title, List<Selection> selections)
        {
            string bottomLine = "";

            Console.Clear();

            string topLine = GetLine(Title);
            Console.WriteLine(topLine);

            //Prints the selections with correct positioning in the center
            for (int i = 0; i < selections.Count; i++)
            {
                string selectionString = "|";
                for (int j = 0; j < topLine.Length / 2 - 6; j++) selectionString += ' ';
                selectionString += $"{i + 1}. {selections[i].Name}";
                int spacesRemaining = topLine.Length - selectionString.Length;
                for (int k = 1; k < spacesRemaining; k++) selectionString += " ";
                selectionString += "|";

                Console.WriteLine(selectionString);
            }
            for (int i = 0; i < Title.Length + 2; i++) bottomLine += '-';
            Console.WriteLine($"+-----{bottomLine}-----+");
        }

        protected virtual int SelectionInputHandler(int amount)
        {
            Console.CursorVisible = true;

            int selection = 0;

            while (selection <= 0 || selection > amount)
            {
                try
                {
                    Console.WriteLine($" Selection(1-{amount}) ");
                    Console.Write(" = ");
                    selection = int.Parse(Console.ReadLine());
                }
                catch (Exception) { continue; }
                break;
            }

            return selection;
        }

        protected virtual void InputValue(string txt, ref int variable)
        {
            Console.Clear();
            Console.Write(txt);

            int value = 0;

            while (value <= 0 || value > 50)
            {
                try
                {
                    value = int.Parse(Console.ReadLine());
                }
                catch (Exception) { continue; }
                break;
            }

            variable = value;
        }

        protected virtual void DisplaySelection(int num)
        {
            Console.CursorVisible = false;

            Console.WriteLine("+----------------+");
            Console.WriteLine($"|  {num} selected..  |");
            Console.WriteLine("+----------------+");

            Thread.Sleep(500);

            Console.Clear();
        }

        private string GetLine(string s)
        {
            return $"+----- {s} -----+";
        }
    }

    class Selection
    {
        public string Name { get; private set; }

        public Selection(string name)
        {
            Name = name;
        }
    }
}
