using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Swordsprite
{
    private Vector2 position;
    private Vector2 origin;
    private Texture2D mSpriteTexture;
    private float angle = 0f;
    private float acceleration = 0;
    private bool inSwing = false;

	public Swordsprite()
	{
        

	}
    //public void swingStart()
    //{
    //    inSwing = true;
    //    acceleration = 1;
    //}
    public bool swingStart(Vector2 flickData)
    {
        float yAcc = 0;
        inSwing = true;
        bool direction; //left?
        if (flickData.Y <= 0)
        {
            direction = true;
            yAcc = (float)Math.Round(flickData.Y * -1);
        }
        else
        {
            direction = false;
            yAcc = (float)Math.Round(flickData.Y);
        }

        acceleration = (yAcc / 9000);
        return direction;
    }
    //public void swing()
    //{
    //    if (inSwing)
    //    {
    //        angle = angle + ((float) 0.8 * acceleration);
    //        acceleration = acceleration - (float) 0.05;

    //        if (acceleration <= 0)
    //        {
    //            inSwing = false;
    //        }
    //    }
        
    //}

    public void swing()
    {
        if (inSwing)
        {
            angle = angle + ((float)0.8 * acceleration);
            acceleration = acceleration - (float)0.03;

            if (acceleration <= 0)
            {
                inSwing = false;
            }
        }

    }
    public void lswing()
    {
        if (inSwing)
        {
            angle = angle - ((float)0.8 * acceleration);
            acceleration = acceleration - (float)0.03;

            if (acceleration <= 0)
            {
                inSwing = false;
            }
        }

    }

    public void LoadContent(ContentManager theContentManager, string theAssetName)
    {
        mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName);
        origin.X = 0;
        origin.Y = mSpriteTexture.Height / 2;
    }
    public void Draw(SpriteBatch theSpriteBatch)
    {
        theSpriteBatch.Draw(mSpriteTexture, position, null, Color.White, angle, origin, 1.0f, SpriteEffects.None, 0f);
    }
    public void snapTo(Vector2 heroPosition)
    {
        position.X = heroPosition.X;
        position.Y = heroPosition.Y;

    }

    //public bool CollidesWith(Sprite other)
    //{
    //    // Default behavior uses per-pixel collision detection
    //    return CollidesWith(other, true);
    //}

    //public bool CollidesWith(Sprite other, bool calcPerPixel)
    //{
    //    // Get dimensions of texture
    //    int widthOther = other.Texture.Width;
    //    int heightOther = other.Texture.Height;
    //    int widthMe = Texture.Width;
    //    int heightMe = Texture.Height;

    //    if (calcPerPixel &&                                // if we need per pixel
    //        ((Math.Min(widthOther, heightOther) > 100) ||  // at least avoid doing it
    //        (Math.Min(widthMe, heightMe) > 100)))          // for small sizes (nobody will notice :P)
    //    {
    //        return Bounds.Intersects(other.Bounds) // If simple intersection fails, don't even bother with per-pixel
    //            && PerPixelCollision(this, other);
    //    }

    //    return Bounds.Intersects(other.Bounds);
    //}

    //static bool PerPixelCollision(Sprite a, Sprite b)
    //{
    //    // Get Color data of each Texture
    //    Color[] bitsA = new Color[a.Texture.Width * a.Texture.Height];
    //    a.Texture.GetData(bitsA);
    //    Color[] bitsB = new Color[b.Texture.Width * b.Texture.Height];
    //    b.Texture.GetData(bitsB);

    //    // Calculate the intersecting rectangle
    //    int x1 = Math.Max(a.Bounds.X, b.Bounds.X);
    //    int x2 = Math.Min(a.Bounds.X + a.Bounds.Width, b.Bounds.X + b.Bounds.Width);

    //    int y1 = Math.Max(a.Bounds.Y, b.Bounds.Y);
    //    int y2 = Math.Min(a.Bounds.Y + a.Bounds.Height, b.Bounds.Y + b.Bounds.Height);

    //    // For each single pixel in the intersecting rectangle
    //    for (int y = y1; y < y2; ++y)
    //    {
    //        for (int x = x1; x < x2; ++x)
    //        {
    //            // Get the color from each texture
    //            Color a = bitsA[(x - a.Bounds.X) + (y - a.Bounds.Y) * a.Texture.Width];
    //            Color b = bitsB[(x - b.Bounds.X) + (y - b.Bounds.Y) * b.Texture.Width];

    //            if (a.A != 0 && b.A != 0) // If both colors are not transparent (the alpha channel is not 0), then there is a collision
    //            {
    //                return true;
    //            }
    //        }
    //    }
    //    // If no collision occurred by now, we're clear.
    //    return false;
    //}

    //private Rectangle bounds = Rectangle.Empty;
    //public virtual Rectangle Bounds
    //{
    //    get
    //    {
    //        return new Rectangle(
    //            (int)Position.X - Texture.Width,
    //            (int)Position.Y - Texture.Height,
    //            Texture.Width,
    //            Texture.Height);
    //    }

    //}


}
