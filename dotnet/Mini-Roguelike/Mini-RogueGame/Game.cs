using System;
using System.IO;
using ConsoleMap;
using Mini_Rogue;
using Utils;

namespace Mini_RogueGame
{
    public class Game
    {
        private readonly Map _map;
        private readonly Rogue _rogue;

        public Game(string pathToMap)
        {
            if (!File.Exists(pathToMap))
            {
                throw new FileNotFoundException();
            }

            _map = new Map(File.OpenText(pathToMap));
            var flag = true;
            for (var i = 0; i < _map.Height && flag; ++i)
            {
                for (var j = 0; j < _map.Width; ++j)
                {
                    var curPoint = new Point {X = i, Y = j};
                    if (_map.IsPosFree(curPoint))
                    {
                        _rogue = new Rogue(curPoint);
                        flag = false;
                        break;
                    }
                }
            }

            if (_rogue != null)
            {
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
