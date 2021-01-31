using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float lengthX, lengthY, startposX, startposY;
    private GameObject cam;
    public float parallaxEffectX;
    public float parallaxEffectY;

    // Start is called before the first frame update
    void Start() {
        cam = Camera.main.gameObject;
        startposX = transform.position.x;
        startposY = transform.position.y;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void FixedUpdate() {
        float distanceX = (cam.transform.position.x * parallaxEffectX);
        float distanceY = (cam.transform.position.y * parallaxEffectY);
        transform.position = new Vector3(startposX + distanceX, startposY + distanceY, transform.position.z);

    }
}
