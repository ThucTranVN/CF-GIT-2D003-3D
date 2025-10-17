using UnityEngine;

public class GameManager : BaseManager<GameManager>
{
    void Start()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenHome>();
        }
    }
}
