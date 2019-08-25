using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //changabel Variables
    [Header("Player Movement")];
    public float MoveForce = 350f;
    public float maxSpeed = 5f;
    public float JumpForce = 1000f;

    public Transform CheckGround;

    [HideInInspector] public bool jump = true;
    [HideInInspector] public bool FacingRight = true;

    private bool Grounded = false;
    private Animator anim;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        //Get Components
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if character is  groundet  and set the jumplock to true
        Grounded = Physics2D.Linecast(transform.position, CheckGround.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && Grounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        //Moveing Left / Right
        float h = Input.GetAxisRaw("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(h));

        if (h * rb2d.velocity.x < maxSpeed)
        {
            rb2d.AddForce(Vector2.right * h * MoveForce);
        }
        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
        {
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
        }

        //Flip Character in the derection he is moveing
        if (h > 0 && !FacingRight)
        {
            Flip();
        }
        else if (h < 0 && FacingRight)
        {
            Flip();
        }

        // Handel Jump
        if (jump)
        {
            anim.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0f, JumpForce));
            jump = false;
        }
    }

    void Flip()
    {
        // Handel Character Flip
        FacingRight = !FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
