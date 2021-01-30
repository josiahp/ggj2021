using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamaController : MonoBehaviour
{

    public GameObject winText;
    
    void OnTriggerEnter2D(Collider2D c) {
        if (c.gameObject.tag == "Player") {
            winText.SetActive(true);
        }
    }
}
