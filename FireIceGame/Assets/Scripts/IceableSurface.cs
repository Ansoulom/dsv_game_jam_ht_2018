using UnityEngine;

public class IceableSurface : MonoBehaviour
{
#region Variables

    [SerializeField] private Vector2 normal_ = new Vector2(-1f, 0f);

    private IceableSurface next_;
    private IceableSurface previous_;
    private IceableSurface head_;

#endregion

#region Properties

    private IceableSurface Tail
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

#region Public Methods

    public void AddBlock(GameObject blockPrefab)
    {
        var tail = Tail;
        var pos = tail.transform.position + new Vector3(normal_.x, normal_.y, 0f);
        var obj = Instantiate(blockPrefab, pos, Quaternion.identity);
        var surface = obj.GetComponent<IceableSurface>();
        surface.normal_ = head_.normal_;
        surface.previous_ = tail;
        tail.next_ = surface;
    }

    public void RemoveBlock()
    {
        var tail = Tail;
        if (tail.previous_ == null)
        {
            return;
        }

        tail.previous_.next_ = null;
        Destroy(tail.gameObject);
    }

#endregion

#region Private Methods

    private void Start()
    {
        if (!head_)
        {
            head_ = this;
        }
    }

#endregion
}
