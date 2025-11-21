using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseManager<PlayerController>
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float xClamp = 2f;
    [SerializeField]
    private float yClamp = 2f;
    private Vector2 movement;
    private Rigidbody rigidbody;

    protected override void Awake()
    {
        base.Awake();

        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleMoveMent();
    }


    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    private void HandleMoveMent()
    {
        Vector3 currentPosition = rigidbody.position;
        Vector3 moveDirection = new Vector3(movement.x, 0f, movement.y);
        Vector3 newPosition = currentPosition + moveDirection * (moveSpeed * Time.fixedDeltaTime);
        newPosition.x = Mathf.Clamp(newPosition.x, -xClamp, xClamp);
        newPosition.y = Mathf.Clamp(newPosition.y, -yClamp, yClamp);
        rigidbody.MovePosition(newPosition);
    }
}
