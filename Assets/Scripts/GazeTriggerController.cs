using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazeTriggerController : MonoBehaviour {
    
    Image image;
    bool isLoading;
    float startTime;

    public float gazeTime = 1.0f;
    public Color baseColor = Color.black;
    public Color loadingColor = Color.blue;
    public Color completeColor = Color.green;

    void Start() {
        GameObject baseGameObject = transform.Find("Base").gameObject;
        baseGameObject.GetComponent<MeshRenderer>().material.color = baseColor;
        GameObject imageObject = transform.Find("Canvas/Image").gameObject;
        image = imageObject.GetComponent<Image>();
        isLoading = false;
    }

    void FixedUpdate() {
        if (isLoading && image.fillAmount < 1) {
            image.fillAmount = (Time.time - startTime) / gazeTime;
            if (image.fillAmount >= 1) {
                image.color = completeColor;
            }
        }
    }

    public void OnEnter() {
        isLoading = true;
        image.color = loadingColor;
        startTime = Time.time;
    }

    public void OnExit() {
        isLoading = false;
        image.fillAmount = 0.0f;
    }

}
