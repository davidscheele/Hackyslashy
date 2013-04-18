using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Slime : Enemy
{
    


	public Slime(Vector2 _position)
	{
        //this.TopLeft = this.PositionRandomization;
        this.TopLeft = _position;
        this.StartPosition = this.TopLeft;
        this.RunSpeed = 2;
        this.AssetName = "slime";
	}

    



}
