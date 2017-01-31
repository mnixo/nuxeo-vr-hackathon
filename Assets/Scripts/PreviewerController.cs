using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewerController : MonoBehaviour {

    public GameObject imagePreviewPrefab;

    NuxeoEntity entity;
    GameObject obj;
    float delta;

	void Awake() {
		
	}

    void FixedUpdate() {
        if (entity == null) {
            return;
        }
        if (entity.isPicture()) {
            obj.transform.localPosition = Vector3.up * (2 + (Mathf.Sin(delta * 5) / 20));
            obj.transform.localEulerAngles = new Vector3(0.0f, Mathf.Sin(delta * 2.5f) * 10, 0.0f);
        } else if (entity.is3d()) {
            obj.transform.localPosition = Vector3.up * (2 + (Mathf.Sin(delta * 5) / 20));
            obj.transform.localEulerAngles = new Vector3(0.0f, delta * 100, 0.0f);
        }
        delta += 0.01f;
    }

    public void setEntity(NuxeoEntity entity) {
        this.entity = entity;
        delta = 0.0f;
        if (obj != null) {
            Destroy(obj);
        }
        obj = new GameObject();
        obj.transform.parent = transform;
        obj.transform.localPosition = Vector3.up * 2;
        obj.transform.localEulerAngles = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        if (entity.isPicture()) {
            GameObject imagePreview = Instantiate(imagePreviewPrefab);
            imagePreview.transform.Find("NuxeoImagePreviewRaw").gameObject.GetComponent<RawImage>().texture = entity.image;
            imagePreview.transform.parent = obj.transform;
            imagePreview.transform.localPosition = Vector3.zero;
            imagePreview.transform.localEulerAngles = Vector3.up * 90;
            imagePreview.transform.localScale = Vector3.one * 3;
        } else if (entity.is3d()) {
            Vector3 meshSize = entity.mesh.bounds.size;
            float maxSize = Mathf.Max(Mathf.Max(meshSize.x, meshSize.y), meshSize.z);
            GameObject meshPreview = new GameObject();
            meshPreview.transform.parent = obj.transform;
            meshPreview.transform.localPosition = Vector3.zero - entity.mesh.bounds.center;
            meshPreview.transform.localEulerAngles = Vector3.zero;
            meshPreview.AddComponent<MeshFilter>();
            meshPreview.AddComponent<MeshRenderer>();
            meshPreview.GetComponent<MeshFilter>().mesh = entity.mesh;
            meshPreview.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Standard"));
            obj.transform.localScale = Vector3.one * 3 * (1 / maxSize);
        }
    }

}
