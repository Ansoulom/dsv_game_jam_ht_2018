using UnityEngine;

public class MeltableSimple : IceSurface
{
    protected override void Update()
    {
        if (fireTick_)
        {
            if (meltTimer_.Update(Time.deltaTime))
            {
                if (meltSource_)
                {
                    meltSource_.Play();
                }
                else
                {
                    Debug.LogWarning("Meltable object \"" + name + "\" doesn't have an audio source assigned!");
                }

                Destroy(gameObject);
            }
        }
        else
        {
            meltTimer_.Reset();
        }
    }
}
