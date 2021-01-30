using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocatorController : MonoBehaviour
{
    // Start is called before the first frame update

    public float secondsToLive = 2;
    private float remainingSeconds;

    private GameObject player;

    private SpriteRenderer sprite;
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");

        remainingSeconds = secondsToLive;
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingSeconds <= 0) {
            DestroyImmediate(this.gameObject);
            return;
        }

        updatePosition();
        remainingSeconds -= Time.deltaTime;
        sprite.color = new Color(1f, 1f, 1f, easeOutCubic(remainingSeconds / secondsToLive));
    }

    float easeOutCubic(float i) {
        return 1 - Mathf.Pow(1 - i, 3);
    }

    void updatePosition() {

        Transform parent = gameObject.transform.parent;
        Vector2 direction = parent.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Vector2 screenPos = Camera.main.WorldToViewportPoint(parent.position);

        if(screenPos.x >= 0f && screenPos.x <= 1f && screenPos.y >= 0f && screenPos.y <= 1f){
            return;
        }

        Vector2 onScreenPos = new Vector2(screenPos.x - 0.5f, screenPos.y - 0.5f) * 2;
        float max = Mathf.Max(Mathf.Abs(onScreenPos.x), Mathf.Abs(onScreenPos.y));

        onScreenPos *= 0.8f;
        onScreenPos = (onScreenPos / (max * 2)) + new Vector2(0.5f, 0.5f);

        Vector3 worldPos = Camera.main.ViewportToWorldPoint(onScreenPos);
        worldPos.z = 0;

        gameObject.transform.position = worldPos;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
