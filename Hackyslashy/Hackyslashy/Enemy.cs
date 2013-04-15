using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public abstract class Enemy
{
    private Vector2 position = new Vector2(100, 100);
    private Texture2D spriteTexture;

    private int runSpeed;
    private String assetName = "slime";

	public Enemy()
	{
        

	}

    public void LoadContent(ContentManager _contentManager)
    {
        spriteTexture = _contentManager.Load<Texture2D>(assetName);
    }
    public void Draw(SpriteBatch _spriteBatch)
    {
        _spriteBatch.Draw(spriteTexture, position, Color.White);
    }
    public Vector2 getPosition()
    {
        Vector2 _position = position;
        _position.X = _position.X + (spriteTexture.Width/2);
        _position.Y = _position.Y + (spriteTexture.Height/2);
        return _position;
    }

    public Vector2 getTopPosition()
    {
        return position;
    }
    public void setRunSpeed(int _speed)
    {
        runSpeed = _speed;
    }
    public float getHeight()
    {
        return this.spriteTexture.Height;
    }
    public float getWidth()
    {
        return this.spriteTexture.Width;
    }
    public void setPosition(Vector2 _position)
    {
        this.position = _position;
    }

    public void runTo(Vector2 _destination)
    {
            float xPercentage = calculateXPercentage(_destination);
            float yPercentage = 1 - xPercentage;

            if (position.X + (spriteTexture.Width / 2) > _destination.X)
            {
                position.X = position.X - runSpeed * xPercentage;
            }
            if (position.X + (spriteTexture.Width / 2) < _destination.X)
            {
                position.X = position.X + runSpeed * xPercentage;
            }
            if (position.Y + (spriteTexture.Height / 2) > _destination.Y)
            {
                position.Y = position.Y - runSpeed * yPercentage;
            }
            if (position.Y + (spriteTexture.Height / 2) < _destination.Y)
            {
                position.Y = position.Y + runSpeed * yPercentage;
            }
    }
    public void jumpTo(Vector2 _destination)
    {
        position.X = _destination.X - (spriteTexture.Width / 2);
        position.Y = _destination.Y - (spriteTexture.Height / 2);
    }

    private float calculateXPercentage(Vector2 _destination)
    {

        double xPerc = 0;
        double xRel = 0;
        double yRel = 0;
        double fullRel;


        if (position.X + 25 > _destination.X)
        {
            xRel = position.X + 25 - _destination.X;
        }
        if (position.X + 25 < _destination.X)
        {
            xRel = _destination.X - position.X + 25;
        }
        if (position.Y + 25 > _destination.Y)
        {
            yRel = position.Y + 25 - _destination.Y;
        }
        if (position.Y + 25 < _destination.Y)
        {
            yRel = _destination.Y - position.Y + 25;
        }

        fullRel = xRel + yRel;
        xPerc = ((100 / fullRel) * xRel)/100;

        return (float) xPerc;
    }



}
