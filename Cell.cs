using Microsoft.Xna.Framework;

namespace MineCifter {
    public class Cell {

        public Point pos;
        public bool IsBomb;
        public bool IsMarked;
        public bool IsHidden;
        public int AdjBombs;

        public Cell(Point pos, bool isBomb, bool isMarked) {
            this.pos = pos;
            this.IsBomb = isBomb;
            this.IsMarked = isMarked;
            this.IsHidden = true;
        }

        public void GetBombs(Board board) {
            if (!this.IsBomb) {
                for (var y = -1; y <= 1; y++) {
                    for (var x = -1; x <= 1; x++) {
                        var currPos = this.pos + new Point(x, y);
                        if (currPos.X < 0 || currPos.X >= Board.Width || currPos.Y < 0 || currPos.Y >= Board.Height)
                            continue;
                        if (board.grid[currPos.X, currPos.Y].IsBomb)
                            this.AdjBombs++;
                    }
                }
            }
        }

        public void InteractCell(Board board) {
            if (this.IsHidden)
                this.IsHidden = false;
        }


    }
}