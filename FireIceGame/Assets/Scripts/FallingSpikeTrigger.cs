using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class FallingSpikeTrigger : MonoBehaviour
{
    [SerializeField] private List<Rigidbody2D> spikes_ = new List<Rigidbody2D>();
    [SerializeField] private float minTime_ = 0.2f;
    [SerializeField] private float maxTime_ = 0.5f;

    private bool activated_;
    private Timer timer_;
    private int currentIndex_;
    private List<Vector3> startPositions_ = new List<Vector3>();

    // Use this for initialization
    private void Start()
    {
        timer_ = new Timer(Random.Range(minTime_, maxTime_));
        foreach (var spike in spikes_)
        {
            startPositions_.Add(spike.transform.position);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!activated_)
        {
            return;
        }

        if (timer_.Update(Time.deltaTime))
        {
            spikes_[currentIndex_].transform.position = startPositions_[currentIndex_];
            spikes_[currentIndex_].position = startPositions_[currentIndex_];
            spikes_[currentIndex_].velocity = Vector2.zero;
            spikes_[currentIndex_].simulated = true;
            currentIndex_ = ++currentIndex_ % spikes_.Count;
            timer_ = new Timer(Random.Range(minTime_, maxTime_));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        activated_ = true;
    }
}
