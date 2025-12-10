using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class TMPGlowController : MonoBehaviour
{
    public TextMeshProUGUI targetText; // Assign your TextMeshProUGUI component in the Inspector
    public Color glowColor = Color.yellow; // The desired glow color
    [Range(0f, 1f)]
    public float glowPower = 0.5f; // The desired glow power

    private float lastGlowPower = -1f; // Track the last glow power value

    void Start()
    {
        if (targetText == null)
        {
            Debug.LogError("TextMeshProUGUI component not assigned to targetText!");
            return;
        }

        // Ensure the glow keyword is enabled on the material
        // This is necessary if the glow was not enabled in the Inspector
        targetText.fontSharedMaterial.EnableKeyword("GLOW_ON");

        // Set the glow color
        targetText.fontSharedMaterial.SetColor(ShaderUtilities.ID_GlowColor, glowColor);

        // Set the glow power
        targetText.fontSharedMaterial.SetFloat(ShaderUtilities.ID_GlowPower, glowPower);
        lastGlowPower = glowPower;

        // Update the mesh padding to ensure the glow renders correctly
        targetText.UpdateMeshPadding();
    }

    void Update()
    {
        // Update glow power if changed in Inspector at runtime
        if (targetText != null && glowPower != lastGlowPower)
        {
            SetGlowPower(glowPower);
        }
    }

    // Method to change glow power at runtime
    public void SetGlowPower(float newPower)
    {
        if (targetText == null) return;

        glowPower = Mathf.Clamp01(newPower); // Clamp between 0 and 1
        targetText.fontSharedMaterial.SetFloat(ShaderUtilities.ID_GlowPower, glowPower);
        targetText.UpdateMeshPadding();
        lastGlowPower = glowPower;
    }

    // Example of how to change glow dynamically
    public void SetGlowParameters(Color newColor, float newPower)
    {
        if (targetText == null) return;

        glowColor = newColor;
        glowPower = Mathf.Clamp01(newPower);
        targetText.fontSharedMaterial.SetColor(ShaderUtilities.ID_GlowColor, newColor);
        targetText.fontSharedMaterial.SetFloat(ShaderUtilities.ID_GlowPower, glowPower);
        targetText.UpdateMeshPadding();
        lastGlowPower = glowPower;
    }
}
