using UnityEngine;

public class MeltableSimple : IceSurface
{
    protected override void Update()
    {
        if (fireTick_)
        {
            if (meltTimer_.Update(Time.deltaTime))
            {
                Destroy(gameObject);
            }
        }
        else
        {
            meltTimer_.Reset();
        }
    }
}
