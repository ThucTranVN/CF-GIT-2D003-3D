using UnityEngine;

/// <summary>
/// Trigger zone that destroys any objects that enter it.
/// Used to clean up obstacles and other objects that have passed behind the camera.
/// </summary>
public class ObstacleDestroyer : MonoBehaviour
{
    /// <summary>
    /// Called when another collider enters the trigger zone.
    /// Destroys the entering GameObject to prevent accumulation of off-screen objects.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        Destroy(other.gameObject);
    }
}
