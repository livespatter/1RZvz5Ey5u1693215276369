using UnityEngine;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;
using EaAds;
using UnityEngine.Networking;
using System.IO;
using System.Collections;

public class GameUtils {

    public static async Task<Settings> GetJsonDataFromServer(string url, Action<DataProgress> progresss)
    {
        var dataProgress = new DataProgress();
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            // Send request and wait
            www.SendWebRequest();

            // While the request is still processing...
            while (!www.isDone)
            {
                var progresssPersen = string.Format("{0:0}%", www.downloadProgress * 100);

                dataProgress.progressPersen = progresssPersen;
                dataProgress.progressFloat = www.downloadProgress;

                progresss.Invoke(dataProgress);
                await Task.Delay(100);  // Tunggu sedikit sebelum memeriksa lagi.
            }

            // If there are network errors
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                return null;
            }
            else
            {
                // Parse JSON response
                var result = JsonConvert.DeserializeObject<Settings>(www.downloadHandler.text);
                dataProgress.progressPersen = "100%";
                dataProgress.progressFloat = www.downloadProgress;

                progresss.Invoke(dataProgress);
                await Task.Delay(300);  // Tunggu sedikit sebelum memeriksa lagi.
                return result;
            }
        }
    }
    public static Settings GetJsonDataFromLocal()
    {
        TextAsset jsonTextAsset = Resources.Load<TextAsset>("JsonData");
        // Parse JSON response
        var root = JsonConvert.DeserializeObject<RootEASettingModel>(jsonTextAsset.text);
        return root.Settings;
    }

   public static System.Collections.IEnumerator LoadSpriteCoroutine(string url, Action<Sprite> callback)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                callback(sprite);
            }
            else
            {
                Debug.LogError("Failed to load texture from URL");
                callback(null);
            }
        }
    }

     public static async Task<GameObject> LoadMyAssets(string pathAsset, string assetName, Action<DataProgress> progresss) {
       
        /*
        var path = Path.Combine("file://" + Application.streamingAssetsPath, pathAsset);
        var bundleLoadRequest = UnityWebRequestAssetBundle.GetAssetBundle(path);
         // Asset Bundle Load Progress
        _ = CheckProgress(bundleLoadRequest, "Asset Bundle Loading Progress: ", progresss);

        await AwaitAsyncOperation(bundleLoadRequest.SendWebRequest());

        if (bundleLoadRequest.result != UnityWebRequest.Result.Success) {
            Debug.Log($"Failed to load AssetBundle: {bundleLoadRequest.error}");
            throw new Exception($"Failed to load AssetBundle: {bundleLoadRequest.error}");
        }

        var myLoadedAssetBundle = DownloadHandlerAssetBundle.GetContent(bundleLoadRequest);

        var assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>(assetName);

        // Asset Load Progress
        _ = CheckProgress(assetLoadRequest, "Asset Loading Progress: ", progresss);

        await AwaitAsyncOperation(assetLoadRequest);

        return assetLoadRequest.asset as GameObject;
        */
        
    
        var path = Path.Combine(Application.streamingAssetsPath, pathAsset);
        AssetBundle myLoadedAssetBundle;
        // if (!File.Exists(path)) {
        //     Debug.Log("File doesn't exist. Path: " + path);
        //     throw new FileNotFoundException("File doesn't exist. Path: " + path);
        // }

        // var bundleLoadRequest = AssetBundle.LoadFromFileAsync(path);
         var bundleLoadRequest = BetterStreamingAssets.LoadAssetBundleAsync(pathAsset);

        // Asset Bundle Load Progress
        await CheckProgress(bundleLoadRequest, "Asset Bundle Loading Progress: ", progresss);

        await AwaitAsyncOperation(bundleLoadRequest);

        myLoadedAssetBundle = bundleLoadRequest.assetBundle;

        if (myLoadedAssetBundle == null) {
            Debug.Log("Failed to load AssetBundle!");
            throw new Exception("Failed to load AssetBundle!");
        }

        var assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>(assetName);

        // Asset Load Progress
        await CheckProgress(assetLoadRequest, "Asset Loading Progress: ", progresss);

        await AwaitAsyncOperation(assetLoadRequest);

        return assetLoadRequest.asset as GameObject;
        

    }

    static async Task CheckProgress(UnityWebRequest request, string message, Action<DataProgress> progresss) {
        var dataProgress = new DataProgress();
        while (!request.isDone) {
            Debug.Log(message + request.downloadProgress);
              var progresssPersen = string.Format("{0:0}%", request.downloadProgress * 100);

            dataProgress.progressPersen = progresssPersen;
            dataProgress.progressFloat = request.downloadProgress;

            progresss.Invoke(dataProgress);
            await Task.Delay(100); // check every 100 milliseconds
        }
    }

     static async Task CheckProgress(AsyncOperation operation, string message, Action<DataProgress> progresss) {
        var dataProgress = new DataProgress();
        while (!operation.isDone) {
            Debug.Log(message + operation.progress);
             var progresssPersen = string.Format("{0:0}%", operation.progress * 100);

            dataProgress.progressPersen = progresssPersen;
            dataProgress.progressFloat = operation.progress;

            progresss.Invoke(dataProgress);
            await Task.Delay(100); // check every 100 milliseconds
        }
    }

    static Task AwaitAsyncOperation(AsyncOperation asyncOp) {
        var tcs = new TaskCompletionSource<bool>();

        asyncOp.completed += op => {
            tcs.SetResult(true);
        };

        return tcs.Task;
    }

    public static Sprite LoadImageFromResources(string pathImage)
    {
        // Resources.Load berfungsi relatif terhadap direktori Resources,
        // jadi Anda hanya perlu menyertakan bagian "Images/imag001" dari jalur
        Texture2D texture = Resources.Load<Texture2D>(pathImage);

        if(texture == null)
        {
            Debug.LogError("Image not found at specified path.");
            return null;
        }

        Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        return sprite;
    }
}