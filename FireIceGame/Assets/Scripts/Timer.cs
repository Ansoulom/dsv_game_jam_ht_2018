using System;
using UnityEngine;

public class Timer
{
#region Variables

    private readonly float targetTime_;

#endregion

#region Properties

    public float PercentDone
    {
        get
        {
            if (targetTime_ <= 0f)
            {
                return IsDone ? 1f : 0f;
            }
            return Mathf.Clamp(TimePassed / targetTime_, 0f, 1f);
        }
    }

    public bool IsDone { get; private set; }

    public float TimePassed { get; private set; }

    public float TimeLeft
    {
        get { return Mathf.Max(targetTime_ - TimePassed, 0f); }
    }

    public float MaxTime
    {
        get { return targetTime_; }
    }

#endregion

#region Public Methods

    public Timer(float maxTime, bool doneFromBeginning = false)
    {
        if (maxTime < 0) throw new ArgumentOutOfRangeException();
        targetTime_ = maxTime;
        TimePassed = doneFromBeginning ? targetTime_ : 0f;
        IsDone = doneFromBeginning;
    }
    
    // Returns true if the timer completes when this method runs
    public bool Update(float timePassed)
    {
        if (IsDone) return false;

        TimePassed += timePassed;
        if (TimePassed >= targetTime_) IsDone = true;

        return IsDone;
    }
    
    public void ResetToSurplus()
    {
        if (TimePassed >= targetTime_)
        {
            TimePassed -= targetTime_;
        }
        IsDone = false;
    }
    
    public void Reset()
    {
        TimePassed = 0f;
        IsDone = false;
    }
    
    public void Finish()
    {
        TimePassed = targetTime_;
        IsDone = true;
    }

#endregion
}
