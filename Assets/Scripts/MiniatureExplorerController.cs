using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniatureExplorerController : MonoBehaviour {

    public GameObject miniaturePrefab;

    float distance = 4.5f;
    float vAngleIncrement = 16.0f;
    float hAngleIncrement = 17.0f;
    float vAngleInitial = -8.0f;
    int rows = 3;
    int columns = 2;
    string baseUrl = "https://nightly.nuxeo.com/nuxeo/";
    string apiPathRoot = "api/v1/path/";
    GameObject current;
    List<GameObject> children;

	void Awake() {
        current = Instantiate(miniaturePrefab);
        current.transform.parent = transform;
        current.transform.localPosition = new Vector3(0.0f, 0.0f, distance);
        current.transform.RotateAround(Vector3.zero, Vector3.left, rows * vAngleIncrement + vAngleInitial);
        current.GetComponent<MiniatureController>().enable();
        current.GetComponent<MiniatureController>().setText("ROOT");
        current.GetComponent<MiniatureController>().setImage("domain");
        current.GetComponent<MiniatureController>().setExplorer(this);
        current.GetComponent<MiniatureController>().setUrl(baseUrl + apiPathRoot);
        children = new List<GameObject>();
        GameObject obj;
        for (int r = rows - 1; r >= 0; r--) {
            for (int c = -columns; c <= columns; c++) {
                obj = Instantiate(miniaturePrefab);
                obj.transform.parent = transform;
                obj.transform.localPosition = new Vector3(0.0f, 0.0f, distance);
                obj.transform.RotateAround(Vector3.zero, Vector3.left, r * vAngleIncrement + vAngleInitial);
                obj.transform.RotateAround(Vector3.zero, Vector3.up, c * hAngleIncrement);
                obj.GetComponent<MiniatureController>().disable();
                obj.GetComponent<MiniatureController>().setText("");
                obj.GetComponent<MiniatureController>().setImage(null);
                obj.GetComponent<MiniatureController>().setExplorer(this);
                children.Add(obj);
            }
        }
	}

    IEnumerator WaitForRequest(WWW www) {
        yield return www;
        if (www.error == null) {
            Debug.Log(www.text);
        } else {
            Debug.Log(www.error);
        }
    }

    void makeRequest(string url) {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Basic " + System.Convert.ToBase64String(
            System.Text.Encoding.ASCII.GetBytes("Administrator:Administrator")));
        headers.Add("X-NXproperties", "*");
        StartCoroutine(WaitForRequest(new WWW(url, null, headers)));
    }

    public void triggerMiniature(MiniatureController miniature) {
        makeRequest(miniature.getUrl());
        makeRequest(miniature.getUrl() + "@children");
    }
	
}
