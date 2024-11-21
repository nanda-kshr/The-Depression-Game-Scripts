using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; 
    private Animator animator;   
    private Rigidbody2D rb;     
    private Vector3 originalScale;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
    }

    private void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");  
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);  


        if (moveInput != 0)
        {
            animator.SetBool("isWalking", true); 
        }
        else
        {
            animator.SetBool("isWalking", false); 
        }


        if (moveInput > 0)
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);  
        else if (moveInput < 0)
            transform.localScale = new Vector3(- originalScale.x, originalScale.y, originalScale.z); 
    }
}

