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
        Hero heroSprite; //The Sprite controllable by the player
        Sword swordSprite; //The Sprite connected to the hero sprite

        Enemy testSlime; //A simple testsprite
        Enemy bigTestSlime; //A simply testsprite

        Rectangle heroBounds; //invisible boundaries around the hero
        Rectangle enemyBounds; //invisible boundaries around the enemy

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
            heroSprite = new Hero();
            swordSprite = new Sword();
            testSlime = new Slime();
            bigTestSlime = new BigSlime(2);
            //addSlimes(5);




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
            heroSprite.LoadContent(this.Content, "face");
            swordSprite.LoadContent(this.Content, "sword");
            testSlime.LoadContent(this.Content);
            bigTestSlime.LoadContent(this.Content);
            loadEnemies();


            heroBounds = new Rectangle(
                (int)(heroSprite.TopLeft.X), 
                (int)(heroSprite.TopLeft.Y), 
                (int)(heroSprite.Width), 
                (int)(heroSprite.Height));
            enemyBounds = new Rectangle(
                (int)(testSlime.TopLeft.X),
                (int)(testSlime.TopLeft.Y),
                (int)(testSlime.Width),
                (int)(testSlime.Height));

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

            updateEnemyBounds(testSlime.TopLeft);
            enemyRectangleString = enemyBounds.ToString();
            heroRectangleString = heroBounds.ToString();



            TouchPanel.EnabledGestures =
                GestureType.Tap |
                GestureType.DoubleTap |
                GestureType.Flick;

            swordSprite.snapTo(heroSprite.Center);
            testSlime.runTo(heroSprite.Center);
            bigTestSlime.runTo(heroSprite.Center);
            runEnemies();


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
                _enemy.runTo(heroSprite.Center);
            }

            if (swingdir)
            {
                swordSprite.lswing();
            }
            else
            {
                swordSprite.swing();
            }

            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();

                switch (gesture.GestureType)
                {
                    case(GestureType.DoubleTap):
                        heroSprite.jumpTo(gesture.Position);
                        break;
                    
                    case(GestureType.Flick):
                        debugString = gesture.Delta.ToString();
                        swingdir = swordSprite.swingStart(gesture.Delta);
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
                heroSprite.clearRunDelay();
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
            heroSprite.runTo(touch.Position);
            updateHeroBounds(heroSprite.Center);
        }
        private void HandleResetTouch(TouchLocation touch)
        {
        }
        private void addToEnemyList(Enemy enemy)
        {
            enemyList.Add(enemy);
        }
        private void addSlimes(int _ammount)
        {
            int ammount = _ammount;
            while (ammount >= 0)
            {
                Enemy slime = new Slime();
                addToEnemyList(slime);
                ammount--;
            }

        }
        private void loadEnemies()
        {
            foreach(Enemy enemy in enemyList){
                enemy.LoadContent(this.Content);
            }

        }
        private void drawEnemies()
        {
            foreach (Enemy enemy in enemyList)
            {
                enemy.Draw(this.spriteBatch);
            }
        }
        private void runEnemies()
        {
            foreach (Enemy enemy in enemyList)
            {
                enemy.runTo(heroSprite.Center);
            }
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
            heroSprite.Draw(this.spriteBatch);
            swordSprite.Draw(this.spriteBatch);
            testSlime.Draw(this.spriteBatch);
            bigTestSlime.Draw(this.spriteBatch);
            drawEnemies();
            
            spriteBatch.DrawString(debugFont, debugString, new Vector2(10, 10), Color.Black);
            spriteBatch.DrawString(debugFont, intersectString, new Vector2(10, 30), Color.Black);
            spriteBatch.DrawString(debugFont, heroRectangleString, new Vector2(10, 50), Color.Black);
            spriteBatch.DrawString(debugFont, enemyRectangleString, new Vector2(10, 70), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
