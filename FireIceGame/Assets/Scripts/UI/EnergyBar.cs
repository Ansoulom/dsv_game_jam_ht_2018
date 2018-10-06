using UnityEngine;

public class EnergyBar : MonoBehaviour
{
#region Variables

    [SerializeField] private Beam beam_;
    [SerializeField] private RectTransform fillTransform_;

#endregion

    // Use this for initialization
    private void Start()
    {
        beam_.OnEnergyChanged += OnEnergyChanged;
    }

    private void OnEnergyChanged(float energy)
    {
        fillTransform_.localScale = new Vector3(energy / beam_.MaxEnergy, 1f, 1f);
    }
}
