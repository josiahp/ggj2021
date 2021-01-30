using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echolocator : MonoBehaviour
{   
    public float Distance;
    public float fireRate = 0.2f;
    float timeUntilFire;
    public string lookFor;
    public GameObject locator;
    public Collider2D cameraCollider;
    public LayerMask layer;

    private Transform cam;

    public void Start() {
        cam = Camera.main.transform;
    }
    
    private void Update() {
        //  && timeUntilFire < Time.time
        if (Input.GetMouseButtonDown(0)) {
            locate();
            //timeUntilFire = Time.time + fireRate;
        }
    }
    public void locate() {
        GameObject[] NPCs = GameObject.FindGameObjectsWithTag(lookFor);
        //Debug.Log(lookFor);
        //Debug.Log(NPCs);
        foreach (GameObject NPC in NPCs) {
            //Debug.Log(NPC.name);
            //Debug.Log(Vector3.Distance(this.transform.position, NPC.transform.position));
            if (Vector3.Distance(this.transform.position, NPC.transform.position) < Distance) {
                PingBack(NPC);
            }
        }
    }

    public void PingBack(GameObject NPC) {
        Vector2 Direction = new Vector2 (this.transform.position.x - NPC.transform.position.x, this.transform.position.y - NPC.transform.position.y).normalized;
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(NPC.transform.position.x, NPC.transform.position.y), Direction, Mathf.Infinity, layer);
        //Debug.Log(hit.transform.position.x);
        //Debug.Log(hit.transform.position.y);
        Instantiate(locator, hit.point, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
    }
}
