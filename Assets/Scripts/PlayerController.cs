using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Distance;
    public string lookFor;
    public GameObject locator;

    private Camera cam;

    public GameObject ping;
    private SpriteRenderer pingSprite;
    public float pingDuration = 0.75f;
    private float pingTimeRemaining;

    public GameObject VoiceNoteControllerObj;
    private VoiceNoteController VoiceController;
    private Animator anim;
    public float movementSpeed;
    private Rigidbody2D rb;

    private bool isFacingRight = true;

    private float mx;
    private float my;

    private UIController UIController;

    private AudioSource voice;

    public void Start()
    {
        cam = Camera.main;
        pingSprite = ping.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        ping.SetActive(false);

        VoiceController = VoiceNoteControllerObj.GetComponent<VoiceNoteController>();
        UIController = GameObject.Find("UIController").GetComponent<UIController>();
        voice = gameObject.GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            UIController.FadeOutToEnd(1);
        }

        if (VoiceController.CanUseVoice() && pingTimeRemaining <= 0 && (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump")))
        {
            VoiceController.UseVoice();
            voice.Play();
            locate();
        }

        if (pingTimeRemaining <= 0)
        {
            ping.SetActive(false);
        }
        else
        {
            pingSprite.color = new Color(1f, 1f, 1f, easeOutCubic(pingTimeRemaining / pingDuration));
            pingTimeRemaining -= Time.deltaTime;
        }

        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");

        if (mx != 0 || my != 0)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = new Vector3(transform.position.x + mx * movementSpeed, transform.position.y + my * movementSpeed, transform.position.z);
            Vector3 velocity = Vector3.Lerp(startPosition, endPosition, Time.deltaTime);
            transform.position = velocity;
            var moveDirection = endPosition - startPosition;
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * 5f);
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

    float easeOutCubic(float i)
    {
        return 1 - Mathf.Pow(1 - i, 3);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "PowerUp")
        {
            VoiceController.RecoverVoice();
        }
    }
}
