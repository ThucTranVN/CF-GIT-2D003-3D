using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingDemo : MonoBehaviour
{
    public Volume postProcessVolume;
    private float value = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnDamage();
        }
    }

    private void OnDamage()
    {
        if(postProcessVolume.profile.TryGet(out Vignette vignette))
        {
            value += 0.2f;
            value = Mathf.Clamp(value, 0, 0.6f);
            vignette.intensity.value = value;
        }
    }
}
