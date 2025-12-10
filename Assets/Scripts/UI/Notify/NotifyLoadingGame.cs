using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Loading screen notify that displays progress while loading the game scene.
/// Shows loading progress and requires player input to continue after loading completes.
/// </summary>
public class NotifyLoadingGame : BaseNotify
{
    [Header("UI References")]
    [SerializeField]
    /// <summary>
    /// TextMeshPro text component displaying loading status messages.
    /// </summary>
    private TMP_Text txtLoading;
    
    [SerializeField]
    /// <summary>
    /// Slider component displaying loading progress (0 to 1).
    /// </summary>
    private Slider sldLoading;

    /// <summary>
    /// Initializes the loading notify.
    /// </summary>
    public override void Init()
    {
        base.Init();
    }

    /// <summary>
    /// Shows the loading notify and starts the scene loading coroutine.
    /// </summary>
    /// <param name="data">Optional data to pass to the notify.</param>
    public override void Show(object data)
    {
        base.Show(data);
        StartCoroutine(LoadScene());
    }

    /// <summary>
    /// Hides the loading notify.
    /// </summary>
    public override void Hide()
    {
        base.Hide();
    }

    /// <summary>
    /// Coroutine that loads the game scene asynchronously and updates the loading UI.
    /// Waits for player input (Space key) before activating the scene.
    /// </summary>
    private IEnumerator LoadScene()
    {
        yield return null;

        // Start loading the game scene asynchronously
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Terrain");
        asyncOperation.allowSceneActivation = false; // Don't activate immediately
        
        // Update loading progress while scene is loading
        while (!asyncOperation.isDone)
        {
            // Update progress bar and text
            sldLoading.value = asyncOperation.progress;
            txtLoading.text = $"Loading {asyncOperation.progress * 100}%";

            // When loading is nearly complete (90%+), wait for player input
            if(asyncOperation.progress >= 0.9f)
            {
                sldLoading.value = 1f;
                txtLoading.text = $"Press space bar to continue";

                // Wait for player to press Space before activating scene
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    this.Hide();
                    if (UIManager.HasInstance)
                    {
                        // Show fade overlap for smooth transition
                        UIManager.Instance.ShowOverlap<OverlapFade>();

                        OverlapFade overlapFade = UIManager.Instance.GetExistOverlap<OverlapFade>();

                        // Perform fade transition
                        overlapFade.Fade(
                            fadeTime: 1f,
                            onDuringFade: () =>
                            {
                                // Activate scene during fade
                                asyncOperation.allowSceneActivation = true;
                            },
                            onFinish: () =>
                            {
                                // Show game screen after fade completes
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
