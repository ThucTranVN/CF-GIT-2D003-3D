using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

/// <summary>
/// Fade overlay effect for smooth scene transitions.
/// Uses DOTween to create fade in/out animations with callback support.
/// </summary>
public class OverlapFade : BaseOverlap
{
    [SerializeField]
    /// <summary>
    /// Image component used for the fade effect (full-screen overlay).
    /// </summary>
    private Image imgFade;
    
    [SerializeField]
    /// <summary>
    /// Color to use for the fade effect (typically black or white).
    /// </summary>
    private Color fadeColor;

    /// <summary>
    /// Hides the fade overlap.
    /// </summary>
    public override void Hide()
    {
        base.Hide();
    }

    /// <summary>
    /// Shows the fade overlap.
    /// </summary>
    /// <param name="data">Optional data to pass to the overlap.</param>
    public override void Show(object data)
    {
        base.Show(data);
    }

    /// <summary>
    /// Initializes the fade overlap.
    /// </summary>
    public override void Init()
    {
        base.Init();
    }

    /// <summary>
    /// Performs a fade in/out animation sequence.
    /// Fades to black, invokes a callback during the fade, then fades back out.
    /// </summary>
    /// <param name="fadeTime">Duration (in seconds) for each fade (in and out).</param>
    /// <param name="onDuringFade">Callback invoked when fade reaches maximum opacity (during black screen).</param>
    /// <param name="onFinish">Callback invoked when fade out completes.</param>
    public void Fade(float fadeTime, Action onDuringFade, Action onFinish)
    {
        // Set fade color
        imgFade.color = fadeColor;
        UIManager.Instance.UICamera.backgroundColor = fadeColor;
        // Start with transparent
        SetAlpha(0);
        
        // Create DOTween sequence for fade animation
        Sequence sequence = DOTween.Sequence();
        // Fade in to black
        sequence.Append(imgFade.DOFade(1f, fadeTime));
        // Invoke callback during full fade (good time to switch scenes)
        sequence.AppendCallback(() => { onDuringFade.Invoke(); });
        // Fade out from black
        sequence.Append(imgFade.DOFade(0f, fadeTime));
        // Invoke finish callback when animation completes
        sequence.OnComplete(() =>
        {
            onFinish.Invoke();
        });
    }

    /// <summary>
    /// Sets the alpha value of the fade image and UI camera background.
    /// </summary>
    /// <param name="alphaValue">Alpha value to set (0 = transparent, 1 = opaque).</param>
    private void SetAlpha(float alphaValue)
    {
        Color color = imgFade.color;
        color.a = alphaValue;
        imgFade.color = color;
        // Also set camera background to match for seamless fade
        UIManager.Instance.UICamera.backgroundColor = color;
    }
}
