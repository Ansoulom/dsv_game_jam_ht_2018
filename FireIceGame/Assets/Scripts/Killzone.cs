using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour {
    [HideInInspector] public GameObject[] players;
    [HideInInspector] public Lives lives;
    public float respawnTime = 1f;

    private bool respawning;

    private void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
        lives = FindObjectOfType<Lives>();
    }
    private void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag("Player")) {
            other.gameObject.SetActive(false);
            if(respawning == true) {
                lives.GameOver();
            }
            lives.HurtPlayer();
            respawning = true;
            StartCoroutine(Respawn(other.gameObject));
        }
    }
    private IEnumerator Respawn (GameObject player) {
        yield return new WaitForSeconds(respawnTime);
        GameObject otherPlayer;
        if (GameObject.ReferenceEquals(player, players[0])) {
            otherPlayer = players[1];
        }
        else {
            otherPlayer = players[0];
        }
        player.transform.position = otherPlayer.transform.position;
        respawning = false;
        player.SetActive(true);
    }
}
