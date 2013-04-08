using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Hackyslashy
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;



         // Sprites
        Hero mSprite;
        Swordsprite sSprite;

        Enemy testSlime;

        Rectangle heroBounds;
        Rectangle enemyBounds;

        SpriteFont debugFont;
        bool touching;
        String debugString = "0";
        String intersectString = "0";
        String enemyRectangleString = "0";
        String heroRectangleString = "0";
        bool swingdir = true;

        List<Enemy> enemyList = new List<Enemy>();
        


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            mSprite = new Hero();
            sSprite = new Swordsprite();
            testSlime = new Slime(5);


            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            mSprite.LoadContent(this.Content, "face");
            sSprite.LoadContent(this.Content, "sword");
            testSlime.LoadContent(this.Content, "slime");

            heroBounds = new Rectangle(
                (int)(mSprite.getPosition().X - (mSprite.getWidth()/2)), 
                (int)(mSprite.getPosition().Y - (mSprite.getHeight()/2)), 
                (int)(mSprite.getWidth()), 
                (int)(mSprite.getHeight()));
            enemyBounds = new Rectangle(
                (int)(testSlime.getPosition().X - (testSlime.getWidth() / 2)),
                (int)(testSlime.getPosition().Y - (testSlime.getHeight() / 2)),
                (int)(testSlime.getWidth()),
                (int)(testSlime.getHeight()));

            debugFont = Content.Load<SpriteFont>("SpriteFont1");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            updateEnemyBounds(testSlime.getPosition());
            enemyRectangleString = enemyBounds.ToString();
            heroRectangleString = heroBounds.ToString();

            TouchPanel.EnabledGestures =
                GestureType.Tap |
                GestureType.DoubleTap |
                GestureType.Flick;

            sSprite.snapTo(mSprite.getPosition());
            testSlime.runTo(mSprite.getRoundedPosition());

            if (heroBounds.Intersects(enemyBounds))
            {
                intersectString = "Intersection!";

            }
            else
            {
                intersectString = "No Intersection";
            }


            foreach (Enemy _enemy in enemyList)
            {
                _enemy.runTo(mSprite.getPosition());
            }

            if (swingdir)
            {
                sSprite.lswing();
            }
            else
            {
                sSprite.swing();
            }

            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();

                switch (gesture.GestureType)
                {
                    case(GestureType.DoubleTap):
                        mSprite.jumpTo(gesture.Position);
                        break;
                    
                    case(GestureType.Flick):
                        debugString = gesture.Delta.ToString();
                        swingdir = sSprite.swingStart(gesture.Delta);
                        break;
                        
                }

            }



            TouchCollection touches = TouchPanel.GetState();
            if (!touching && touches.Count > 0)
            {
                touching = true;
            }
            else if (touches.Count == 0)
            {
                touching = false;
                mSprite.clearRunDelay();
            }

            if (touching)
            {
                TouchLocation touch = touches.First();
                HandleBoardTouch(touch);
                HandleResetTouch(touch);
            }


            base.Update(gameTime);
        }
        private void updateHeroBounds(Vector2 _position)
        {
            heroBounds.X = (int)_position.X;
            heroBounds.Y = (int)_position.Y;

        }
        private void updateEnemyBounds(Vector2 _position)
        {
            enemyBounds.X = (int)_position.X;
            enemyBounds.Y = (int)_position.Y;

        }

        private void HandleBoardTouch(TouchLocation touch)
        {
            mSprite.runTo(touch.Position);
            updateHeroBounds(mSprite.getTopPosition());

        }
        private void HandleResetTouch(TouchLocation touch)
        {
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            mSprite.Draw(this.spriteBatch);
            sSprite.Draw(this.spriteBatch);
            testSlime.Draw(this.spriteBatch);
            spriteBatch.DrawString(debugFont, debugString, new Vector2(10, 10), Color.Black);
            spriteBatch.DrawString(debugFont, intersectString, new Vector2(10, 30), Color.Black);
            spriteBatch.DrawString(debugFont, heroRectangleString, new Vector2(10, 50), Color.Black);
            spriteBatch.DrawString(debugFont, enemyRectangleString, new Vector2(10, 70), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
