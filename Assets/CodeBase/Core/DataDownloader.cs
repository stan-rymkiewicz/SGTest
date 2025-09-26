using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace CodeBase.Core
{
    public class JsonDataDownloader<T>
    {
        private MonoBehaviour _coroutineRunner;

        public JsonDataDownloader(MonoBehaviour coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }
        
        public void Download(string url, Action<bool, T> callback)
        {
            _coroutineRunner.StartCoroutine(DownloadRoutine(url, callback));
        }

        private IEnumerator DownloadRoutine(string url, Action<bool, T> callback)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                yield return request.SendWebRequest();
                
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Error downloading JSON: {request.error}");
                    callback(false, default);
                }
                else
                {
                    try
                    {
                        string json = request.downloadHandler.text;
                        T data = JsonUtility.FromJson<T>(json);
                        callback(true, data);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"JSON parse error: {e.Message}");
                        callback(false, default);
                    }
                }
            }
        }
    }
    
    public class SpritesDownloader
    {
        private MonoBehaviour _coroutineRunner;

        public SpritesDownloader(MonoBehaviour coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

       
        public void Download(string[] urls, Action<bool, List<KeyValuePair<string, Sprite>>> callback)
        {
            _coroutineRunner.StartCoroutine(DownloadRoutine(urls, callback));
        }

        private IEnumerator DownloadRoutine(string[] urls, Action<bool, List<KeyValuePair<string, Sprite>>> callback)
        {
            var requests = new List<UnityWebRequest>();
            var operations = new List<UnityWebRequestAsyncOperation>();

            // Start all downloads at once
            foreach (string url in urls)
            {
                var request = UnityWebRequestTexture.GetTexture(url);
                requests.Add(request);
                operations.Add(request.SendWebRequest());
            }

            // Wait until all are finished
            foreach (var op in operations)
            {
                yield return op; // doesn't block others, just waits per operation
            }

            // Collect results
            List < KeyValuePair<string, Sprite> > results = new List < KeyValuePair<string, Sprite> >();

            for (int i = 0; i < requests.Count; i++)
            {
                if (requests[i].result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Error downloading Texture from {urls[i]}: {requests[i].error}");
                }
                else
                {
                    try
                    {
                        Texture2D texture = DownloadHandlerTexture.GetContent(requests[i]);
                        if (texture)
                        {
                            Sprite sprite = Sprite.Create(
                                texture,
                                new Rect(0, 0, texture.width, texture.height),
                                new Vector2(0.5f, 0.5f)
                            );
                        
                            results.Add(new KeyValuePair<string, Sprite>(requests[i].url, sprite));
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Texture request parse error: {e.Message}");
                    }
                }
                
            }

            callback(true, results);
        }
    }
}