public class BeamFire : Beam
{
#region Protected Method

    protected override void HitRay(IceBlock hit)
    {
        hit.Head.TickFire();
    }

#endregion
}
