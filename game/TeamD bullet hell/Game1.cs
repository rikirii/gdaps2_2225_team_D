using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TeamD_bullet_hell
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont arialBold30;
        private Texture2D tempWallpaper;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            arialBold30 = Content.Load <SpriteFont>("arial");
            tempWallpaper = Content.Load<Texture2D>("temp");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(tempWallpaper, new Rectangle(0,0, _graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height), Color.White);

            //temp title. do not take our title seriously pls -Ricky Y
            _spriteBatch.DrawString(arialBold30, "<Galatic Inferno: BulletStorm>", new Vector2(20, 20), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}