using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Hero
{
    private Vector2 position = new Vector2(200, 200);
    public Vector2 Center
    {
        get
        {
            Vector2 _position = position;
            _position.X = _position.X + (this.Width / 2);
            _position.Y = _position.Y + (this.Height / 2);
            return _position;
        }
        set
        {
            position.X = value.X - (this.Width / 2);
            position.Y = value.Y - (this.Height / 2);
        }
    }
    public Vector2 TopLeft
    {
        get
        {
            return position;
        }
    }
    private Rectangle bounds;
    public Rectangle Bounds
    {
        get
        {
            return bounds;
        }
        set
        {
            bounds = value;
        }
    }

    public Texture2D Texture
    {
        get
        {
            return texture;
        }
        set
        {
            texture = value;
        }
    }
    private Texture2D texture;
    public float Height
    {
        get
        {
            return this.texture.Height;
        }
    }
    public float Width
    {
        get
        {
            return this.texture.Width;
        }
    }
    private int runSpeed = 10;
    private int runDelayCounter = 0;

	public Hero()
	{
        

	}

    public void LoadContent(ContentManager theContentManager, string theAssetName)
    {
        this.texture = theContentManager.Load<Texture2D>(theAssetName);
    }
    public void Draw(SpriteBatch theSpriteBatch)
    {
        theSpriteBatch.Draw(texture, position, Color.White);
        this.Bounds = new Rectangle((int)this.TopLeft.X, (int)this.TopLeft.Y, (int)this.Width, (int)this.Height);
    }

    public void clearRunDelay()
    {
        this.runDelayCounter = 0;
    }

    public void runTo(Vector2 _destination)
    {
        if (this.runDelayCounter >= 5)
        {
            float xPercentage = calculateXPercentage(_destination);
            float yPercentage = 1 - xPercentage;

            if (this.Center.X > _destination.X)
            {
                position.X = position.X - (runSpeed * xPercentage);
            }
            if (this.Center.X < _destination.X)
            {
                position.X = position.X + (runSpeed * xPercentage);
            }
            if (this.Center.Y > _destination.Y)
            {
                position.Y = position.Y - (runSpeed * yPercentage);
            }
            if (this.Center.Y < _destination.Y)
            {
                position.Y = position.Y + (runSpeed * yPercentage);
            }
            this.bounds.X = (int)position.X;
            this.bounds.Y = (int)position.Y;
        }
        else
        {
            this.runDelayCounter++;
        }

        //this.position = touchPosition;
    }
    public void jumpTo(Vector2 _destination)
    {
        this.Center = _destination;
        this.bounds.X = (int)position.X;
        this.bounds.Y = (int)position.Y;
    }

    private float calculateXPercentage(Vector2 _destination)
    {

        double xPerc = 0;
        double xRel = 0;
        double yRel = 0;
        double fullRel;


        if (this.Center.X > _destination.X)
        {
            xRel = this.Center.X - _destination.X;
        }
        if (this.Center.X < _destination.X)
        {
            xRel = _destination.X - this.Center.X;
        }
        if (this.Center.Y > _destination.Y)
        {
            yRel = this.Center.Y - _destination.Y;
        }
        if (this.Center.Y < _destination.Y)
        {
            yRel = _destination.Y - this.Center.Y;
        }

        fullRel = xRel + yRel;
        xPerc = ((100 / fullRel) * xRel)/100;

        return (float) xPerc;
    }



}
