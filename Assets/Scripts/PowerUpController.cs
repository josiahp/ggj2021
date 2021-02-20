using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D c) {
        if (c.gameObject.tag == "Player") {
            StartCoroutine(ReactAndDestroy());
        }
    }

    IEnumerator ReactAndDestroy() {
        GetComponent<ParticleSystem>().Play();
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
