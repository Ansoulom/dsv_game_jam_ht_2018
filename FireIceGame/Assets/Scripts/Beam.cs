using System;
using UnityEngine;

public abstract class Beam : MonoBehaviour
{
#region Variables

    [SerializeField] private float range_;
    [SerializeField] private Transform beamParent_;
    [SerializeField] private Transform particleSystem_;
    [SerializeField] private string horizontalInput_;
    [SerializeField] private string verticalInput_;
    [SerializeField] private float inputThreshold_;
    [SerializeField] private float maxEnergy_;
    [SerializeField] private float energyDrainPerSec_;
    [SerializeField] private float energyRegPerSec_;
    [SerializeField] private float regenerationCooldown_ = 1f;
    [SerializeField] private LayerMask raycastMask_;
    [SerializeField] private AudioSource loopSource_;
    [SerializeField] private AudioSource loopSourceHit_;

    private Vector2 aimDirection_;
    private bool shooting_;
    private bool shotPrevFrame_;
    private float energy_;
    private Timer regCooldownTimer_;
    private bool hitBlock_;

#endregion

#region Properties

    public float MaxEnergy
    {
        get { return maxEnergy_; }
    }

#endregion

#region Events

    public event Action<float> OnEnergyChanged = delegate {}; 

#endregion

#region Protected Methods

    protected virtual void Start()
    {
        energy_ = maxEnergy_;
        regCooldownTimer_ = new Timer(regenerationCooldown_, true);
    }

    protected abstract void HitRay(IceBlock hit);

#endregion
    
    private void Update ()
    {
		ProcessInput();
        hitBlock_ = false;
        if (CheckBeam())
        {
            ShootBeam();
        }
        else
        {
            if (shotPrevFrame_)
            {
                regCooldownTimer_.Reset();
                loopSource_.Stop();
            }
            else
            {
                regCooldownTimer_.Update(Time.deltaTime);
                if (regCooldownTimer_.IsDone)
                {
                    energy_ = Mathf.Min(maxEnergy_, energy_ + energyRegPerSec_ * Time.deltaTime);
                    OnEnergyChanged(energy_);
                }
            }
            shotPrevFrame_ = false;
        }

        if (!hitBlock_)
        {
            loopSourceHit_.Stop();
        }
        else
        {
            if (!loopSourceHit_.isPlaying)
            {
                loopSourceHit_.Play();
            }
        }
	}

    private void ProcessInput()
    {
        var inputDir = new Vector2(Input.GetAxis(horizontalInput_), Input.GetAxis(verticalInput_));

        aimDirection_ = inputDir.normalized;
        
        shooting_ = inputDir.magnitude > inputThreshold_;
    }

    private bool CheckBeam()
    {
        var shouldShoot = shooting_ && energy_ > 0f;
        foreach (var particles in particleSystem_.GetComponentsInChildren<ParticleSystem>())
        {
            var emission = particles.emission;
            emission.enabled = shouldShoot;
        }

        return shouldShoot;
    }

    private void ShootBeam()
    {
        energy_ = Mathf.Max(0, energy_ - energyDrainPerSec_ * Time.deltaTime);
        OnEnergyChanged(energy_);
        beamParent_.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(aimDirection_.y, aimDirection_.x) * Mathf.Rad2Deg);
        var hit = Physics2D.Raycast(beamParent_.position, aimDirection_, range_, raycastMask_);
        if (hit.collider)
        {
            var block = hit.collider.GetComponent<IceBlock>();
            if (block)
            {
                HitRay(block);
                hitBlock_ = true;
            }
        }

        shotPrevFrame_ = true;
        if (!loopSource_.isPlaying)
        {
            loopSource_.Play();
        }
    }
}
