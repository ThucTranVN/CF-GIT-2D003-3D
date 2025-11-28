using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField]
    protected GameObject pickupEffect;
    [SerializeField]
    protected Transform spawnEffectPosition;
    private const string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            //Debug.Log(other.gameObject.name);
            OnPickUp();
            Destroy(gameObject);
        }
    }

    protected abstract void OnPickUp();
}
