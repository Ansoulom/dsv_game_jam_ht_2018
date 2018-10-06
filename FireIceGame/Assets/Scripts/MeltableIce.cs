using UnityEngine;

public class MeltableIce : IceSurface
{
    protected override void Update()
    {
        if (fireTick_)
        {
            if (meltTimer_.Update(Time.deltaTime))
            {
                RemoveBlock();
                meltTimer_.ResetToSurplus();
            }
        }

        fireTick_ = false;
        iceTick_ = false;
    }
}
