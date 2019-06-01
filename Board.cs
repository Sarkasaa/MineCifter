using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;

namespace MineCifter {
    public class Board {

        public const int Width = 20;
        public const int Height = 20;
        public Cell[,] grid;
        public Random rand;
        public int Diff = 7;
        public const float Scale = 32;
        public bool IsGameOver;

        public Board() {
            this.grid = new Cell[Width, Height];
            this.rand = new Random();
            this.Fill();
            foreach (var cell in this.AllCells()) {
                cell.GetBombs(this);
            }
        }

        public bool GameOver() {
            foreach (var cell in this.AllCells()) {
                if (cell.IsBomb && !cell.IsHidden) {
                    this.IsGameOver = true;
                    return true;
                }
            }
            return false;
        }

        public void UncoverAllBombs() {
            foreach (var cell in this.AllCells()) {
                if (cell.IsBomb)
                    cell.IsHidden = false;
            }
        }

        public void Fill() {
            for (var y = 0; y < Height; y++) {
                for (var x = 0; x < Width; x++) {
                    this.grid[x, y] = new Cell(new Point(x, y), this.rand.Next(0, this.Diff) == 1, false);
                }
            }
        }

        public IEnumerable<Cell> AllCells() {
            for (var y = 0; y < Height; y++) {
                for (var x = 0; x < Width; x++) {
                    yield return this.grid[x, y];
                }
            }
        }

        public void Uncover(int x, int y) {
            var cell = this.grid[x, y];
            cell.IsHidden = false;
            if (cell.AdjBombs < 1) {
                for (var yOff = -1; yOff <= 1; yOff++) {
                    for (var xOff = -1; xOff <= 1; xOff++) {
                        var currPos = new Point(xOff + x, yOff + y);
                        if (currPos.X < 0 || currPos.X >= Width || currPos.Y < 0 || currPos.Y >= Height)
                            continue;
                        var currCell = this.grid[currPos.X, currPos.Y];
                        if (currCell.IsBomb || !currCell.IsHidden)
                            continue;
                        this.Uncover(currPos.X,currPos.Y);
                    }
                }
            }
        }

        public void Draw(SpriteBatch batch, Viewport screen) {
            var offset = new Vector2(8, 0) / 32F;
            var matrix = Matrix.CreateScale(Scale);
            batch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: matrix);
            foreach (var cell in this.AllCells()) {
                batch.FillRectangle(cell.pos.ToVector2(), new Size2(1, 1), cell.IsHidden ? Color.Gray : cell.IsBomb ? Color.DarkRed : Color.DarkGray);
                batch.DrawRectangle(cell.pos.ToVector2(), new Size2(1, 1), Color.Black, 1 / 32F);
                if (!cell.IsBomb && cell.AdjBombs > 0 && !cell.IsHidden)
                    batch.DrawString(GameImpl.font, cell.AdjBombs.ToString(), cell.pos.ToVector2() + offset, Color.Black, 0, Vector2.Zero, 1 / 32F, SpriteEffects.None, 0);
            }
            batch.End();
        }

    }
}