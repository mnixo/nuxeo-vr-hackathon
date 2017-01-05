using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuxeoTest : MonoBehaviour {

    IEnumerator WaitForRequest(WWW www) {
        //http://answers.unity3d.com/questions/207920/callback-from-a-coroutine.html
        yield return www;
        if (www.error == null) {
            Debug.Log(www.text);
        } else {
            Debug.Log(www.error);
        }
    }

    void getRoot() {
        string url = "https://nightly.nuxeo.com/nuxeo/api/v1/path/";
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Basic " + System.Convert.ToBase64String(
            System.Text.Encoding.ASCII.GetBytes("Administrator:Administrator")));
        StartCoroutine(WaitForRequest(new WWW(url, null, headers)));
    }

    void Start() {
        getRoot();
    }

}
