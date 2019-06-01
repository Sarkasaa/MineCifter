using Microsoft.Xna.Framework.Input;

namespace MineCifter {
    public static class InputHandler {

        private static ButtonState wasLeftPressed;
        
        public static bool Update(Board board) {
            var change = false;
            var state = Mouse.GetState();

            if (state.LeftButton == ButtonState.Pressed && state.LeftButton != wasLeftPressed) {
                //Console.WriteLine((int) (state.X / Board.Scale) + " " + (int) (state.Y / Board.Scale));
                board.grid[(int) (state.X / Board.Scale), (int) (state.Y / Board.Scale)].IsHidden = false;

                change = true;
            }
            wasLeftPressed = state.LeftButton;
            return change;
        }

    }
}