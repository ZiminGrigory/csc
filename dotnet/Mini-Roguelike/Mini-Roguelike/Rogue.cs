namespace Mini_Roguelike
{
    public class Rogue
    {
        public Point Position { get; private set; }
        public int DeltaMove { get; set; }

        public Rogue(Point position)
        {
            Position = position;
            DeltaMove = 1;
        }

        public void MoveTo(Point newPosition)
        {
            Position = newPosition;
        }

        public Point NextPointIfMove(Directions direction)
        {
            switch (direction)
            {
                case Directions.Forward:
                    return new Point{ X = Position.X - DeltaMove, Y = Position.Y };
                case Directions.Backward:
                    return new Point { X = Position.X + DeltaMove, Y = Position.Y };
                case Directions.Left:
                    return new Point { X = Position.X, Y = Position.Y - DeltaMove };
                case Directions.Right:
                    return new Point { X = Position.X, Y = Position.Y + DeltaMove };
                default:
                    return new Point();
            }
        }
    }
}
