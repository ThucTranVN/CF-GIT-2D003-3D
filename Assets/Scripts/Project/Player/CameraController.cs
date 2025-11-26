using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : BaseManager<CameraController>
{
    [SerializeField]
    private CinemachineCamera cinemachineCamera;

    [SerializeField]
    private float minFOV = 20f;
    [SerializeField]
    private float maxFOV = 120f;
    [SerializeField]
    private float zoomDuration = 1f;
    [SerializeField]
    private float zoomSpeedModifier = 1f;

    protected override void Awake()
    {
        base.Awake();
        cinemachineCamera = GetComponentInChildren<CinemachineCamera>();
    }

    public void ChangeCameraFOV(float speedAmount)
    {
        StartCoroutine(ChangeFOVRoutine(speedAmount));
    }

    private IEnumerator ChangeFOVRoutine(float speedAmount)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float targetFOV = Mathf.Clamp(startFOV + speedAmount * zoomSpeedModifier, minFOV, maxFOV);

        float elapsedTime = 0f;

        while (elapsedTime < zoomDuration)
        {
            float t = elapsedTime / zoomDuration;
            elapsedTime += Time.fixedUnscaledDeltaTime;

            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
            yield return null;
        }

        cinemachineCamera.Lens.FieldOfView = targetFOV;
    }
}
