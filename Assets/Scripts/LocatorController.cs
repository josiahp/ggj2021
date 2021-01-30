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
        updatePosition();
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

        // Calculate the angle of rotation for the sprite
        Transform parent = gameObject.transform.parent;
        Vector2 direction = parent.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Vector2 screenPos = Camera.main.WorldToViewportPoint(parent.position);

        // If the parent is onscreen there is no reason to bother with finding the right screen position
        if(screenPos.x >= 0f && screenPos.x <= 1f && screenPos.y >= 0f && screenPos.y <= 1f) {
            return;
        }

        // normalize the screen coordinates to between -1 and 1
        Vector2 onScreenPos = new Vector2(screenPos.x - 0.5f, screenPos.y - 0.5f) * 2;
        float max = Mathf.Max(Mathf.Abs(onScreenPos.x), Mathf.Abs(onScreenPos.y));

        // 0 to 1 along this vector represents a line from the player to the edge of the viewport.
        // multiply by this to change how far the sprite appears from the viewport edge. lower is further.
        onScreenPos *= 0.9f;
        onScreenPos = (onScreenPos / (max * 2)) + new Vector2(0.5f, 0.5f);

        Vector3 worldPos = Camera.main.ViewportToWorldPoint(onScreenPos);

        // set the z or else it becomes -10 and invisible
        worldPos.z = 0;

        gameObject.transform.position = worldPos;

        // flip the rotation around because the sprite is supposed to be originating from the parent, so the angle is flipped
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle - 180);
    }
}
