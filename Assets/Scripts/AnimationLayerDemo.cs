using TMPro;
using UnityEngine;

public class AnimationLayerDemo : MonoBehaviour
{
    [SerializeField]
    private Animator m_Animator;
    [SerializeField]
    private TextMeshProUGUI m_HealthText;
    [SerializeField]
    [Range(0f, 1f)]
    private float m_MaximumInjuredLayerWeight;
    private float m_MaxHealth = 100f;
    private float m_CurrentHealth;
    private int m_InjuredLayerIndex;
    private float layerWeightVelocity;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_CurrentHealth = m_MaxHealth;
        m_InjuredLayerIndex = m_Animator.GetLayerIndex("Injured Layer");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_CurrentHealth -= m_MaxHealth / 10;

            if (m_CurrentHealth < 0)
            {
                m_CurrentHealth = m_MaxHealth;
            }
        }

        m_HealthText.text = $"Health {m_CurrentHealth}";

        float healthPercentage = m_CurrentHealth / m_MaxHealth;

        float currentInjuredLayerWeight = m_Animator.GetLayerWeight(m_InjuredLayerIndex);

        float targetInjuredLayerWeight = (1 - healthPercentage);

        m_Animator.SetLayerWeight(m_InjuredLayerIndex,
            Mathf.SmoothDamp(currentInjuredLayerWeight, targetInjuredLayerWeight,
            ref layerWeightVelocity, 0.2f));
    }
}
