using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using System;
using System.Threading.Tasks;

/// <summary>
/// Demo script demonstrating UniTask usage for asynchronous operations.
/// Shows frame-based delays, async web requests, and concurrent task execution.
/// </summary>
public class UnitaskDemo : MonoBehaviour
{
    /// <summary>
    /// Starts the async demo when the component is initialized.
    /// </summary>
    async Task Start()
    {
        DemoAsync();
    }

    /// <summary>
    /// Demonstrates various UniTask features:
    /// - Frame-based delays
    /// - Async web requests
    /// - Concurrent task execution with WhenAll
    /// </summary>
    async UniTaskVoid DemoAsync()
    {
        // Wait for 2 seconds (frame-based operation similar to coroutines)
        await UniTask.Delay(TimeSpan.FromSeconds(2), ignoreTimeScale: false);

        // Local async function to fetch text from a web request
        async UniTask<string> GetTextAsync(UnityWebRequest req)
        {
            var op = await req.SendWebRequest();
            return op.downloadHandler.text;
        }

        // Create three concurrent web request tasks
        var task1 = GetTextAsync(UnityWebRequest.Get("https://www.google.com"));
        var task2 = GetTextAsync(UnityWebRequest.Get("https://www.bing.com"));
        var task3 = GetTextAsync(UnityWebRequest.Get("https://www.yahoo.com"));

        // Wait for all tasks to complete concurrently and get results using tuple syntax
        var (google, bing, yahoo) = await UniTask.WhenAll(task1, task2, task3);

        // Log the results
        Debug.Log(google);
        Debug.Log(bing);
        Debug.Log(yahoo);
    }
}
