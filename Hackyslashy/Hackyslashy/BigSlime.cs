using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class BigSlime : Enemy
{
    


	public BigSlime(int _speed)
	{
        this.RunSpeed = _speed;
        this.AssetName = "slimeBig";
        this.TopLeft = new Vector2(700, 100);
	}

    



}
