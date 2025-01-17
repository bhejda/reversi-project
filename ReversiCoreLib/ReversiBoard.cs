namespace ReversiCoreLib
{
    public enum BoardTileContent: byte
    {
        None = 0,
        Player1 = 1,
        Player2 = 2
    }

    public enum BoardState : int
    {
        Uninitialized = 0,
        NewGame = 1,
        Ingame = 2,
        Full = 3,
    }

    public readonly struct Coordinate (int x, int y)
    {
        public readonly int X = x;
        public readonly int Y = y;

        public Coordinate AddVector(Coordinate vector) => 
            new Coordinate(X + vector.X, Y + vector.Y);
    }

    public class ReversiBoard(int dimension)
    {
        public static IEnumerable<Coordinate> AllDirections()
        {
            for (int x = -1; x <= 1; x++)
                for (int y = -1; y <= 1; y++)
                    if (x!= 0 || y!= 0)
                        yield return new Coordinate(x, y);
        }

        public readonly int Dimension = dimension;
        public int XDimension => Dimension;
        public int YDimension => Dimension;

        private BoardTileContent[,] Board;
        
        public void InitBoard()
        {
            Board = new BoardTileContent[XDimension, YDimension];
            Board[XDimension / 2 - 1, YDimension / 2 - 1] = BoardTileContent.Player1;
            Board[XDimension / 2, YDimension / 2 - 1] = BoardTileContent.Player2;
            Board[XDimension / 2 - 1, YDimension / 2] = BoardTileContent.Player2;
            Board[XDimension / 2, YDimension / 2] = BoardTileContent.Player1;
        }

        public bool InBounds (int x, int y) =>
            x >= 0 && y >= 0 && x < XDimension && y < YDimension;

        public bool IsEmpty(int x, int y) => InBounds(x,y) && Board[x, y] == BoardTileContent.None;

        public bool IsMy(int x, int y, BoardTileContent player) =>
            InBounds(x, y) && Board[x, y] == player;

        public bool IsOtherPlayer(int x, in int y, BoardTileContent player)
        {
            if (player == BoardTileContent.None)
                throw new ArgumentException($"Invalid value {nameof(BoardTileContent.None)} in method IsOtherPlayer", nameof(player));

            return InBounds(x, y) && (int)Board[x, y] == ((int)player % 2) + 1;
        }

        //public CanReverseInDirection
    }
}
