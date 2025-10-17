using UnityEngine;
using UnityEngine.UI;

public class PopupSetting : BasePopup
{
    [SerializeField]
    private Button btnClose;

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
}
