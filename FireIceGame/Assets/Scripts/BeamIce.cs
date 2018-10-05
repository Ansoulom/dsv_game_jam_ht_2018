using UnityEngine;

public class BeamIce : Beam
{
#region Variables

    [SerializeField] private float iceCreationTime_;
    [SerializeField] private GameObject iceBlockPrefab_;
    
    private Timer creationTimer_;
    private Collider2D previousCollider_;

#endregion


#region Protected Methods

    protected override void Start()
    {
        base.Start();
        creationTimer_ = new Timer(iceCreationTime_);
    }

    protected override void HitRay(RaycastHit2D hit)
    {
        if (hit.collider != previousCollider_)
        {
            StopHit();
            previousCollider_ = hit.collider;
        }
        if (creationTimer_.Update(Time.deltaTime))
        {
            var pos = hit.collider.transform.position + new Vector3(hit.normal.x, hit.normal.y, 0f);
            Instantiate(iceBlockPrefab_, pos, Quaternion.identity);
            creationTimer_.ResetToSurplus();
        }
    }

    protected override void StopHit()
    {
        creationTimer_.Reset();
        previousCollider_ = null;
    }

    #endregion
}
