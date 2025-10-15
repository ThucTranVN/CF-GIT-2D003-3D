using UnityEngine;
using TMPro;

public class ListenEventDemo : MonoBehaviour
{
    public TMP_Text healthText;
    public TMP_Text weaponNameText;
    public TMP_Text weaponAmmoText;
    private int healthValue = 0;

    void Start()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(
                ListenType.ON_UPDATE_PLAYER_HEALTH, OnUpdatePlayerHealth);

            ListenerManager.Instance.Register(
                ListenType.ON_PLAYER_CHANGE_WEAPON, OnPlayerChangeWeapon);
        }
    }

    void OnDestroy()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.UnRegister(
                ListenType.ON_UPDATE_PLAYER_HEALTH, OnUpdatePlayerHealth);

            ListenerManager.Instance.UnRegister(
                ListenType.ON_PLAYER_CHANGE_WEAPON, OnPlayerChangeWeapon);
        }
    }

    private void OnUpdatePlayerHealth(object value)
    {
        if(value != null)
        {
            if(value is int health)
            {
                healthValue = health;
                healthText.text = $"Health: {healthValue}";
            }
        }
    }

    private void OnPlayerChangeWeapon(object value)
    {
        if(value != null)
        {
            if(value is WeaponInfo info)
            {
                weaponNameText.text = $"Weapon: {info.WeaponName}";
                weaponAmmoText.text = $"Ammo: {info.WeaponAmmo}";
            }
        }
    }
}
