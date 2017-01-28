using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerGloveController : MonoBehaviour {

    public GameObject imagePreviewPrefab;
    public MiniatureExplorerController explorerController;

    NuxeoEntity entity;
    float delta = 0.0f;

	void Awake() {
		
	}

    void FixedUpdate() {
        if (entity == null) {
            return;
        }
        foreach (Transform child in transform) {
            if (entity.isPicture()) {
                child.localPosition = new Vector3(-0.1f, 0.1f + Mathf.Sin(delta * 4) * 0.05f, 0.0f);
                child.localRotation = Quaternion.Euler(10.0f - Mathf.Sin(delta * 4) * 5, 40.0f, 0.0f);
            } else if (entity.is3d()) {
                child.localPosition = new Vector3(0.0f, 0.0f + Mathf.Sin(delta * 4) * 0.05f, 0.5f);
                child.localRotation = Quaternion.Euler(0.0f, delta * 80, 0.0f);
            }
        }
        delta += 0.01f;
    }

    public void setEntity(NuxeoEntity entity) {
        delta = 0.0f;
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        this.entity = entity;
        if (entity.isPicture()) {
            GameObject o = Instantiate(imagePreviewPrefab);
            o.transform.parent = transform;
            o.transform.localPosition = new Vector3(-0.1f, 0.1f, 0.0f);
            o.transform.localRotation = Quaternion.Euler(10.0f, 40.0f, 0.0f);
            o.transform.localScale = Vector3.one;
            o.transform.Find("NuxeoImagePreviewRaw").gameObject.GetComponent<RawImage>().texture = entity.image;
        } else if (entity.is3d()) {
            GameObject o = entity.model;
            o.transform.parent = transform;
            o.transform.localPosition = new Vector3(0.0f, 0.0f, 0.5f);
            o.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            o.transform.localScale *= 0.2f;
        }
    }
	
}
