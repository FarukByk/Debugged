using UnityEngine;

public class CharCont : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    public Transform CameraTransform;
    Animator animator;
    float rotation;
    HoldBlockCode holdingSystem;
    bool walkable;
    CapsuleCollider CapsuleCollider;
    public bool stopAction;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        holdingSystem = GetComponent<HoldBlockCode>();
        CapsuleCollider = GetComponent<CapsuleCollider>();  
    }

    public void StopAction()
    {
        stopAction = true;
        animator.SetTrigger("put");
    }
    void Update()
    {
        if (stopAction)
        {
            rb.linearVelocity = Vector3.zero;
            animator.SetBool("walk", false);
            CapsuleCollider.enabled = false;
            holdingSystem.enabled = false;
        }
        else
        {
            holdingSystem.enabled = true;
            CapsuleCollider.enabled = true;
            walkable = !holdingSystem.putting;
            CameraTransform.position = transform.position;
            Vector3 Inputs = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            if (walkable)
            {
                rb.linearVelocity = CameraTransform.rotation * Inputs * speed;

                animator.SetBool("walk", Inputs != Vector3.zero);

                if (Inputs != Vector3.zero)
                {
                    rotation = Mathf.Atan2(Inputs.x, Inputs.z) * Mathf.Rad2Deg;
                }

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, rotation + CameraTransform.localEulerAngles.y, 0), Time.deltaTime * 15);
            }
            else
            {
                rb.linearVelocity = Vector3.zero;
            }
        }
        
    }
}
