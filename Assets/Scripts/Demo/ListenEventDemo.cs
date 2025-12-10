using UnityEngine;
using TMPro;

/// <summary>
/// Demo script demonstrating how to listen to events through the ListenerManager.
/// Registers listeners for health and weapon change events and updates UI accordingly.
/// </summary>
public class ListenEventDemo : MonoBehaviour
{
    [Header("UI References")]
    /// <summary>
    /// TextMeshPro text component for displaying health value.
    /// </summary>
    public TMP_Text healthText;
    
    /// <summary>
    /// TextMeshPro text component for displaying weapon name.
    /// </summary>
    public TMP_Text weaponNameText;
    
    /// <summary>
    /// TextMeshPro text component for displaying weapon ammo count.
    /// </summary>
    public TMP_Text weaponAmmoText;
    
    /// <summary>
    /// Current health value stored locally.
    /// </summary>
    private int healthValue = 0;

    /// <summary>
    /// Registers event listeners for health updates and weapon changes.
    /// </summary>
    void Start()
    {
        if (ListenerManager.HasInstance)
        {
            // Register listener for health update events
            ListenerManager.Instance.Register(
                ListenType.ON_UPDATE_PLAYER_HEALTH, OnUpdatePlayerHealth);

            // Register listener for weapon change events
            ListenerManager.Instance.Register(
                ListenType.ON_PLAYER_CHANGE_WEAPON, OnPlayerChangeWeapon);
        }
    }

    /// <summary>
    /// Unregisters all event listeners when the object is destroyed.
    /// Important to prevent memory leaks and null reference errors.
    /// </summary>
    void OnDestroy()
    {
        if (ListenerManager.HasInstance)
        {
            // Unregister health update listener
            ListenerManager.Instance.UnRegister(
                ListenType.ON_UPDATE_PLAYER_HEALTH, OnUpdatePlayerHealth);

            // Unregister weapon change listener
            ListenerManager.Instance.UnRegister(
                ListenType.ON_PLAYER_CHANGE_WEAPON, OnPlayerChangeWeapon);
        }
    }

    /// <summary>
    /// Callback method invoked when a health update event is broadcast.
    /// Updates the health text display with the new health value.
    /// </summary>
    /// <param name="value">The health value passed from the event (expected to be an int).</param>
    private void OnUpdatePlayerHealth(object value)
    {
        if(value != null)
        {
            // Type check and cast to int
            if(value is int health)
            {
                healthValue = health;
                healthText.text = $"Health: {healthValue}";
            }
        }
    }

    /// <summary>
    /// Callback method invoked when a weapon change event is broadcast.
    /// Updates the weapon name and ammo text displays with the new weapon information.
    /// </summary>
    /// <param name="value">The WeaponInfo object passed from the event.</param>
    private void OnPlayerChangeWeapon(object value)
    {
        if(value != null)
        {
            // Type check and cast to WeaponInfo
            if(value is WeaponInfo info)
            {
                weaponNameText.text = $"Weapon: {info.WeaponName}";
                weaponAmmoText.text = $"Ammo: {info.WeaponAmmo}";
            }
        }
    }
}
