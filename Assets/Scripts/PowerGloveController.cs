using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerGloveController : MonoBehaviour {

    public GameObject imagePreviewPrefab;
    public MiniatureExplorerController explorerController;

    NuxeoEntity entity;

	void Awake() {
		
	}

    public void setEntity(NuxeoEntity entity) {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        this.entity = entity;
        StartCoroutine(test(entity.fileDataUrl));
    }

    IEnumerator test(string url) {
        Texture2D tex;
        tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Basic " + System.Convert.ToBase64String(
            System.Text.Encoding.ASCII.GetBytes("Administrator" + ":" + "Nuxeo2015")));
        headers.Add("X-NXproperties", "*");
        WWW www = new WWW(url, null, headers);
        yield return www;
        www.LoadImageIntoTexture(tex);
        GameObject obj = Instantiate(imagePreviewPrefab);
        obj.transform.parent = transform;
        obj.transform.localPosition = new Vector3(-0.2f, 0.0f, 0.0f);
        obj.transform.localScale = Vector3.one;
        obj.transform.localRotation = Quaternion.Euler(10.0f, 30.0f, -10.0f);
        obj.transform.Find("NuxeoImagePreviewRaw").gameObject.GetComponent<RawImage>().texture = tex;
    }
	
}
