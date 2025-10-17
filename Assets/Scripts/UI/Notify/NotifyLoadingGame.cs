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
                if (UIManager.HasInstance)
                {
                    //UIManager.Instance.ShowOverlap<OverlapFade>();
                    UIManager.Instance.ShowScreen<ScreenGame>();
                }

                asyncOperation.allowSceneActivation = true;
                this.Hide();
            }

            yield return null;
        }
    }
}
