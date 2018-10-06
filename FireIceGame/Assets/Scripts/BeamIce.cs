public class BeamIce : Beam
{
#region Protected Methods
    
    protected override void HitRay(IceBlock hit)
    {
        hit.Head.TickIce();
    }

#endregion
}
