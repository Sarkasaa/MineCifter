using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;

namespace MineCifter {
    public class Board {

        public const int Width = 10;
        public const int Height = 10;
        public Cell[,] grid;
        public Random rand;
        public int Diff = 3;
        public const float Scale = 32;

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
                if (cell.IsBomb && !cell.IsHidden)
                    return true;
            }
            return false;
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


        public void Draw(SpriteBatch batch, Viewport screen) {
            var offset = new Vector2(8, 0) / 32F;
            var matrix = Matrix.CreateScale(Scale);
            batch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: matrix);
            foreach (var cell in this.AllCells()) {
                batch.FillRectangle(cell.pos.ToVector2(), new Size2(1, 1), cell.IsBomb && !cell.IsHidden ? Color.DarkRed : Color.Gray);
                if (!cell.IsBomb && cell.AdjBombs > 0 && !cell.IsHidden)
                    batch.DrawString(GameImpl.font, cell.AdjBombs.ToString(), cell.pos.ToVector2() + offset, Color.Black, 0, Vector2.Zero, 1 / 32F, SpriteEffects.None, 0);
            }
            batch.End();
        }

    }
}