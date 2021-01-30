using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunaDialogue : MonoBehaviour
{
    public GameObject LevelManager;
    public SphereCollider col;
    public string text;
    private bool playerInRange = false;
    void Update()
    {
        if (playerInRange) {
            PopUpScript pop = LevelManager.GetComponent<PopUpScript>();
            pop.PopUp(text); 
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") playerInRange = true;
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player") playerInRange = false;
    }
}
