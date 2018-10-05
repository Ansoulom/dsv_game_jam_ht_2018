using UnityEngine;

public class IceSurface : MonoBehaviour
{
#region Variables

    [SerializeField] private Vector2 normal_ = new Vector2(-1f, 0f);
    [SerializeField] private float timeUntilFade = 5f;
    [SerializeField] private float fadeTime = 1f;
    
    private Timer waitTimer;
    private Timer fadeTimer;

    private IceBlock next_;

#endregion

#region Properties

    public Vector2 Normal
    {
        get { return normal_; }
    }

    private IceBlock Tail
    {
        get
        {
            var tail = next_;
            if (!tail)
            {
                return null;
            }
            while (tail.next_ != null)
            {
                tail = tail.next_;
            }

            return tail;
        }
    }

#endregion

#region Private Methods

    private void Start()
    {
        waitTimer = new Timer(timeUntilFade);
        fadeTimer = new Timer(fadeTime);
    }

    private void Update()
    {

    }

    public void AddBlock(GameObject blockPrefab)
    {
        var tail = Tail;
        var pos = tail.transform.position + new Vector3(normal_.x, normal_.y, 0f);
        var obj = Instantiate(blockPrefab, pos, Quaternion.identity);
        var surface = obj.GetComponent<IceBlock>();
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
}
