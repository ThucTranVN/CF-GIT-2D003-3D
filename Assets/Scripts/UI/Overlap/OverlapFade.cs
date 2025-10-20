using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class OverlapFade : BaseOverlap
{
    [SerializeField]
    private Image imgFade;
    [SerializeField]
    private Color fadeColor;

    public override void Hide()
    {
        base.Hide();
    }

    public override void Show(object data)
    {
        base.Show(data);
    }

    public override void Init()
    {
        base.Init();
    }

    public void Fade(float fadeTime, Action onDuringFade, Action onFinish)
    {
        imgFade.color = fadeColor;
        UIManager.Instance.UICamera.backgroundColor = fadeColor;
        SetAlpha(0);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(imgFade.DOFade(1f, fadeTime)); //FadeIn
        sequence.AppendCallback(() => { onDuringFade.Invoke(); }); //Invoke OnDuringFade
        sequence.Append(imgFade.DOFade(0f, fadeTime)); //FadeOut 
        sequence.OnComplete(() =>
        {
            onFinish.Invoke();
        });
    }

    private void SetAlpha(float alphaValue)
    {
        Color color = imgFade.color;
        color.a = alphaValue;
        imgFade.color = color;
        UIManager.Instance.UICamera.backgroundColor = color;
    }
}
