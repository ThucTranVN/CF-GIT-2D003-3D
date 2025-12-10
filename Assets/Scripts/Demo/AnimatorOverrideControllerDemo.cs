using UnityEngine;

/// <summary>
/// Demo script demonstrating Animator parameter control using string hashing.
/// Sets the "IsMoving" animator parameter based on Space key input.
/// </summary>
public class AnimatorOverrideControllerDemo : MonoBehaviour
{
    [SerializeField]
    /// <summary>
    /// Reference to the Animator component.
    /// </summary>
    private Animator m_Animator;
    
    /// <summary>
    /// Hashed integer ID for the "IsMoving" animator parameter.
    /// Using StringToHash is more efficient than passing strings directly to Animator methods.
    /// </summary>
    private int IS_MOVING_PARM = Animator.StringToHash("IsMoving");

    /// <summary>
    /// Gets the Animator component reference.
    /// </summary>
    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Updates the IsMoving animator parameter based on Space key input.
    /// </summary>
    void Update()
    {
        // Set IsMoving to true while Space key is held
        if (Input.GetKey(KeyCode.Space))
        {
            m_Animator.SetBool(IS_MOVING_PARM, true);
        }
        else
        {
            // Set IsMoving to false when Space key is released
            m_Animator.SetBool(IS_MOVING_PARM, false);
        }
    }
}
