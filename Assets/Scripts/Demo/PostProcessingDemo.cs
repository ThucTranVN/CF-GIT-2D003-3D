using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Demo script demonstrating runtime modification of post-processing effects.
/// Increases vignette intensity on damage to create a visual feedback effect.
/// </summary>
public class PostProcessingDemo : MonoBehaviour
{
    [Header("Post Processing Reference")]
    /// <summary>
    /// Reference to the Volume component containing post-processing effects.
    /// </summary>
    public Volume postProcessVolume;
    
    /// <summary>
    /// Current vignette intensity value (0 to 0.6).
    /// </summary>
    private float value = 0f;

    /// <summary>
    /// Checks for input to trigger damage effect.
    /// </summary>
    void Update()
    {
        // Trigger damage effect on left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            OnDamage();
        }
    }

    /// <summary>
    /// Applies damage visual effect by increasing vignette intensity.
    /// Simulates taking damage by darkening the screen edges.
    /// </summary>
    private void OnDamage()
    {
        // Try to get the Vignette effect from the post-processing volume
        if(postProcessVolume.profile.TryGet(out Vignette vignette))
        {
            // Increase vignette intensity
            value += 0.2f;
            // Clamp value between 0 and 0.6 to prevent over-darkening
            value = Mathf.Clamp(value, 0, 0.6f);
            // Apply the new intensity value to the vignette effect
            vignette.intensity.value = value;
        }
    }
}
