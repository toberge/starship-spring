public class ShieldEffect : PowerupEffect
{
    private static int applied = 0;

    public override void Apply(Ship ship)
    {
        if (applied == 0)
            ship.RaiseShields();
        applied++;
    }

    public override void Remove(Ship ship)
    {
        applied--;
        if (applied == 0)
            ship.LowerShields();
    }
}
