using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField]
    Transform groundCheck = default;

    [SerializeField]
    LayerMask whatIsGround = default;

    [Header("Movement")]
    [SerializeField] float runSpeed = 6f;

    [Header("Jumping")]
    [SerializeField] float jumpForce = 300f;
    [SerializeField] readonly int jumps = 2;

    [Header("Misc")]
    [SerializeField] GameObject weapon;

    Animator animator;

    bool grounded;
    bool wasGrounded;
    bool facingRight = true;

    int currentJumps;

    const float GROUNDED_SIZE_X = .84f;
    const float GROUNDED_SIZE_Y = .15f;
    Vector2 groundedSize;

    public UnityEvent OnLandEvent;
    public UnityEvent OnJumpEvent;

    public float JumpForce
    {
        get { return jumpForce; }
    }

    public float RunSpeed
    {
        get { return runSpeed; }
    }

    private void Awake()
    {
        currentJumps = jumps;
        groundedSize = new Vector2(GROUNDED_SIZE_X, GROUNDED_SIZE_Y);

        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (groundCheck != null)
        {
            grounded = false;
            Collider2D[] colliders = Physics2D.OverlapBoxAll(groundCheck.position, groundedSize, 0, whatIsGround);
            if (colliders.Length > 0)
            {
                wasGrounded = grounded;
                grounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }
        else Debug.LogError("Fatal Error: Groundcheck object was null, cannot continue...");

        float horizontal = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Horizontal", Mathf.Abs(horizontal));
        animator.SetBool("Grounded", grounded);
        if (Input.GetButtonDown("Jump") && (grounded || currentJumps > 0))
            animator.SetTrigger("Jump");
        animator.SetInteger("Jumps", currentJumps);
        if (Input.GetButtonDown("Attack"))
            weapon.GetComponent<Animator>().SetTrigger("Swing");

        if ((horizontal > 0 && !facingRight) ||
            (horizontal < 0 && facingRight))
            Flip();
    }

    public void OnLand()
    {
        currentJumps = jumps;
    }

    public void OnJump()
    {
        currentJumps--;
    }

    public void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
