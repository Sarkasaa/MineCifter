using Microsoft.Xna.Framework.Input;

namespace MineCifter {
    public static class InputHandler {

        private static ButtonState wasLeftPressed;

        public static void Update(Board board, GameImpl game) {
            var state = Mouse.GetState();

            if (state.LeftButton == ButtonState.Pressed && state.LeftButton != wasLeftPressed) {
                //Console.WriteLine((int) (state.X / Board.Scale) + " " + (int) (state.Y / Board.Scale));
                board.Uncover((int) (state.X / Board.Scale), (int) (state.Y / Board.Scale));

                if (board.IsGameOver)
                    game.Board = new Board();

                else if (board.GameOver())
                    board.UncoverAllBombs();
            }
            wasLeftPressed = state.LeftButton;
        }

    }
}