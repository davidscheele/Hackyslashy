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

        Enemy bigTestSlime; //A simply testsprite

        SpriteFont debugFont;
        bool touching;
        String debug01String = "0";
        String debug02String = "0";
        String debug03String = "0";
        String debug04String = "0";
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
            bigTestSlime = new BigSlime(2);
            enemyList.Add(bigTestSlime);
            addSlimes(5);




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
            bigTestSlime.LoadContent(this.Content);
            loadEnemies();


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


            //debug01String = enemyList[0].Bounds.ToString();
            //debug02String = enemyList[1].Bounds.ToString();
            //debug03String = enemyList[2].Bounds.ToString();
            //debug04String = enemyList[3].Bounds.ToString();

            checkIntersect();

            TouchPanel.EnabledGestures =
                GestureType.Tap |
                GestureType.DoubleTap |
                GestureType.Flick;

            swordSprite.snapTo(heroSprite.Center);
            bigTestSlime.runTo(heroSprite.Center);
            runEnemies();


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




        private void HandleBoardTouch(TouchLocation touch)
        {
            heroSprite.runTo(touch.Position);
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
            while (ammount > 0)
            {
                Enemy slime = new Slime(new Vector2(20, ammount * 100));
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
        private void checkIntersect()
        {
            foreach (Enemy enemy in enemyList)
            {
                if (enemy.Bounds.Intersects(heroSprite.Bounds))
                {
                    enemy.die();
                }
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
            bigTestSlime.Draw(this.spriteBatch);
            drawEnemies();
            
            spriteBatch.DrawString(debugFont, debug01String, new Vector2(10, 10), Color.Black);
            spriteBatch.DrawString(debugFont, debug02String, new Vector2(10, 30), Color.Black);
            spriteBatch.DrawString(debugFont, debug04String, new Vector2(10, 50), Color.Black);
            spriteBatch.DrawString(debugFont, debug03String, new Vector2(10, 70), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
