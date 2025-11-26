using UnityEngine;

public class PlayerMovementDemo : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;
    public float moveSpeed;
    public float rotationMaxDegree;
    public float jumpHeight;

    private float yGravity;
    private bool isJumping;
    private bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = moveDirection.magnitude;

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    inputMagnitude *= 2;
        //}

        animator.SetFloat("InputMagnitude", inputMagnitude, 0.05f, Time.deltaTime);

        moveDirection.Normalize();

        //transform.Translate(moveDirection * moveSpeed * Time.deltaTime,
        //    Space.World);

        yGravity += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            yGravity = -0.5f;
            animator.SetBool("IsGrounded", true);
            isGrounded = true;
            animator.SetBool("IsJumping", false);
            isJumping = false;
            animator.SetBool("IsFalling", false);

            if (Input.GetButtonDown("Jump"))
            {
                yGravity = jumpHeight;
                animator.SetBool("IsJumping", true);
                isJumping = true;
            }
        }
        else
        {
            animator.SetBool("IsGrounded", false);
            isGrounded = false;

            if((isJumping && yGravity < 0) || yGravity < -3.5f)
            {
                animator.SetBool("IsFalling", true);
            }
        }



        if (moveDirection != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(moveDirection,
                Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                toRotation, rotationMaxDegree * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        if (!isGrounded)
        {
            Vector3 velocity = moveDirection * inputMagnitude * jumpHeight;
            velocity.y = yGravity;
            characterController.Move(velocity * Time.deltaTime);
        }
    }

    private void OnAnimatorMove()
    {
        Vector3 velocity = animator.deltaPosition;
        velocity.y = yGravity * Time.deltaTime;
        characterController.Move(velocity);
    }
}
