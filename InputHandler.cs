using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MineCifter {
    public static class InputHandler {

        private static ButtonState wasLeftPressed;
        private static ButtonState wasRightPressed;

        public static void Update(Board board, GameImpl game) {
            var state = Mouse.GetState();
            var coord = new Point((int) (state.X / Board.Scale), (int) (state.Y / Board.Scale));

            if (state.LeftButton == ButtonState.Pressed && state.LeftButton != wasLeftPressed) {
                //Console.WriteLine((int) (state.X / Board.Scale) + " " + (int) (state.Y / Board.Scale));
                board.Uncover(coord.X, coord.Y);

                if (board.IsGameOver)
                    game.Board = new Board(game.LaunchArgs);

                else if (board.GameOver())
                    board.UncoverAllBombs();
            }
            if (state.RightButton == ButtonState.Pressed && state.RightButton != wasRightPressed) {
                board.Mark(coord.X, coord.Y);
            }
            wasRightPressed = state.RightButton;
            wasLeftPressed = state.LeftButton;
        }

    }
}