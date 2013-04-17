using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Slime : Enemy
{
    


	public Slime()
	{
        this.TopLeft = this.PositionRandomization;
        this.StartPosition = this.TopLeft;
        this.RunSpeed = 4;
        this.AssetName = "slime";
	}

    



}
