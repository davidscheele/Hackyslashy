using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Sprite
{
    public Vector2 position = new Vector2(0, 0);
    private Texture2D mSpriteTexture;

	public Sprite()
	{
        

	}

    public void LoadContent(ContentManager theContentManager, string theAssetName)
    {
        mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName);
    }
    public void Draw(SpriteBatch theSpriteBatch)
    {
        theSpriteBatch.Draw(mSpriteTexture, Position, Color.White);
    }

}
