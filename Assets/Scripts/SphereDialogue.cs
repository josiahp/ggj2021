using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDialogue : MonoBehaviour
{    
    public GameObject arrowObj;
    private GameObject arrow;
    private GameObject npc;
    private bool visited = false;

    private float angle;

    public NPCManager npcManager;
    public NPCManager mamaManager;

    void Start() {
        Vector3 arrowPos = new Vector3 (transform.position.x, transform.position.y + 2.0f, transform.position.z);
        arrow = Instantiate(arrowObj, arrowPos, Quaternion.identity);
        arrow.transform.parent = transform;
        arrow.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            if (!visited) {
                // 20% of the time we point to an NPC. The rest of the time we point to Mama.
                if (Random.Range(0, 1) <= 0.2f) {
                    npc = npcManager.GetRandomNPC();
                } else {
                    npc = mamaManager.GetRandomNPC();
                }
                Vector3 vect = new Vector3(npc.transform.position.x - arrow.transform.position.x, npc.transform.position.y - arrow.transform.position.y, npc.transform.position.z - arrow.transform.position.z);
                if (Mathf.Abs(vect.y) > 0.05f) {
                    arrow.transform.up = vect;
                }
                visited = true;
            }
            arrow.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            arrow.SetActive(false);
        }
    }
}
