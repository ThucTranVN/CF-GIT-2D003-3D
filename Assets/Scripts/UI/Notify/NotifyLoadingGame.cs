using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class NotifyLoadingGame : BaseNotify
{
    [SerializeField]
    private TMP_Text txtLoading;
    [SerializeField]
    private Slider sldLoading;

    public override void Init()
    {
        base.Init();
    }

    public override void Show(object data)
    {
        base.Show(data);
        StartCoroutine(LoadScene());
    }

    public override void Hide()
    {
        base.Hide();
    }

    private IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Terrain");
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            sldLoading.value = asyncOperation.progress;
            txtLoading.text = $"Loading {asyncOperation.progress * 100}%";

            if(asyncOperation.progress >= 0.9f)
            {
                sldLoading.value = 1f;
                txtLoading.text = $"Press space bar to continue";

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    this.Hide();
                    if (UIManager.HasInstance)
                    {
                        UIManager.Instance.ShowOverlap<OverlapFade>();

                        OverlapFade overlapFade = UIManager.Instance.GetExistOverlap<OverlapFade>();

                        overlapFade.Fade(
                            fadeTime: 1f,
                            onDuringFade: () =>
                            {
                                asyncOperation.allowSceneActivation = true;
                            },
                            onFinish: () =>
                            {
                                UIManager.Instance.ShowScreen<ScreenGame>();
                                overlapFade.Hide();
                            });
                    }
                } 
            }

            yield return null;
        }
    }
}
