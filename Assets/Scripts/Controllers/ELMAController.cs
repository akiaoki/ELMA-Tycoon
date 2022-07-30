using UnityEngine;
using ELMA.SDK.Models;
using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json;

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

    // Вызывать через StartCoroutine(CreateUser(user));
    IEnumerator CreateUser(UserModel user) {
        var url = "https://radvlfsvyzgxs.t-elma365.ru/api/extensions/7b2dc10b-1758-4b01-a74b-2b1a7622ccd7/script/createPlayer";
        string jsonData = JsonConvert.SerializeObject(user);

        var req = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();
    }
}
