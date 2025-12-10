using UnityEngine;

/// <summary>
/// Demo script demonstrating how to broadcast events through the ListenerManager.
/// Shows examples of broadcasting health updates and weapon change events.
/// </summary>
public class BroadcastEventDemo : MonoBehaviour
{
    /// <summary>
    /// Checks for input and broadcasts corresponding events.
    /// </summary>
    void Update()
    {
        // Broadcast health update event on left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            if (ListenerManager.HasInstance)
            {
                // Generate random health value and broadcast it
                int healthValue = Random.Range(10, 100);
                ListenerManager.Instance.BroadCast(
                    ListenType.ON_UPDATE_PLAYER_HEALTH, healthValue);
            }
        }

        // Handle weapon change input
        HandleChangeWeapon();
    }

    /// <summary>
    /// Handles weapon change input and broadcasts weapon change events.
    /// </summary>
    private void HandleChangeWeapon()
    {
        // Switch to Rifle weapon on key 1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (ListenerManager.HasInstance)
            {
                WeaponInfo weaponInfo = new(weaponName: "Riffle", weaponAmmo: Random.Range(20, 50));
                ListenerManager.Instance.BroadCast(ListenType.ON_PLAYER_CHANGE_WEAPON,
                    weaponInfo);
            }
        }
        // Switch to Pistol weapon on key 2
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (ListenerManager.HasInstance)
            {
                WeaponInfo weaponInfo = new(weaponName: "Pistol", weaponAmmo: Random.Range(5, 15));
                ListenerManager.Instance.BroadCast(ListenType.ON_PLAYER_CHANGE_WEAPON,
                    weaponInfo);
            }
        }
    }
}
