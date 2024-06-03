using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CSharp2Project
{
    public class MainMenuScreen : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        // Button positions
        Rectangle playButton;
        Rectangle highScoresButton;
        Rectangle quitButton;

        // Mouse state
        MouseState currentMouseState;
        MouseState previousMouseState;

        // Background image
        Texture2D backgroundImage;

        public MainMenuScreen()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load the font
            font = Content.Load<SpriteFont>("menuFont");

            // Load the background image
            backgroundImage = Content.Load<Texture2D>("background");

            // Calculate button size based on font size
            Vector2 buttonSize = font.MeasureString("Play Game");
            int buttonWidth = (int)buttonSize.X + 20; // Add padding
            int buttonHeight = (int)buttonSize.Y + 20;

            // Initialize buttons with dynamic sizes
            playButton = new Rectangle(300, 200, buttonWidth, buttonHeight);
            highScoresButton = new Rectangle(300, 300, buttonWidth, buttonHeight);
            quitButton = new Rectangle(300, 400, buttonWidth, buttonHeight);
        }

        protected override void Update(GameTime gameTime)
        {
            currentMouseState = Mouse.GetState();

            // Check if the left mouse button was clicked
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                Point mousePosition = currentMouseState.Position;

                if (playButton.Contains(mousePosition))
                {
                    // Start the game
                    // Add logic to transition to the game screen
                }
                else if (highScoresButton.Contains(mousePosition))
                {
                    // Show high scores
                    // Add logic to transition to the high scores screen
                }
                else if (quitButton.Contains(mousePosition))
                {
                    Exit();
                }
            }

            previousMouseState = currentMouseState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Clear the screen with a color
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // Draw the background image
            spriteBatch.Draw(backgroundImage, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);

            // Draw the title
            string title = "Main Menu";
            Vector2 titleSize = font.MeasureString(title);
            Vector2 titlePosition = new Vector2(
                (GraphicsDevice.Viewport.Width - titleSize.X) / 2,
                50 // Adjust the Y position as needed
            );
            Color titleColor = Color.Gold;
            spriteBatch.DrawString(font, title, titlePosition, titleColor);

            // Draw the buttons as rounded rectangles
            DrawButton(playButton, "Play Game");
            DrawButton(highScoresButton, "High Scores");
            DrawButton(quitButton, "Quit");

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawButton(Rectangle buttonRect, string buttonText)
        {
            // Draw the rounded rectangle
            DrawRoundedRectangle(buttonRect, 20, Color.OrangeRed);

            // Draw the button text
            Vector2 textSize = font.MeasureString(buttonText);
            Vector2 textPosition = new Vector2(
                buttonRect.X + (buttonRect.Width - textSize.X) / 2,
                buttonRect.Y + (buttonRect.Height - textSize.Y) / 2
            );
            spriteBatch.DrawString(font, buttonText, textPosition, Color.White);
        }

        private void DrawRoundedRectangle(Rectangle rect, int radius, Color color)
        {
            Texture2D texture = CreateCircleTexture(radius, color);

            // Draw corners
            spriteBatch.Draw(texture, new Vector2(rect.Left, rect.Top), color); // top-left
            spriteBatch.Draw(texture, new Vector2(rect.Right - radius * 2, rect.Top), color); // top-right
            spriteBatch.Draw(texture, new Vector2(rect.Left, rect.Bottom - radius * 2), color); // bottom-left
            spriteBatch.Draw(texture, new Vector2(rect.Right - radius * 2, rect.Bottom - radius * 2), color); // bottom-right

            // Draw edges
            spriteBatch.Draw(CreateRectangleTexture(rect.Width - radius * 2, radius, color), new Rectangle(rect.Left + radius, rect.Top, rect.Width - radius * 2, radius), color); // top
            spriteBatch.Draw(CreateRectangleTexture(rect.Width - radius * 2, radius, color), new Rectangle(rect.Left + radius, rect.Bottom - radius, rect.Width - radius * 2, radius), color); // bottom
            spriteBatch.Draw(CreateRectangleTexture(radius, rect.Height - radius * 2, color), new Rectangle(rect.Left, rect.Top + radius, radius, rect.Height - radius * 2), color); // left
            spriteBatch.Draw(CreateRectangleTexture(radius, rect.Height - radius * 2, color), new Rectangle(rect.Right - radius, rect.Top + radius, radius, rect.Height - radius * 2), color); // right

            // Draw center
            spriteBatch.Draw(CreateRectangleTexture(rect.Width - radius * 2, rect.Height - radius * 2, color), new Rectangle(rect.Left + radius, rect.Top + radius, rect.Width - radius * 2, rect.Height - radius * 2), color); // center
        }

        private Texture2D CreateCircleTexture(int radius, Color color)
        {
            int diameter = radius * 2;
            Texture2D texture = new Texture2D(GraphicsDevice, diameter, diameter);
            Color[] data = new Color[diameter * diameter];

            float radiusSquared = radius * radius;

            for (int x = 0; x < diameter; x++)
            {
                for (int y = 0; y < diameter; y++)
                {
                    int index = x * diameter + y;
                    Vector2 position = new Vector2(x - radius, y - radius);
                    if (position.LengthSquared() <= radiusSquared)
                    {
                        data[index] = color;
                    }
                    else
                    {
                        data[index] = Color.Transparent;
                    }
                }
            }

            texture.SetData(data);
            return texture;
        }

        private Texture2D CreateRectangleTexture(int width, int height, Color color)
        {
            Texture2D texture = new Texture2D(GraphicsDevice, width, height);
            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; ++i) data[i] = color;
            texture.SetData(data);
            return texture;
        }
    }
}
