using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;

namespace MineCifter {
    public class GameImpl : Game {

        public static GameImpl instance;
        private SpriteBatch batch;
        public Board Board;
        public static BitmapFont font;

        public GameImpl() {
            this.Content.RootDirectory = "Content";
            new GraphicsDeviceManager(this) {
                PreferredBackBufferWidth = (int) (Board.Width * Board.Scale),
                PreferredBackBufferHeight = (int) (Board.Height * Board.Scale)
            };
            this.IsMouseVisible = true;
        }

        
        protected override void LoadContent() {
            this.batch = new SpriteBatch(this.GraphicsDevice);
            this.Board = new Board();
            font = this.Content.Load<BitmapFont>("font");
        }
        
        

        protected override void Draw(GameTime gameTime) {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            this.Board.Draw(this.batch, this.GraphicsDevice.Viewport);
        }

        protected override void Update(GameTime gameTime) {
            InputHandler.Update(this.Board, this);
        }

    }
}