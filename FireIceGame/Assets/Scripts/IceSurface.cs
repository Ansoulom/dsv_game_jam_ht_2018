using UnityEngine;

public class IceSurface : IceBlock
{
#region Variables

    [SerializeField] private Vector2 normal_ = new Vector2(-1f, 0f);
    [SerializeField] private GameObject iceBlockPrefab_;
    [SerializeField] private float timeUntilMelt_ = 5f;
    [SerializeField] private float meltTime_ = 1f;
    [SerializeField] private float iceTime_;

    private Timer waitTimer_;
    protected Timer meltTimer_;
    private Timer iceTimer_;
    protected bool fireTick_;
    protected bool iceTick_;

#endregion

#region Properties
    
    private IceBlock Tail
    {
        get
        {
            IceBlock tail = this;
            while (tail.Next != null)
            {
                tail = tail.Next;
            }

            return tail;
        }
    }

#endregion

#region Public Methods

    public void TickIce()
    {
        iceTick_ = true;
    }

    public void TickFire()
    {
        fireTick_ = true;
    }

#endregion

#region Private Methods

    private void Start()
    {
        iceTimer_ = new Timer(iceTime_);
        waitTimer_ = new Timer(timeUntilMelt_);
        meltTimer_ = new Timer(meltTime_);
        Head = this;
    }

    protected virtual void Update()
    {
        if (iceTick_)
        {
            waitTimer_.Reset();
            if (iceTimer_.Update(Time.deltaTime))
            {
                AddBlock(iceBlockPrefab_);
                iceTimer_.ResetToSurplus();
            }

            if (!fireTick_)
            {
                meltTimer_.Reset();
            }
        }
        else
        {
            iceTimer_.Reset();
        }
        if (!fireTick_ && !iceTick_)
        {
            waitTimer_.Update(Time.deltaTime);
        }
        if (fireTick_)
        {
            waitTimer_.Finish();
        }
        if (waitTimer_.IsDone && meltTimer_.Update(Time.deltaTime))
        {
            RemoveBlock();
            meltTimer_.Reset();
        }

        fireTick_ = false;
        iceTick_ = false;
    }

    private void AddBlock(GameObject blockPrefab)
    {
        var tail = Tail;
        var pos = tail.transform.position + new Vector3(normal_.x, normal_.y, 0f);
        var obj = Instantiate(blockPrefab, pos, Quaternion.identity);
        var surface = obj.GetComponent<IceBlock>();
        surface.Head = this;
        surface.Previous = tail;
        tail.Next = surface;
    }

    protected void RemoveBlock()
    {
        var tail = Tail;
        if (tail.Previous == null)
        {
            return;
        }

        tail.Previous.Next = null;
        Destroy(tail.gameObject);
    }

#endregion
}
