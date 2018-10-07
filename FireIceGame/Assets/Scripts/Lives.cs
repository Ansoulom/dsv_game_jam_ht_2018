using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour {
    public int startingLives = 3;
    public GameObject[] hearts;
    [SerializeField] private AudioSource damageSource_;
    private int currentLives;
    private bool lost_;

    private void Start () {
        currentLives = startingLives;
    }

    private void Update () {
        if(currentLives <= 0) {
            GameOver();
        }
    }

    public void HurtPlayer () {
        damageSource_.Play();
        currentLives--;
        switch (currentLives) {
            case 2:
                hearts[0].SetActive(false);
                break;
            case 1:
                hearts[1].SetActive(false);
                break;
            case 0:
                hearts[2].SetActive(false);
                break;
        }
    }

    public void GameOver () {
        if (lost_)
        {
            return;
        }

        lost_ = true;
        StartCoroutine(WaitGameOver());
    }

    private IEnumerator WaitGameOver()
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Game Over");
    }
}
