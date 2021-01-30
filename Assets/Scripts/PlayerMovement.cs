using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float movementSpeed;
    public Rigidbody2D rb;
    public CapsuleCollider2D col;

    [HideInInspector] public bool isFacingRight = true;
    [HideInInspector] public bool isFacingUp = true;

    public Animator anim;
    public float jumpForce = 20f;
    public Transform feet;
    public Transform sides;
    public LayerMask groundLayers;

    private float mx;
    private float my;
  
    private void Update() {
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");

        if (new Vector2(mx, my).magnitude > 0.05f) {
            anim.SetTrigger("swimming");
        }
        float k = isFacingRight ? 1f : -1f;
        float angle = Mathf.Atan2(my, k*mx) * Mathf.Rad2Deg;
        if (angle == 180) angle = 0;
        if (k < 0) angle = -angle;
        //Debug.Log(angle);
        //Debug.Log(k);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        anim.rootRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (mx > 0 && !isFacingRight) {
            Flip();
        } else if (mx < 0 && isFacingRight) {
            Flip();
        }
        //anim.SetBool("isGrounded", IsGrounded());
    }
    private void Flip()
	{
	    isFacingRight = !isFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    private void FixedUpdate() {
        Vector2 movement = new Vector2(mx * movementSpeed, my * movementSpeed);
        rb.velocity = movement;
    }

    /*void Jump() {
        Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
        rb.velocity = movement;
    }

    public bool IsGrounded() {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayers);
        if (groundCheck) {
            return true;
        }
        return false;
    } */
}
