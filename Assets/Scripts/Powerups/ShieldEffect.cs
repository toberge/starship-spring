public class ShieldEffect : PowerupEffect
{
    private static int applied = 0;

    public override void Apply(Ship ship)
    {
        applied++;
        if (applied == 1)
            ship.ShieldUp();
    }

    public override void Remove(Ship ship)
    {
        applied--;
        if (applied == 0)
            ship.ShieldDown();
    }
}
