using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour {
    public Transform respawn;
    public Lives lives;
    public float respawnTime = 1f;

    private bool respawning;

    private void Start () {
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
            StartCoroutine(Respawn(other));
        }
    }
    private IEnumerator Respawn (Collider2D player) {
        yield return new WaitForSeconds(respawnTime);
        player.transform.position = respawn.position;
        respawning = false;
        player.gameObject.SetActive(true);
    }
}
