using UnityEngine;

public class BeamIce : Beam
{
#region Protected Methods
    
    protected override void HitRay(RaycastHit2D hit)
    {
        var iceable = hit.transform.GetComponent<IceBlock>();
        if (iceable)
        {
            iceable.Head.TickIce();
        }
    }

#endregion
}
