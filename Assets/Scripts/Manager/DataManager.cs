using UnityEngine;

public class DataManager : BaseManager<DataManager>
{
    [SerializeField]
    private GlobalConfig GlobalConfig;

    public float GetMoveSpeed()
    {
        return GlobalConfig.MoveSpeed;
    }

    public void SetMoveSpeed(float value)
    {
        GlobalConfig.MoveSpeed = value;
    }
}
