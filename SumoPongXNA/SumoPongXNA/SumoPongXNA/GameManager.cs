using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SumoPongXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameManager : Actor
    {
        public Rectangle arena, edge;
        public Paddle left, right;
        public Label scoreboard, leftScore, rightScore;

        delegate void UpdateFunction(GameTime gameTime);
        UpdateFunction currentUpdate;

        private float timer = 0;
        
        private int leftCount = 0;
        private int rightCount = 0;

        private static GameManager instance;
        public static GameManager Instance { 
            get 
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            } 
        }

        private GameManager()
        {
            ResetGame();
            GoToCountdown();

            scoreboard = new Label();
            scoreboard.transform.position = new Vector2(arena.Center.X - 40, arena.Top - 30);

            leftScore = new Label();
            leftScore.transform.position = new Vector2(arena.Left - 20, arena.Top - 0);

            rightScore = new Label();
            rightScore.transform.position = new Vector2(arena.Right + 10, arena.Top - 0);

            UpdateScore();
        }

        public void ResetGame()
        {
            if (left != null)
            {
                left.CleanUp();
            }
            if (right != null)
            {
                right.CleanUp();
            }

            left = new Paddle(Paddle.PaddleType.Left);
            right = new Paddle(Paddle.PaddleType.Right);

            int width = SumoPong.Instance.GraphicsDevice.Viewport.Width;
            int height = SumoPong.Instance.GraphicsDevice.Viewport.Height;

            arena = new Rectangle(Constants.PADDLE_WIDTH,
                Constants.PADDLE_WIDTH,
                width - Constants.PADDLE_WIDTH * 2,
                height - Constants.PADDLE_WIDTH * 2);

            edge = new Rectangle(0, 0, width, height);

            left.transform.position = new Vector2(arena.X + Constants.KNOCKBACK_DISTANCE, height / 2);
            right.transform.position = new Vector2(arena.X + arena.Width - Constants.KNOCKBACK_DISTANCE - right.width, height / 2);

        }

        void UpdateScore()
        {
            leftScore.text = leftCount.ToString();
            rightScore.text = rightCount.ToString();
        }

        public void GoToMatchEnd()
        {
            ResetGame();
            currentUpdate = Update_MatchEnded;
            timer = 2;
            UpdateScore();
        }

        public void GoToCountdown()
        {
            currentUpdate = Update_BeginningNextMatch;
            timer = 3;
        }

        public void GoToRunGame()
        {
            currentUpdate = Update_Running;
            scoreboard.text = "Game On!";
        }

        public void Update_MatchEnded(GameTime gameTime)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                GoToCountdown();
            }
        }

        public void Update_BeginningNextMatch(GameTime gameTime)
        {
            timer -= Time.deltaTime;
            
            if (timer > 2)
            {
                scoreboard.text = "3..";
            }
            else if(timer > 1)
            {
                scoreboard.text = "2..";
            }
            else if (timer > 0)
            {
                scoreboard.text = "1..";
            }
            else
            {
                GoToRunGame();
            }
        }

        public void Update_Running(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W) ||
                   Keyboard.GetState().IsKeyDown(Keys.A))
            {
                left.MoveUp();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S) ||
                Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                left.MoveDown();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) ||
                Keyboard.GetState().IsKeyDown(Keys.X))
            {
                left.Fire();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                right.MoveUp();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                right.MoveDown();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.NumPad0))
            {
                right.Fire();
            }


            if (left.transform.position.X + left.width <= arena.X)
            {
                scoreboard.text =  "Right wins!";
                rightCount++;
                GoToMatchEnd();
            }
            else if (right.transform.position.X >= arena.Right)
            {
                scoreboard.text = "Left wins!";
                leftCount++;
                GoToMatchEnd();
            }
        }

        public override void Update(GameTime gameTime)
        {
            currentUpdate(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawRectangle(spriteBatch, arena);
            DrawRectangle(spriteBatch, edge);
        }

        void DrawRectangle(SpriteBatch spriteBatch, Rectangle rect)
        {
            Texture2D line = ContentLoader.Texure_White;
            int thickness = 2;
            Rectangle left = new Rectangle(rect.X, rect.Y, thickness, rect.Height);
            Rectangle right = new Rectangle(rect.Right - thickness, rect.Y, thickness, rect.Height);
            Rectangle up = new Rectangle(rect.X, rect.Y, rect.Width, thickness);
            Rectangle down = new Rectangle(rect.X, rect.Bottom - thickness, rect.Width, thickness);

            spriteBatch.Draw(line, left, Color.Beige);
            spriteBatch.Draw(line, right, Color.Beige);
            spriteBatch.Draw(line, up, Color.Beige);
            spriteBatch.Draw(line, down, Color.Beige);
        }
    }
}
