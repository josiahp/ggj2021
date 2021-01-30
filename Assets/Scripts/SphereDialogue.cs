using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDialogue : MonoBehaviour
{
    public GameObject LevelManager;
    public string text;
    private bool playerInRange = false;
    private bool poppedUp = false;
    void Update()
    {
        //Debug.Log("working?");
        if (playerInRange && !poppedUp) {
            PopUpScript pop = LevelManager.GetComponent<PopUpScript>();
            pop.PopUp(text);
            poppedUp = true;
        } 
    }

    void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("Collision Detected "+other.gameObject.name);
        if (other.tag == "Player") {
            playerInRange = true;
            //Debug.Log("player entered"); 
        }
    }

    public void popUp() {
        poppedUp = false;
    }

    void OnTriggerExit2D(Collider2D other) {
        //Debug.Log("Collision Detected "+ other.gameObject.name);
        if (other.tag == "Player") {
            playerInRange = false;
            if (poppedUp) {
                PopUpScript pop = LevelManager.GetComponent<PopUpScript>();
                pop.animator.SetTrigger("close");
            }
            poppedUp = false;
            //Debug.Log("player exited"); 
        }
    }
}
