using UnityEngine;

public class BeamFire : Beam
{
#region Protected Method

    protected override void HitRay(RaycastHit2D hit)
    {
        var iceable = hit.transform.GetComponent<IceBlock>();
        if (iceable)
        {
            iceable.Head.TickFire();
        }
    }

#endregion
}
