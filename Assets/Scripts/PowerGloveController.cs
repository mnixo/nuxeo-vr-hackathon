using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerGloveController : MonoBehaviour {

    public GameObject imagePreviewPrefab;
    public MiniatureExplorerController explorerController;

    NuxeoEntity entity;
    float val = 0.0f;

	void Awake() {
		
	}

    void FixedUpdate() {
        transform.localPosition = new Vector3(
            0.1f + Mathf.Sin(val) * 0.02f, 
            transform.localPosition.y,
            transform.localPosition.z
        );
        transform.localEulerAngles = new Vector3(0.0f, Mathf.Sin(val) * 5, Mathf.Sin(val) * 5);
        val += 0.02f;
    }

    public void setEntity(NuxeoEntity entity) {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        this.entity = entity;
        GameObject obj = Instantiate(imagePreviewPrefab);
        obj.transform.parent = transform;
        obj.transform.localPosition = new Vector3(-0.2f, 0.0f, 0.0f);
        obj.transform.localScale = Vector3.one;
        obj.transform.localRotation = Quaternion.Euler(10.0f, 30.0f, -10.0f);
        obj.transform.Find("NuxeoImagePreviewRaw").gameObject.GetComponent<RawImage>().texture = entity.image;
    }
	
}
