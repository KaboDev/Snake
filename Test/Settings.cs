using System.Collections.Generic;
using System.Runtime;

namespace Snake_Game
{
    class Settings : UIBuilder
    {
        public int Width => _width;
        public int Height => _height;
        public int Speed => _speed;

        private int _width;
        private int _height;
        private int _speed;

        private int _lastWidth;
        private int _lastHeight;
        private int _lastSpeed;

        public Settings(int width, int height, int speed)
        {
            _width = width;
            _height = height;
            _speed = speed;
        }

        public int Display()
        {
            List<Selection> selections = new List<Selection>()
            {
                new Selection("Size"),
                new Selection("Speed"),
                new Selection("Cancel"),
                new Selection("Apply"),
            };

            BuildSelection("Settings", selections);

            int num = SelectionInputHandler(selections.Count);

            DisplaySelection(num);

            return num;
        }

        public void SizeSetting()
        {
            List<Selection> selections = new List<Selection>()
            {
                new Selection($"Width ({Width})"),
                new Selection($"Height ({Height})"),
                new Selection($"Back"),
            };

            BuildSelection("Settings", selections);
            int num = SelectionInputHandler(selections.Count);

            if (num == 1)
                InputValue("Specify a width(1-50): ", ref _width);
            else if (num == 2) InputValue("Specify a height(1-50): ", ref _height);
        }

        public void SpeedSetting()
        {
            List<Selection> selections = new List<Selection>()
            {
                new Selection($"Speed ({_speed})"),
                new Selection($"Back"),
            };

            BuildSelection("Settings", selections);
            int num = SelectionInputHandler(selections.Count);

            if (num == 1)
                InputValue("Set the speed: ", ref _speed);
        }

        public void SetReset()
        {
            _lastWidth = _width;
            _lastHeight = _height;
            _lastSpeed = _speed;
        }

        public void Reset()
        {
            _width = _lastWidth;
            _height = _lastHeight;
            _speed = _lastSpeed;
        }
    }
}
