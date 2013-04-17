using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Slime : Enemy
{
    


	public Slime(int _speed)
	{
        this.RunSpeed = _speed;
        this.AssetName = "slime";
	}

    



}
