using TMPro;
using UnityEngine;

/// <summary>
/// Demo script demonstrating animation layer weight blending based on health.
/// As health decreases, the injured animation layer weight increases smoothly.
/// </summary>
public class AnimationLayerDemo : MonoBehaviour
{
    [SerializeField]
    /// <summary>
    /// Reference to the Animator component controlling character animations.
    /// </summary>
    private Animator m_Animator;
    
    [SerializeField]
    /// <summary>
    /// TextMeshPro text component displaying current health value.
    /// </summary>
    private TextMeshProUGUI m_HealthText;
    
    [SerializeField]
    [Range(0f, 1f)]
    /// <summary>
    /// Maximum weight value for the injured animation layer (0-1 range).
    /// </summary>
    private float m_MaximumInjuredLayerWeight;
    
    /// <summary>
    /// Maximum health value for the character.
    /// </summary>
    private float m_MaxHealth = 100f;
    
    /// <summary>
    /// Current health value of the character.
    /// </summary>
    private float m_CurrentHealth;
    
    /// <summary>
    /// Index of the "Injured Layer" in the Animator controller.
    /// </summary>
    private int m_InjuredLayerIndex;
    
    /// <summary>
    /// Velocity reference for SmoothDamp function to smoothly transition layer weights.
    /// </summary>
    private float layerWeightVelocity;

    /// <summary>
    /// Gets the Animator component reference.
    /// </summary>
    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Initializes health values and gets the injured layer index from the Animator.
    /// </summary>
    void Start()
    {
        m_CurrentHealth = m_MaxHealth;
        m_InjuredLayerIndex = m_Animator.GetLayerIndex("Injured Layer");
    }

    /// <summary>
    /// Handles health damage input and updates animation layer weight based on health percentage.
    /// </summary>
    void Update()
    {
        // Reduce health when Space key is pressed (demo input)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_CurrentHealth -= m_MaxHealth / 10;

            // Reset health to max if it goes below zero
            if (m_CurrentHealth < 0)
            {
                m_CurrentHealth = m_MaxHealth;
            }
        }

        // Update health display text
        m_HealthText.text = $"Health {m_CurrentHealth}";

        // Calculate health as a percentage (0 to 1)
        float healthPercentage = m_CurrentHealth / m_MaxHealth;

        // Get current injured layer weight
        float currentInjuredLayerWeight = m_Animator.GetLayerWeight(m_InjuredLayerIndex);

        // Calculate target weight: inverse of health (lower health = higher injured weight)
        float targetInjuredLayerWeight = (1 - healthPercentage);

        // Smoothly transition to target weight using SmoothDamp for natural animation blending
        m_Animator.SetLayerWeight(m_InjuredLayerIndex,
            Mathf.SmoothDamp(currentInjuredLayerWeight, targetInjuredLayerWeight,
            ref layerWeightVelocity, 0.2f));
    }
}
