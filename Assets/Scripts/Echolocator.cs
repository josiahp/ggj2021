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

    private Camera cam;

    public GameObject ping;
    public SpriteRenderer pingSprite;
    public float pingDuration = 0.75f;
    private float pingTimeRemaining;

    public void Start()
    {
        cam = Camera.main;
        pingSprite = ping.GetComponent<SpriteRenderer>();
        ping.SetActive(false);
    }

    private void Update()
    {
        if (pingTimeRemaining <= 0 && Input.GetMouseButtonDown(0))
        {
            locate();
        }

        if (pingTimeRemaining <= 0) {
            ping.SetActive(false);
        } else {
            pingSprite.color = new Color(1f, 1f, 1f, easeOutCubic(pingTimeRemaining / pingDuration));
            pingTimeRemaining -= Time.deltaTime;
        }
    }
    public void locate()
    {
        ping.SetActive(true);
        pingTimeRemaining = pingDuration;

        GameObject[] NPCs = GameObject.FindGameObjectsWithTag(lookFor);
        foreach (GameObject NPC in NPCs)
        {
            Vector2 vpPos = cam.WorldToViewportPoint(NPC.transform.position);
            Debug.Log(vpPos);
            if ((vpPos.x <= 0 || vpPos.x >= 1 || vpPos.y <= 0 || vpPos.y >= 1) &&
                Vector3.Distance(this.transform.position, NPC.transform.position) < Distance)
            {
                PingBack(NPC);
            }
        }
    }

    public void PingBack(GameObject NPC)
    {
        GameObject ping = Instantiate(locator, NPC.transform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
        ping.transform.parent = NPC.transform;
    }

    float easeOutCubic(float i) {
        return 1 - Mathf.Pow(1 - i, 3);
    }
}
