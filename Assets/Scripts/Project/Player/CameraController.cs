using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// Manager for controlling camera field of view changes.
/// Smoothly transitions camera FOV when level speed changes to create a speed effect.
/// </summary>
public class CameraController : BaseManager<CameraController>
{
    [SerializeField]
    /// <summary>
    /// Reference to the Cinemachine Camera component that controls the main camera.
    /// </summary>
    private CinemachineCamera cinemachineCamera;

    [Header("FOV Settings")]
    [SerializeField]
    /// <summary>
    /// Minimum allowed field of view value.
    /// </summary>
    private float minFOV = 20f;
    
    [SerializeField]
    /// <summary>
    /// Maximum allowed field of view value.
    /// </summary>
    private float maxFOV = 120f;
    
    [SerializeField]
    /// <summary>
    /// Duration (in seconds) for the FOV transition animation.
    /// </summary>
    private float zoomDuration = 1f;
    
    [SerializeField]
    /// <summary>
    /// Multiplier for converting speed amount to FOV change amount.
    /// </summary>
    private float zoomSpeedModifier = 1f;

    /// <summary>
    /// Initializes the Cinemachine Camera reference.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        cinemachineCamera = GetComponentInChildren<CinemachineCamera>();
    }

    /// <summary>
    /// Initiates a smooth FOV change based on the speed amount.
    /// Called when level speed increases to create a visual speed effect.
    /// </summary>
    /// <param name="speedAmount">The amount to change the FOV by (will be multiplied by zoomSpeedModifier).</param>
    public void ChangeCameraFOV(float speedAmount)
    {
        StartCoroutine(ChangeFOVRoutine(speedAmount));
    }

    /// <summary>
    /// Coroutine that smoothly transitions the camera FOV over time.
    /// </summary>
    /// <param name="speedAmount">The amount to change the FOV by.</param>
    private IEnumerator ChangeFOVRoutine(float speedAmount)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        // Calculate target FOV and clamp it within min/max bounds
        float targetFOV = Mathf.Clamp(startFOV + speedAmount * zoomSpeedModifier, minFOV, maxFOV);

        float elapsedTime = 0f;

        // Smoothly interpolate FOV over the zoom duration
        while (elapsedTime < zoomDuration)
        {
            float t = elapsedTime / zoomDuration;
            elapsedTime += Time.fixedUnscaledDeltaTime;

            // Lerp between start and target FOV
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
            yield return null;
        }

        // Ensure final FOV is exactly the target value
        cinemachineCamera.Lens.FieldOfView = targetFOV;
    }
}
