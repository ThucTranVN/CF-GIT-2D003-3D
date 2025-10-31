using UnityEngine;

[CreateAssetMenu(fileName = "GlobalConfig", menuName = "Scriptable Objects/GlobalConfig")]
public class GlobalConfig : ScriptableObject
{
    [Header("Player")]
    public float MoveSpeed;
    public float TurnSpeed;
    public float JumpHeight;
    public float MaxHealth;

    [Header("AI")]
    public float AIMaaxHeath;
    public float DieForce = 10f;
    public float StopDistanceLimit = 1.5f;


    [Header("UI")]
    public float GameLoadingTime = 2f;


}
