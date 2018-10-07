using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip_;

    public void Switch()
    {
        var manager = MusicManager.Instance;
        if (manager)
        {
            manager.SwitchMusic(musicClip_);
        }
    }
}
