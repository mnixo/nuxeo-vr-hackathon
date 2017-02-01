using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MiniatureExplorerController : MonoBehaviour {

    public GameObject miniaturePrefab;
    public PreviewerController previewerController;
    public SearchController searchController;

    float distance = 4.5f;
    float vAngleIncrement = 16.0f;
    float hAngleIncrement = 17.0f;
    float vAngleInitial = -8.0f;
    int rows = 3;
    int columns = 2;
    string serverInfoUrl = "https://gist.githubusercontent.com/mnixo/a4ed773a84e6548ca19ce756fcda48bc/raw";
    string baseUrl = "https://nightly.nuxeo.com/nuxeo/";
    string username = "Administrator";
    string password = "Administrator";
    NuxeoEntity dummyRoot;
    GameObject current;
    List<GameObject> children;

    delegate void Callback(string json);
    delegate void UpdateGloveCallback(NuxeoEntity entity);

	void Awake() {
        Caching.CleanCache();
        makeRequest(serverInfoUrl, updateServerInfo);
        current = Instantiate(miniaturePrefab);
        current.transform.parent = transform;
        current.transform.localPosition = new Vector3(0.0f, 0.0f, distance);
        current.transform.RotateAround(Vector3.zero, Vector3.left, rows * vAngleIncrement + vAngleInitial);
        dummyRoot = new NuxeoEntity();
        dummyRoot.title = "START";
        dummyRoot.type = "Domain";
        dummyRoot.facets = new List<string> { "Folderish" };
        dummyRoot.entityUrl = baseUrl + "api/v1/path/";
        dummyRoot.childrenUrl = baseUrl + "api/v1/path/@children";
        current.GetComponent<MiniatureController>().setEntity(dummyRoot);
        current.GetComponent<MiniatureController>().setExplorer(this);
        children = new List<GameObject>();
        GameObject obj;
        for (int r = rows - 1; r >= 0; r--) {
            for (int c = -columns; c <= columns; c++) {
                obj = Instantiate(miniaturePrefab);
                obj.transform.parent = transform;
                obj.transform.localPosition = new Vector3(0.0f, 0.0f, distance);
                obj.transform.RotateAround(Vector3.zero, Vector3.left, r * vAngleIncrement + vAngleInitial);
                obj.transform.RotateAround(Vector3.zero, Vector3.up, c * hAngleIncrement);
                obj.GetComponent<MiniatureController>().setEntity(null);
                obj.GetComponent<MiniatureController>().setExplorer(this);
                children.Add(obj);
            }
        }
	}

    IEnumerator WaitForRequest(WWW www, Callback cb) {
        yield return www;
        if (www.error == null) {
            Debug.Log(www.text);
            cb(www.text);
        } else {
            Debug.Log(www.error);
        }
    }

    void makeRequest(string url, Callback cb) {
        StartCoroutine(WaitForRequest(new WWW(url, null, new Dictionary<string, string>()), cb));
    }

    void makeNuxeoApiRequest(string url, Callback cb) {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Basic " + System.Convert.ToBase64String(
            System.Text.Encoding.ASCII.GetBytes(username + ":" + password)));
        headers.Add("X-NXproperties", "*");
        StartCoroutine(WaitForRequest(new WWW(url, null, headers), cb));
    }

    IEnumerator downloadDocumentImage(NuxeoEntity entity, UpdateGloveCallback cb) {
        Texture2D tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Basic " + System.Convert.ToBase64String(
            System.Text.Encoding.ASCII.GetBytes(username + ":" + password)));
        headers.Add("X-NXproperties", "*");
        WWW www = new WWW(entity.fileDataUrl, null, headers);
        yield return www;
        www.LoadImageIntoTexture(tex);
        entity.image = tex;
        cb(entity);
    }

    IEnumerator downloadDocumentModel(NuxeoEntity entity, UpdateGloveCallback cb) {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Basic " + System.Convert.ToBase64String(
            System.Text.Encoding.ASCII.GetBytes(username + ":" + password)));
        headers.Add("X-NXproperties", "*");
        WWW www = new WWW(entity.fileDataUrl, null, headers);
        yield return www;

        objReaderCSharpV4 reader = new objReaderCSharpV4();
        reader.LoadFile(www.text);
        Mesh mesh = reader.getMesh();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        //GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //obj.GetComponent<MeshFilter>().mesh = mesh;
        //obj.GetComponent<Renderer>().material = new Material(Shader.Find("Standard"));

        entity.mesh = mesh;
        cb(entity);
    }

    void updateServerInfo(string json) {
        JSONObject obj = new JSONObject(json);
        baseUrl = obj.GetField("server").str;
        username = obj.GetField("username").str;
        password = obj.GetField("password").str;
        dummyRoot.entityUrl = baseUrl + "api/v1/path/";
        dummyRoot.childrenUrl = baseUrl + "api/v1/path/@children";
        Debug.Log("Server URL: " + baseUrl + "\n" + 
                  "- Username: " + username + "\n" + 
                  "- Password: " + password);
    }

    void updateCurrent(string json) {
        JSONObject obj = new JSONObject(json);
        NuxeoEntity entity = new NuxeoEntity(obj, baseUrl);
        if (entity.parentRef.Equals("/")) {
            entity.entityUrl = baseUrl + "api/v1/path" + entity.parentRef;
        } else {
            entity.entityUrl = baseUrl + "api/v1/id/" + entity.parentRef + "/";
        }
        entity.childrenUrl = entity.entityUrl + "@children";
        current.GetComponent<MiniatureController>().setEntity(entity);
    }

    void updateChildren(string json) {
        JSONObject wrapper = new JSONObject(json);
        List<JSONObject> entries = wrapper.GetField("entries").list;
        for (int i = 0; i < children.Count; i++) {
            NuxeoEntity entity = null;
            if (i < entries.Count) {
                entity = new NuxeoEntity(entries[i], baseUrl);
            }
            children[i].GetComponent<MiniatureController>().setEntity(entity);
        }
    }

    void updatePreviewer(NuxeoEntity entity) {
        previewerController.setEntity(entity);
    }

    public void triggerMiniature(MiniatureController miniature) {
        if (miniature.getEntity().isFolderish()) {
            makeNuxeoApiRequest(miniature.getEntity().entityUrl, updateCurrent);
            makeNuxeoApiRequest(miniature.getEntity().childrenUrl, updateChildren);
        } else {
            searchController.addEntity(miniature.getEntity());
            if (miniature.getEntity().isPicture()) {
                StartCoroutine(downloadDocumentImage(miniature.getEntity(), updatePreviewer));
            } else if (miniature.getEntity().is3d()) {
                StartCoroutine(downloadDocumentModel(miniature.getEntity(), updatePreviewer));
        }
        }
    }
	
}
