using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamaController : MonoBehaviour
{

    private GameObject winText;
    
    void Start() {
        winText = GameObject.FindGameObjectWithTag("WinText");
        winText.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D c) {
        if (c.gameObject.tag == "Player") {
            winText.SetActive(true);
        }
    }
}
