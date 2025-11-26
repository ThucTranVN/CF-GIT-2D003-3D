using UnityEngine;
using System.Linq;

public class RagdollDemo : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody[] ragdollRigidbodies;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        DisableRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerRagdoll(Vector3 force, Vector3 hitPoint)
    {
        EnableRagdoll();
        //Rigidbody hitRigidbody = ragdollRigidbodies
        //    .OrderBy(rigidbody => Vector3.Distance(rigidbody.position, hitPoint)).First();
        Rigidbody hitRigidbody = FindClosetHitRigidbody(hitPoint);
        if (hitRigidbody != null)
        {
            hitRigidbody.AddForceAtPosition(force, hitPoint, ForceMode.Impulse);
        }
    }

    private Rigidbody FindClosetHitRigidbody(Vector3 hitPoint)
    {
        Rigidbody closetRigidbody = null;
        float closestDistance = 0;

        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            float distance = Vector3.Distance(rb.position, hitPoint);
            if (closetRigidbody == null || distance < closestDistance)
            {
                closestDistance = distance;
                closetRigidbody = rb;
            }
        }

        return closetRigidbody;
    }

    private void DisableRagdoll()
    {
        if(ragdollRigidbodies?.Length > 0)
        {
            foreach (Rigidbody rb in ragdollRigidbodies)
            {
                rb.isKinematic = true;
            }

            animator.enabled = true;
        }
    }

    private void EnableRagdoll()
    {
        if (ragdollRigidbodies?.Length > 0)
        {
            foreach (Rigidbody rb in ragdollRigidbodies)
            {
                rb.isKinematic = false;
            }

            animator.enabled = false;
        }
    }
}
