using UnityEngine;
using Unity.Cinemachine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private float shakeModifier = 0.5f;
    private CinemachineImpulseSource impulseSource;
    [SerializeField]
    private ParticleSystem collisionEffect;

    void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        FireImpulse();
        CollisionEffect(collision);
    }

    private void FireImpulse()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = (1f / distance) * shakeModifier;
        shakeIntensity = Mathf.Min(shakeIntensity, 1f);
        impulseSource.GenerateImpulse(shakeIntensity);
    }

    private void CollisionEffect(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];
        collisionEffect.transform.position = contactPoint.point;
        collisionEffect.Play();
    }
}
