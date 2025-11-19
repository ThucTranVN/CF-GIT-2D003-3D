using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float collisionCooldown = 1f;
    [SerializeField]
    private float adjustChunkMoveSpeed = -2f;
    private float coolDownTimer = 0f;

    private const string hitString = "Hit";


    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);

        if (coolDownTimer < collisionCooldown) return;

        if (LevelGenerator.HasInstance)
        {
            LevelGenerator.Instance.ChangeChunkMoveSpeed(adjustChunkMoveSpeed);
        }
        animator.SetTrigger(hitString);
        coolDownTimer = 0f;
    }

    private void Update()
    {
        coolDownTimer += Time.deltaTime;
    }
}
