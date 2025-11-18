using UnityEngine;

public class Pickup : MonoBehaviour
{
    private const string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            //Debug.Log(other.gameObject.name);
        }
    }
}
