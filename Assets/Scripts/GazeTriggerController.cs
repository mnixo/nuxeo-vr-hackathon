using UnityEngine;
using UnityEngine.UI;

public class GazeTriggerController : MonoBehaviour {

    public float gazeTime = 1.0f;
    public Color baseColor = Color.white;
    public Color loadingColor = Color.blue;
    public Color completeColor = Color.green;

    Image imageComponent;
    bool isLoading;
    float startTime;
    public delegate void OnEnterDelegate();
    public delegate void OnTriggerDelegate();
    public delegate void OnExitDelegate();
    OnEnterDelegate onEnterDelegate;
    OnTriggerDelegate onTriggerDelegate;
    OnExitDelegate onExitDelegate;

    void Awake() {
        GameObject baseObj = transform.Find("GazeTriggerBase").gameObject;
        GameObject canvasObj = transform.Find("GazeTriggerLoadingCanvas").gameObject;
        GameObject imageObj = canvasObj.transform.Find("GazeTriggerLoadingImage").gameObject;
        baseObj.GetComponent<MeshRenderer>().material.color = baseColor;
        imageComponent = imageObj.GetComponent<Image>();
        isLoading = false;
    }

    void FixedUpdate() {
        if (isLoading && imageComponent.fillAmount < 1) {
            imageComponent.fillAmount = (Time.time - startTime) / gazeTime;
            if (imageComponent.fillAmount >= 1) {
                imageComponent.color = completeColor;
                onTriggerDelegate();
            }
        }
    }

    public void OnEnter() {
        isLoading = true;
        imageComponent.color = loadingColor;
        startTime = Time.time;
        onEnterDelegate();
    }

    public void OnExit() {
        isLoading = false;
        imageComponent.fillAmount = 0.0f;
        onExitDelegate();
    }

    public void setCallbacks(OnEnterDelegate enter, OnTriggerDelegate trigger, OnExitDelegate exit) {
        onEnterDelegate = enter;
        onTriggerDelegate = trigger;
        onExitDelegate = exit;
    }

}
