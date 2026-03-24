using UnityEngine;

public class PlayerCont : MonoBehaviour
{
    public float speed;
    public float jumpAmount;
    Rigidbody rb;
    Animator animator;
    bool isGrounded;
    public LayerMask ground;
    public Transform checkpoint;
    public bool stopAction;
    CapsuleCollider capsuleCollider;
    public bool win;
    public GameObject winText;
    public Animator rightB, leftB,downB,upB, jumpB;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        animator.SetBool("walk", false);
        animator.SetBool("isGrounded", true);
    }

    
    void Update()
    {
        rightB.SetBool("press", Input.GetAxisRaw("Horizontal") > 0);
        leftB.SetBool("press", Input.GetAxisRaw("Horizontal") < 0);
        downB.SetBool("press", Input.GetAxisRaw("Vertical") < 0);
        upB.SetBool("press", Input.GetAxisRaw("Vertical") > 0);
        jumpB.SetBool("press", Input.GetKey(KeyCode.Space));





        winText.SetActive(win);
        if (win)
        {
            rb.linearVelocity = Vector3.zero;
            capsuleCollider.enabled = false;
        }
        else if (stopAction)
        {
            rb.linearVelocity = Vector3.zero;
            transform.position = new Vector3(checkpoint.position.x,0.5f, checkpoint.position.z);
            capsuleCollider.enabled = false;
        }
        else
        {
            capsuleCollider.enabled = true;
            float horizontal = Input.GetAxisRaw("Horizontal");
            rb.linearVelocity = new Vector3(horizontal * speed, 0, rb.linearVelocity.z);
            if (horizontal > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            if (horizontal < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            animator.SetBool("walk", horizontal != 0);
            animator.SetBool("isGrounded", isGrounded);

            Collider[] colls = Physics.OverlapSphere(transform.position, 0.1f, ground);
            isGrounded = colls.Length > 0;

            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, jumpAmount);
            }
        }
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathArea"))
        {
            death();
        }

        if (other.CompareTag("Jump"))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, 8);
            other.gameObject.GetComponent<Animator>().SetTrigger("jump");
        }

        if (other.CompareTag("Win"))
        {
            win = true;
            myMath.waitAndStart(3, () =>
            {
                win = false;
                GoNextLevel();
                GoEditMode();
            });
        }
    }
    public void GoEditMode()
    {
        GameBoy gb = FindAnyObjectByType<GameBoy>();

        gb.OnEditMode = true;



    }

    public void GoNextLevel()
    {
        LevelEditor lE = FindAnyObjectByType<LevelEditor>();
        lE.GoNextLevel();
    }
    public void death()
    {
        transform.position = checkpoint.position;
    }
}
