using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour {

    GazeTriggerController activator;
    Text text;
    Image icon;
    RectTransform iconTransform;
    float iconRotation = 0.0f;

	void Start() {
        GameObject activatorObj = transform.Find("CardActivatorGazeTrigger").gameObject;
        GameObject textObj = transform.Find("CardTextCanvas/CardText").gameObject;
        GameObject iconObj = transform.Find("CardIconCanvas/CardIcon").gameObject;
        text = textObj.GetComponent<Text>();
        icon = iconObj.GetComponent<Image>();
        iconTransform = icon.GetComponent<RectTransform>();
        activator = activatorObj.GetComponent<GazeTriggerController>();
        activator.setCallbacks(onActivatorEnter, onActivatorComplete, onActivatorExit);
        icon.sprite = Resources.Load<Sprite>("Icons/domain");
	}

    void FixedUpdate() {
        iconRotation += 0.02f;
        iconTransform.Rotate(0.0f, Mathf.Cos(iconRotation) * 0.5f, 0.0f);
    }

    void onActivatorEnter() {
        Debug.Log("onActivatorEnter");
    }

    void onActivatorComplete() {
        Debug.Log("onActivatorComplete");
    }

    void onActivatorExit() {
        Debug.Log("onActivatorExit");
    }

}
