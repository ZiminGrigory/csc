using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mini_Roguelike
{
    // represents map, where * -- means wall, ' ' -- free space, @ -- rogue
    // suppose thate map's area greater or equal than printableWidth x printableHeight
    public class Map
    {
        private readonly char[][] _map;
        private const char RogueChar = '@';
        private const char FreeChar = ' ';
        private const int PrintableWidth = 70;
        private const int PrintableHeight = 26;

        public int Width { get; }
        public int Height { get; }

        public Map(TextReader mapReader)
        {
            try
            {
                var strings = mapReader.ReadToEnd().Split(Environment.NewLine.ToCharArray()
                        , StringSplitOptions.RemoveEmptyEntries);
                Height = strings.Length;
                _map = new char[Height][];
                var widthSet = new HashSet<int>();
                var i = 0;
                foreach (var str in strings)
                {
                    _map[i] = str.ToCharArray();
                    widthSet.Add(_map[i++].Length);
                }

                if (widthSet.Count != 1)
                {
                    throw new FormatException();
                }

                Width = widthSet.First();
            }
            catch (Exception e)
            {
                throw new FormatException($"SMTh goes wrong {e}, wrong format possibly", e);
            }
        }

        public bool IsPosFree(Point pos)
        {
            return 0 <= pos.X && 0 <= pos.Y && pos.X < Height && pos.Y < Width && _map[pos.X][pos.Y] == FreeChar;
        }

        public bool GetRoguePoint(ref Point pos)
        {
            Point freePoint = null;
            for (var i = 0; i < Height; ++i)
            {
                for (var j = 0; j < Width; ++j)
                {
                    if (_map[i][j] == RogueChar)
                    {
                        pos = new Point {X = i, Y = j};
                        return true;
                    }

                    var curPoint = new Point {X = i, Y = j};
                    if (IsPosFree(curPoint) && freePoint == null)
                    {
                        freePoint = curPoint;
                    }
                }
            }

            if (freePoint == null)
            {
                return false;
            }

            pos = freePoint;
            return true;
        }

        // suppose, that robot pos is valid
        // prints map printableWidth x printableHeight with the robot on center (most cases)
        // if there is no robot prints map printableWidth x printableHeight from 0, 0
        public void PrintMapToConsole(bool withRobot, int x, int y)
        {
            if (withRobot)
            {
                _map[x][y] = RogueChar;
                int x0 = x - PrintableHeight / 2 < 0 ? 0 : x - PrintableHeight / 2;
                int x1 = x0 + PrintableHeight > Height ? Height : x0 + PrintableHeight;
                int y0 = y - PrintableWidth / 2 < 0 ? 0 : y - PrintableWidth / 2;
                int y1 = y0 + PrintableWidth > Width ? Width : y0 + PrintableWidth;
                PrintMapToConsole(x0, y0, x1, y1);
                _map[x][y] = FreeChar;
            }
            else
            {
                PrintMapToConsole(0, 79, 0, 29);
            }
        }

        private void PrintMapToConsole(int x0, int y0, int x1, int y1)
        {
            for (int i = x0; i < x1; ++i)
            {
                for (int j = y0; j < y1; ++j)
                {
                    Console.Write(_map[i][j]);
                }

                Console.Write(Environment.NewLine);
            }
        }
    }
}
