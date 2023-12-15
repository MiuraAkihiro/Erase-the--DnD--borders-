using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TextureLoader : MonoBehaviour
{
    public string imageUrl;
    public Renderer targetRenderer;

    void Start()
    {
        StartCoroutine(LoadTexture());
    }

    IEnumerator LoadTexture()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(www);
            ApplyTexture(texture);
        }
        else
        {
            Debug.Log("Failed to load image: " + www.error);
        }
    }

    void ApplyTexture(Texture2D texture)
    {
        if (targetRenderer != null && texture != null)
        {
            targetRenderer.material.mainTexture = texture;
        }
    }


}
