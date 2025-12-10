/// <summary>
/// Data class containing weapon information.
/// Used for passing weapon data through events (e.g., weapon change events).
/// </summary>
public class WeaponInfo
{
    /// <summary>
    /// The name of the weapon (e.g., "Rifle", "Pistol").
    /// </summary>
    public string WeaponName;
    
    /// <summary>
    /// The current ammo count for the weapon.
    /// </summary>
    public int WeaponAmmo;

    /// <summary>
    /// Creates a new WeaponInfo instance with the specified name and ammo count.
    /// </summary>
    /// <param name="weaponName">The name of the weapon.</param>
    /// <param name="weaponAmmo">The ammo count for the weapon.</param>
    public WeaponInfo(string weaponName, int weaponAmmo)
    {
        WeaponName = weaponName;
        WeaponAmmo = weaponAmmo;
    }
}
