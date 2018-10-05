using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField] private readonly float range_;
    [SerializeField] private Transform beamParent_;
    [SerializeField] private ParticleSystem particleSystem_;
    [SerializeField] private string horizontalInput_;
    [SerializeField] private string verticalInput_;
    [SerializeField] private float inputThreshold_;
    [SerializeField] private float maxEnergy_;
    [SerializeField] private float energyDrainPerSec_;
    [SerializeField] private float energyRegPerSec_;

    private Vector2 aimDirection_;
    private bool shooting_;
    private float energy_;


	// Use this for initialization
    private void Start ()
    {
        energy_ = maxEnergy_;
    }
	
	// Update is called once per frame
    private void Update ()
    {
		ProcessInput();
        ShootBeam();
	}

    private void ProcessInput()
    {
        var inputDir = new Vector2(Input.GetAxis(horizontalInput_), Input.GetAxis(verticalInput_));

        aimDirection_ = inputDir.normalized;
        
        shooting_ = inputDir.magnitude > inputThreshold_;
    }

    private void ShootBeam()
    {
        var shouldShoot = shooting_ && energy_ > 0f;
        var emission = particleSystem_.emission;
        emission.enabled = shouldShoot;
        if (!shouldShoot)
        {
            return;
        }

        beamParent_.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(-aimDirection_.y, aimDirection_.x) * Mathf.Rad2Deg);
    }
}
