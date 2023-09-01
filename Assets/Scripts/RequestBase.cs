using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Networking;

public class RequestBase : MonoBehaviour
{
    protected string url = "https://librovaultapi.fickert.space/v1/";

    public void SendGetRequest<T>(string complemento, string token, Action<T> onCompleted = null)
    {
        StartCoroutine(GetRequest<T>(complemento, token, onCompleted));
    }

    private IEnumerator GetRequest<T>(string complemento, string token, Action<T> onCompleted)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url + complemento))
        {
            www.SetRequestHeader("Authorization", "Bearer " + token);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erro na requisição: " + www.error);
                Debug.Log("Detalhes do erro: " + www.downloadHandler.text);
                onCompleted?.Invoke(default(T));
            }
            else
            {
                Debug.Log("Resposta da API: " + www.downloadHandler.text);
                T responseData = JsonConvert.DeserializeObject<T>(www.downloadHandler.text);
                onCompleted?.Invoke(responseData);
            }
        }
    }

    public void SendPostRequest<T>(string urlComplement, string jsonForm, Action<T> onCompleted = null)
    {
        StartCoroutine(PostRequest(urlComplement, jsonForm, onCompleted));
    }

    private IEnumerator PostRequest<T>(string complemento, string json, Action<T> onCompleted)
    {
        UnityWebRequest www = new(url + complemento, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
            Debug.Log("API Error Detail: " + www.downloadHandler.text);
            onCompleted?.Invoke(default(T));
        }
        else
        {
            Debug.Log("API Response: " + www.downloadHandler.text);
            T responseData = JsonConvert.DeserializeObject<T>(www.downloadHandler.text);
            onCompleted?.Invoke(responseData);
        }
    }
}