using UnityEngine;

public class LengthEffect : PowerupEffect
{
    private static int applied = 0;

    public override void Apply(Ship ship)
    {
        applied++;
        ship.LeftSide.GetComponent<SpringJoint2D>().distance = 10;
    }

    public override void Remove(Ship ship)
    {
        applied--;
        if (applied == 0)
            ship.LeftSide.GetComponent<SpringJoint2D>().distance = 2;
    }
}
