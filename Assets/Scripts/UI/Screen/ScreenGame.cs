using UnityEngine;
using TMPro;

public class ScreenGame : BaseScreen
{
    [SerializeField]
    private TMP_Text txtCoin;
    [SerializeField]
    private TMP_Text txtTime;

    public override void Init()
    {
        base.Init();

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.ON_TIMER_CHANGE, OnTimerUpdate);
            ListenerManager.Instance.Register(ListenType.ON_PLAYER_UPDATE_COIN, OnPlayerUpdateCoin);
        }
    }

    public override void Show(object data)
    {
        base.Show(data);
    }

    public override void Hide()
    {
        base.Hide();

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.UnRegister(ListenType.ON_TIMER_CHANGE, OnTimerUpdate);
            ListenerManager.Instance.UnRegister(ListenType.ON_PLAYER_UPDATE_COIN, OnPlayerUpdateCoin);
        }
    }

    private void OnPlayerUpdateCoin(object value)
    {
        if(value != null && value is int coin)
        {
            txtCoin.text = coin.ToString();
        }
    }

    private void OnTimerUpdate(object value)
    {
        if(value != null && value is float time)
        {
            txtTime.text = time.ToString("F1");
        }
    }

}
