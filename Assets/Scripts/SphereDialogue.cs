using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDialogue : MonoBehaviour
{    
    public GameObject arrowObj;
    private GameObject arrow;
    public GameObject npc;
    private SpawnController controller;
    //private GameObject Text;
    private bool visited = false;

    private float angle;

    void Start() {
        GameObject g = GameObject.FindGameObjectWithTag("SpawnController");
        controller = g.GetComponent<SpawnController>();
        Vector3 arrowPos = new Vector3 (transform.position.x, transform.position.y + 2.0f, transform.position.z);
        arrow = Instantiate(arrowObj, arrowPos, Quaternion.identity);
        arrow.transform.parent = this.transform;
        arrow.SetActive(false);
    }

    /*void Update()
    {
        //Debug.Log("working?");
        if (playerInRange && !poppedUp) {
            winText.SetActive(true);
            poppedUp = true;
        }
    } */

    void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("Collision Detected "+other.gameObject.name);
        if (other.tag == "Player") {
            if (!visited) {
                npc = controller.randomNPC();
                arrow.transform.up = new Vector3(npc.transform.position.x - arrow.transform.position.x, npc.transform.position.y - arrow.transform.position.y, npc.transform.position.z - arrow.transform.position.z);
                visited = true;
            }
            //playerInRange = true;
            arrow.SetActive(true);
            Debug.Log(angle);
            //Debug.Log("player entered"); 
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        //Debug.Log("Collision Detected "+ other.gameObject.name);
        if (other.tag == "Player") {
            arrow.SetActive(false);
            /*playerInRange = false;
            if (poppedUp) {
                winText.SetActive(true);
            }
            poppedUp = false; */
            //Debug.Log("player exited"); 
        }
    }
}
