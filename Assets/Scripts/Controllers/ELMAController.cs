using System.Collections.Generic;
using System.Net.Cache;
using UnityEngine;
using ELMA.SDK.Models;
using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;

public class ELMAController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Вызывать через StartCoroutine(CreateUser(user), callback);
    public IEnumerator CreateUser(UserModel user, Action<bool> success) 
    {
        var url = "https://radvlfsvyzgxs.t-elma365.ru/api/extensions/7b2dc10b-1758-4b01-a74b-2b1a7622ccd7/script/createPlayer";
        string jsonData = JsonConvert.SerializeObject(user);

        var req = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.responseCode != 204)
        {
            success(false);
        }
        else
        {
            success(true);
        }
    }

    public IEnumerator UserExists(string nickname, Action<bool> result) 
    {
        var url = "https://radvlfsvyzgxs.t-elma365.ru/api/extensions/7b2dc10b-1758-4b01-a74b-2b1a7622ccd7/script/findUserByNickname";
        var payload = new NicknameSearchRequest();
        payload.Nickname = nickname;

        string jsonData = JsonConvert.SerializeObject(payload);

        var req = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.responseCode != 200)
        {
            result(false);
        }
        else
        {
            var resPayload = JsonConvert.DeserializeObject<NicknameSearchResponse>(req.downloadHandler.text);
            result(resPayload.IsFound);
        }
    }

    public IEnumerator GetUser(string nickname, Action<UserModel> result) 
    {
        var url = "https://radvlfsvyzgxs.t-elma365.ru/api/extensions/7b2dc10b-1758-4b01-a74b-2b1a7622ccd7/script/getDataPlayer";
        var payload = new NicknameSearchRequest();
        payload.Nickname = nickname;

        string jsonData = JsonConvert.SerializeObject(payload);
        Debug.Log(jsonData);

        var req = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        Debug.Log(req.responseCode);

        if (req.responseCode != 200)
        {
            result(new UserModel());
        }
        else
        {
            Debug.Log(req.downloadHandler.text);
            var resPayload = JsonConvert.DeserializeObject<UserModel>(req.downloadHandler.text);
            result(resPayload);
        }
    }

        public IEnumerator UpdateUser(UserModel user, Action<bool> success) 
    {
        var url = "https://radvlfsvyzgxs.t-elma365.ru/api/extensions/7b2dc10b-1758-4b01-a74b-2b1a7622ccd7/script/editDataPlayer";
        string jsonData = JsonConvert.SerializeObject(user);

        var req = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.responseCode != 204)
        {
            success(false);
        }
        else
        {
            success(true);
        }
    }
}
