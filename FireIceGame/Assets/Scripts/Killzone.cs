﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour {
    public Transform respawn;

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag("Player")) {
            other.transform.position = respawn.position;
        }
    }
}
