using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public abstract class Sprite
{
    private Vector2 position = new Vector2(10, 10);
    private Texture2D mSpriteTexture;
    private int runSpeed = 10;
    private int runDelayCounter = 0;

	public Sprite()
	{
        

	}

    public void LoadContent(ContentManager theContentManager, string theAssetName)
    {
        mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName);
    }
    public void Draw(SpriteBatch theSpriteBatch)
    {
        theSpriteBatch.Draw(mSpriteTexture, position, Color.White);
    }
    public Vector2 getPosition()
    {
        Vector2 _position = position;
        _position.X = _position.X + 25;
        _position.Y = _position.Y + 25;
        return _position;
    }
    public void clearRunDelay()
    {
        runDelayCounter = 0;
    }
    public void runTo(Vector2 touchPosition)
    {
        if (runDelayCounter >= 5)
        {
            float xPercentage = calculateXPercentage(touchPosition);
            float yPercentage = 1 - xPercentage;

            if (position.X + 25 > touchPosition.X)
            {
                position.X = position.X - runSpeed * xPercentage;
            }
            if (position.X + 25 < touchPosition.X)
            {
                position.X = position.X + runSpeed * xPercentage;
            }
            if (position.Y + 25 > touchPosition.Y)
            {
                position.Y = position.Y - runSpeed * yPercentage;
            }
            if (position.Y + 25 < touchPosition.Y)
            {
                position.Y = position.Y + runSpeed * yPercentage;
            }

        }
        else
        {
            runDelayCounter++;
        }

        //this.position = touchPosition;
    }
    public void jumpTo(Vector2 _touchPosition)
    {
        position.X = _touchPosition.X - 25;
        position.Y = _touchPosition.Y - 25;
    }

    private float calculateXPercentage(Vector2 _touchPosition)
    {

        double xPerc = 0;
        double xRel = 0;
        double yRel = 0;
        double fullRel;


        if (position.X + 25 > _touchPosition.X)
        {
            xRel = position.X + 25 - _touchPosition.X;
        }
        if (position.X + 25 < _touchPosition.X)
        {
            xRel = _touchPosition.X - position.X + 25;
        }
        if (position.Y + 25 > _touchPosition.Y)
        {
            yRel = position.Y + 25 - _touchPosition.Y;
        }
        if (position.Y + 25 < _touchPosition.Y)
        {
            yRel = _touchPosition.Y - position.Y + 25;
        }

        fullRel = xRel + yRel;
        xPerc = ((100 / fullRel) * xRel)/100;

        return (float) xPerc;
    }



}
