using UnityEngine;

public class IceBlock : MonoBehaviour
{
#region Variables
    
    [SerializeField] private IceBlock next_;
    [SerializeField] private IceBlock previous_;
    [SerializeField] private IceSurface head_;

#endregion

#region Properties
    
    public IceSurface Head
    {
        get { return head_; }
        set { head_ = value; }
    }

    public IceBlock Next
    {
        get { return next_; }
        set { next_ = value; }
    }

    public IceBlock Previous
    {
        get { return previous_; }
        set { previous_ = value; }
    }

#endregion
}
