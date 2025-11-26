using UnityEngine;

public class AnimatorOverrideControllerDemo : MonoBehaviour
{
    [SerializeField]
    private Animator m_Animator;
    private int IS_MOVING_PARM = Animator.StringToHash("IsMoving");

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            m_Animator.SetBool(IS_MOVING_PARM, true);
        }
        else
        {
            m_Animator.SetBool(IS_MOVING_PARM, false);
        }
    }
}
