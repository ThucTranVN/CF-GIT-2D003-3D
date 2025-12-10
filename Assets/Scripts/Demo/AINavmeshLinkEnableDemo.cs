using UnityEngine;
using Unity.AI.Navigation;

/// <summary>
/// Demo script demonstrating dynamic NavMesh link activation based on ground detection.
/// Enables/disables upward and downward NavMesh links depending on what surface the object is above.
/// </summary>
public class AINavmeshLinkEnableDemo : MonoBehaviour
{
    /// <summary>
    /// Raycast hit information for ground detection.
    /// </summary>
    private RaycastHit hit;
    
    [Header("NavMesh Link Arrays")]
    /// <summary>
    /// Array of NavMesh links for upward movement (e.g., jumping onto obstacles).
    /// </summary>
    public NavMeshLink[] linksUpArr;
    
    /// <summary>
    /// Array of NavMesh links for downward movement (e.g., dropping from platforms).
    /// </summary>
    public NavMeshLink[] linksDownArr;

    /// <summary>
    /// Checks the surface below and activates appropriate NavMesh links.
    /// Called at fixed intervals for consistent physics-based detection.
    /// </summary>
    void FixedUpdate()
    {
        // Cast a ray downward to detect what surface is below
        if(Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            // If above ground, enable downward links and disable upward links
            if (hit.collider.CompareTag("Ground"))
            {
                for(int i = 0; i < linksUpArr.Length; i++)
                {
                    linksUpArr[i].activated = false;
                    linksDownArr[i].activated = true;
                }
            }

            // If above obstacle, enable upward links and disable downward links
            if (hit.collider.CompareTag("Obstacle"))
            {
                for (int i = 0; i < linksUpArr.Length; i++)
                {
                    linksUpArr[i].activated = true;
                    linksDownArr[i].activated = false;
                }
            }
        }
    }
}
