using UnityEngine;
using UnityEngine.UI;

public class ScreenHome : BaseScreen
{
    [SerializeField]
    private Button btnSetting;

    public override void Init()
    {
        base.Init();
    }

    public override void Show(object data)
    {
        base.Show(data);
    }

    public override void Hide()
    {
        base.Hide();
    }

    public void OnClickSettingButton()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowPopup<PopupSetting>();
        }
    }

    public void OnClickStartGame()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowNotify<NotifyLoadingGame>();
        }

        this.Hide();
    }

}
