using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using System;
using System.Threading.Tasks;

public class UnitaskDemo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async Task Start()
    {
        DemoAsync();
    }

    async UniTaskVoid DemoAsync()
    {
        // await frame-based operation like a coroutine
        await UniTask.Delay(TimeSpan.FromSeconds(2), ignoreTimeScale: false);

        // get async webrequest
        async UniTask<string> GetTextAsync(UnityWebRequest req)
        {
            var op = await req.SendWebRequest();
            return op.downloadHandler.text;
        }

        var task1 = GetTextAsync(UnityWebRequest.Get("https://www.google.com"));
        var task2 = GetTextAsync(UnityWebRequest.Get("https://www.bing.com"));
        var task3 = GetTextAsync(UnityWebRequest.Get("https://www.yahoo.com"));

        // concurrent async-wait and get results easily by tuple syntax
        var (google, bing, yahoo) = await UniTask.WhenAll(task1, task2, task3);

        Debug.Log(google);
        Debug.Log(bing);
        Debug.Log(yahoo);
    }
}
