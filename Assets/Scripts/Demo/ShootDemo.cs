using UnityEngine;

public class ShootDemo : MonoBehaviour
{
    [SerializeField]
    private float maximumForce;
    [SerializeField]
    private float maximumForceTime;

    private float timeHoldMouseButtonDown;
    private Camera camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            timeHoldMouseButtonDown = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                RagdollDemo ragdollDemo = hitInfo.collider.GetComponentInParent<RagdollDemo>();

                if(ragdollDemo != null)
                {
                    float mouseButtonDownDuration = Time.time - timeHoldMouseButtonDown;
                    float forcePercentage = mouseButtonDownDuration / maximumForceTime;
                    float forceMagnitude = Mathf.Lerp(1, maximumForce, forcePercentage);

                    Vector3 forceDirection = ragdollDemo.transform.position - camera.transform.position;
                    forceDirection.y = 1;
                    forceDirection.Normalize();

                    Vector3 force = forceMagnitude * forceDirection;

                    ragdollDemo.TriggerRagdoll(force, hitInfo.point);
                }
            }
        }
    }
}
