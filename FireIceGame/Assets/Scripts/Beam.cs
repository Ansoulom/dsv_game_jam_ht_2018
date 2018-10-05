using UnityEngine;

public abstract class Beam : MonoBehaviour
{
#region Variables

    [SerializeField] private float range_;
    [SerializeField] private Transform beamParent_;
    [SerializeField] private ParticleSystem particleSystem_;
    [SerializeField] private string horizontalInput_;
    [SerializeField] private string verticalInput_;
    [SerializeField] private float inputThreshold_;
    [SerializeField] private float maxEnergy_;
    [SerializeField] private float energyDrainPerSec_;
    [SerializeField] private float energyRegPerSec_;
    [SerializeField] private LayerMask raycastMask_;

    private Vector2 aimDirection_;
    private bool shooting_;
    private float energy_;

#endregion

#region Protected Methods

    protected virtual void Start()
    {
        energy_ = maxEnergy_;
    }

    protected abstract void HitRay(RaycastHit2D hit);

    protected abstract void StopHit();

#endregion
    
    private void Update ()
    {
		ProcessInput();
        if (CheckBeam())
        {
            ShootBeam();
        }
        else
        {
            StopHit();
        }
	}

    private void ProcessInput()
    {
        var inputDir = new Vector2(Input.GetAxis(horizontalInput_), -Input.GetAxis(verticalInput_));

        aimDirection_ = inputDir.normalized;
        
        shooting_ = inputDir.magnitude > inputThreshold_;
    }

    private bool CheckBeam()
    {
        var shouldShoot = shooting_ && energy_ > 0f;
        var emission = particleSystem_.emission;
        emission.enabled = shouldShoot;

        return shouldShoot;
    }

    private void ShootBeam()
    {
        beamParent_.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(aimDirection_.y, aimDirection_.x) * Mathf.Rad2Deg);
        var hit = Physics2D.Raycast(beamParent_.position, aimDirection_, range_, raycastMask_);
        if (hit.collider)
        {
            HitRay(hit);
        }
        else
        {
            StopHit();
        }
    }
}
