using UnityEngine;

public class IceBlock : MonoBehaviour
{
#region Variables
    
    public IceBlock next_;
    public IceBlock previous_;
    private IceSurface head_;

#endregion

#region Properties

    private IceBlock Tail
    {
        get
        {
            var tail = this;
            while (tail.next_ != null)
            {
                tail = tail.next_;
            }

            return tail;
        }
    }

#endregion
}
