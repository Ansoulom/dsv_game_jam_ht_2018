using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private string sceneName_ = "Main Menu";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(sceneName_);
    }
}
