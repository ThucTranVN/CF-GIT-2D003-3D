using UnityEngine;

public class PlayerMovementDemo : MonoBehaviour
{
    public CharacterController characterController;
    public float moveSpeed;
    public float rotationMaxDegree;
    public float jumpHeight;

    private float yGravity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        moveDirection.Normalize();

        //transform.Translate(moveDirection * moveSpeed * Time.deltaTime,
        //    Space.World);

        yGravity += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                yGravity = jumpHeight;
            }
        }

        Vector3 velocity = moveDirection * moveSpeed;
        velocity.y = yGravity;

        characterController.Move(velocity * Time.deltaTime);

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection,
                Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                toRotation, rotationMaxDegree * Time.deltaTime);
        }
    }
}
