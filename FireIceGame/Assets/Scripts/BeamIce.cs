using UnityEngine;

public class BeamIce : Beam
{
#region Variables

    [SerializeField] private float iceCreationTime_;
    [SerializeField] private GameObject iceBlockPrefab_;
    
    private Timer creationTimer_;
    private IceableSurface previousBlock_;

#endregion


#region Protected Methods

    protected override void Start()
    {
        base.Start();
        creationTimer_ = new Timer(iceCreationTime_);
    }

    protected override void HitRay(RaycastHit2D hit)
    {
        var iceable = hit.transform.GetComponent<IceableSurface>();
        if (iceable != previousBlock_)
        {
            StopHit();
            previousBlock_ = iceable;
        }
        else if (!iceable)
        {
            return;
        }
        if (creationTimer_.Update(Time.deltaTime))
        {
            iceable.AddBlock(iceBlockPrefab_);
            creationTimer_.ResetToSurplus();
        }
    }

    protected override void StopHit()
    {
        creationTimer_.Reset();
        previousBlock_ = null;
    }

    #endregion
}
