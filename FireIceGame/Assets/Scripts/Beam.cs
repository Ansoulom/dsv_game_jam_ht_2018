﻿using System;
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
    [SerializeField] private float regenerationCooldown_ = 1f;
    [SerializeField] private LayerMask raycastMask_;

    private Vector2 aimDirection_;
    private bool shooting_;
    private bool shotPrevFrame_;
    private float energy_;
    private Timer regCooldownTimer_;

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

    protected abstract void HitRay(RaycastHit2D hit);

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
            if (shotPrevFrame_)
            {
                regCooldownTimer_.Reset();
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
        var emission = particleSystem_.emission;
        emission.enabled = shouldShoot;

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
            HitRay(hit);
        }

        shotPrevFrame_ = true;
    }
}
