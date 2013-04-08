using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Hero
{
    private Vector2 position = new Vector2(200, 200);
    private Texture2D mSpriteTexture;
    private int runSpeed = 10;
    private int runDelayCounter = 0;

	public Hero()
	{
        

	}

    public void LoadContent(ContentManager theContentManager, string theAssetName)
    {
        this.mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName);
    }
    public void Draw(SpriteBatch theSpriteBatch)
    {
        theSpriteBatch.Draw(mSpriteTexture, position, Color.White);
    }
    public Vector2 getPosition()
    {
        Vector2 _position = this.position;
        _position.X = _position.X + (this.mSpriteTexture.Width/2);
        _position.Y = _position.Y + (this.mSpriteTexture.Height/2);
        return _position;
    }
    public Vector2 getTopPosition()
    {
        return position;
    }
    public Vector2 getRoundedPosition()
    {
        Vector2 _position = position;
        _position.X = (int)(_position.X + (mSpriteTexture.Width / 2));
        _position.Y = (int)(_position.Y + (mSpriteTexture.Height / 2));
        return _position;
    }
    public void clearRunDelay()
    {
        this.runDelayCounter = 0;
    }
    public float getHeight()
    {
        return this.mSpriteTexture.Height;
    }
    public float getWidth()
    {
        return this.mSpriteTexture.Width;
    }
    public void runTo(Vector2 touchPosition)
    {
        if (this.runDelayCounter >= 5)
        {
            float xPercentage = calculateXPercentage(touchPosition);
            float yPercentage = 1 - xPercentage;

            if (this.position.X + 25 > touchPosition.X)
            {
                this.position.X = this.position.X - this.runSpeed * xPercentage;
            }
            if (this.position.X + 25 < touchPosition.X)
            {
                this.position.X = this.position.X + this.runSpeed * xPercentage;
            }
            if (this.position.Y + 25 > touchPosition.Y)
            {
                this.position.Y = this.position.Y - this.runSpeed * yPercentage;
            }
            if (this.position.Y + 25 < touchPosition.Y)
            {
                this.position.Y = this.position.Y + this.runSpeed * yPercentage;
            }

        }
        else
        {
            this.runDelayCounter++;
        }

        //this.position = touchPosition;
    }
    public void jumpTo(Vector2 _touchPosition)
    {
        this.position.X = _touchPosition.X - 25;
        this.position.Y = _touchPosition.Y - 25;
    }

    private float calculateXPercentage(Vector2 _touchPosition)
    {

        double xPerc = 0;
        double xRel = 0;
        double yRel = 0;
        double fullRel;


        if (this.position.X + 25 > _touchPosition.X)
        {
            xRel = this.position.X + 25 - _touchPosition.X;
        }
        if (this.position.X + 25 < _touchPosition.X)
        {
            xRel = _touchPosition.X - this.position.X + 25;
        }
        if (this.position.Y + 25 > _touchPosition.Y)
        {
            yRel = this.position.Y + 25 - _touchPosition.Y;
        }
        if (this.position.Y + 25 < _touchPosition.Y)
        {
            yRel = _touchPosition.Y - this.position.Y + 25;
        }

        fullRel = xRel + yRel;
        xPerc = ((100 / fullRel) * xRel)/100;

        return (float) xPerc;
    }



}
