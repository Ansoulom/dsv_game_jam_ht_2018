using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour {
    public int startingLives = 3;
    public GameObject[] hearts;
    private int currentLives;

    private void Start () {
        currentLives = startingLives;
    }

    private void Update () {
        if(currentLives <= 0) {
            GameOver();
        }
    }

    public void HurtPlayer () {
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
        SceneManager.LoadScene("Game Over");
    }
}
