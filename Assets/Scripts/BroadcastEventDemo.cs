using UnityEngine;

public class BroadcastEventDemo : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ListenerManager.HasInstance)
            {
                int healthValue = Random.Range(10, 100);
                ListenerManager.Instance.BroadCast(
                    ListenType.ON_UPDATE_PLAYER_HEALTH, healthValue);
            }
        }

        HandleChangeWeapon();
    }

    private void HandleChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (ListenerManager.HasInstance)
            {
                WeaponInfo weaponInfo = new(weaponName: "Riffle", weaponAmmo: Random.Range(20, 50));
                ListenerManager.Instance.BroadCast(ListenType.ON_PLAYER_CHANGE_WEAPON,
                    weaponInfo);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            WeaponInfo weaponInfo = new(weaponName: "Pistol", weaponAmmo: Random.Range(5, 15));
            ListenerManager.Instance.BroadCast(ListenType.ON_PLAYER_CHANGE_WEAPON,
                weaponInfo);
        }
    }
}
