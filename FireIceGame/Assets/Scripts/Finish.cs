using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour {
    [HideInInspector] public GameObject[] players;

    private bool player1 = false, player2 = false;

    private void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    private void Update () {
        if(player1 && player2) {
            SceneManager.LoadScene("Victory Screen");
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag("Player")) {
            if(GameObject.ReferenceEquals(other.gameObject, players[0])) {
                player1 = true;
            }
            else {
                player2 = true;
            }
        }
    }

    private void OnTriggerExit2D (Collider2D other) {
        if (other.CompareTag("Player")) {
            if (GameObject.ReferenceEquals(other.gameObject, players[0])) {
                player1 = false;
            }
            else {
                player2 = false;
            }
        }
    }
}
