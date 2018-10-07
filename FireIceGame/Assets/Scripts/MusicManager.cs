using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource_;

    public static MusicManager Instance
    {
        get { return FindObjectOfType<MusicManager>(); }
    }

    public void SwitchMusic(AudioClip musicClip)
    {
        musicSource_.Stop();
        musicSource_.clip = musicClip;
        musicSource_.Play();
    }
}
