using UnityEngine;
using UnityEngine.UI;

public class PopupReward : BasePopup
{

    public override void Init()
    {
        base.Init();
    }

    public override void Hide()
    {
        base.Hide();
    }

    public override void Show(object data)
    {
        base.Show(data);
    }

    public void OnClickBtnContinue()
    {
        this.Hide();

        if (GameManager.HasInstance)
        {
            GameManager.Instance.ExitGame();
        }
    }
}
