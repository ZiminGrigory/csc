using System;
using System.IO;

namespace Mini_Roguelike
{
    public class Game
    {
        private readonly Map _map;
        private readonly Rogue _rogue;

        public Game(string pathToMap)
        {
            if (!File.Exists(pathToMap))
            {
                throw new FileNotFoundException($"File {pathToMap} doesn't exist or smth wrong with path");
            }

            _map = new Map(File.OpenText(pathToMap));
            Point roguePoint = null;
            if (_map.GetRoguePoint(ref roguePoint))
            {
                _rogue = new Rogue(roguePoint);
                _map.PrintMapToConsole(true, _rogue.Position.X, _rogue.Position.Y);
            }
            else
            {
                throw new ArgumentException("Smth wrong with map, possibly no free space");
            }

            Console.CursorVisible = false;
        }

        public void HandleLeft()
        {
            CheckPositionAndMove(_rogue.NextPointIfMove(Directions.Left));
        }

        public void HandleRight()
        {
            CheckPositionAndMove(_rogue.NextPointIfMove(Directions.Right));
        }

        public void HandleForward()
        {
            CheckPositionAndMove(_rogue.NextPointIfMove(Directions.Forward));
        }

        public void HandleBackward()
        {
            CheckPositionAndMove(_rogue.NextPointIfMove(Directions.Backward));
        }

        private void CheckPositionAndMove(Point newPosition)
        {
            if (!_map.IsPosFree(newPosition))
            {
                return;
            }

            _rogue.MoveTo(newPosition);
            Console.SetCursorPosition(0, 0); // clear looks awful
            _map.PrintMapToConsole(true, newPosition.X, newPosition.Y);
        }
    }
}
