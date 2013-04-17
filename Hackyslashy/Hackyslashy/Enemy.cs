using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public abstract class Enemy
{
    private Vector2 position = new Vector2(100, 100);
    public Vector2 Center
    {
        get
        {
            Vector2 _position = position;
            _position.X = _position.X + (texture.Width / 2);
            _position.Y = _position.Y + (texture.Height / 2);
            return _position;
        }
    }
    public Vector2 TopLeft
    {
        get
        {
            return position;
        }
    }
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

    private Texture2D texture;
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

    private int runSpeed;
    public int RunSpeed
    {
        get
        {
            return runSpeed;
        }
        set
        {
            runSpeed = value;
        }

    }
    private String assetName = "";
    public String AssetName
    {
        get
        {
            return assetName;
        }
        set
        {
            assetName = value;
        }
    }

    private float xPerc = 0;
    public String XPerc
    {
        get
        {
            return xPerc.ToString();
        }
    }
    private float yPerc = 0;
    public String YPerc
    {
        get
        {
            return yPerc.ToString();
        }
    }
    public String Perc
    {
        get
        {
            float _perc = yPerc + xPerc;
            return _perc.ToString();
        }
    }
	public Enemy()
	{
        

	}

    public void LoadContent(ContentManager _contentManager)
    {
        texture = _contentManager.Load<Texture2D>(assetName);
    }
    public void Draw(SpriteBatch _spriteBatch)
    {
        _spriteBatch.Draw(texture, position, Color.White);
    }

    public void setPosition(Vector2 _position) //sets the upper left corner to the given value.
    {
        this.position = _position;
    }

    public void runTo(Vector2 _destination) //lets enemy advance towards the given value. enemy will run the same speed in any direction.
    {
            float xPercentage = calculateXPercentage(_destination);
            float yPercentage = 1 - xPercentage;
            xPerc = xPercentage;
            yPerc = yPercentage;

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
    }
    public void jumpTo(Vector2 _destination) //lets enemy hop to the point of the given value.
    {
        position.X = _destination.X - (texture.Width / 2);
        position.Y = _destination.Y - (texture.Height / 2);
    }

    private float calculateXPercentage(Vector2 _destination) //calculates how much percentage of the rectangle from given value to position of enemy is x. is needed for the run command.
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
