using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;

namespace Blorgorp
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public ArrayList gameObjects = new ArrayList();
        Texture2D smiley;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            MovementChain chain = new MovementChain();
            Movement movement = new Movement();
            movement.AddMovement(Movement.CreateLine(new Vector2(30, 50), new Vector2(200, 300), 0.01f));            
            movement.timeUpper = 100;
            Movement movement1 = new Movement();
            movement1.AddMovement(Movement.CreateLine(new Vector2(200, 300), new Vector2(300, 100), 0.01f));
            movement1.timeUpper = 100;
            Movement movement2 = Movement.CreateOrbit(new Vector2(250, 100), 50, 100);
            movement2.timeUpper = 100;
            movement2.roundMethod = TimeRoundMethod.REVERSE_WRAP;
            chain.AddMovement(movement).AddMovement(movement1).AddMovement(movement2);
            gameObjects.Add(new GameObject(new Vector2(), chain, this));
           

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            smiley = Content.Load<Texture2D>("Smiley");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            foreach(GameObject gameObject in gameObjects)
            {
                gameObject.Tick();
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            foreach (GameObject gameObject in gameObjects)
            {
                _spriteBatch.Draw(smiley, gameObject.position, Color.White);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
