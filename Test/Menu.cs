using System;
using System.Collections.Generic;
using System.Threading;

namespace Snake
{
    class Menu : UIBuilder
    {
        public int Display()
        {
            List<Selection> selections = new List<Selection>()
            {
                new Selection("Play"),
                new Selection("Settings"),
                new Selection("Exit")
            };

            BuildSelection("MENU", selections);

            int num = SelectionInputHandler(selections.Count);

            DisplaySelection(num);

            return num;
        }
    }
}
