using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniatureController : MonoBehaviour {

    Text text;
    Image image;
    GazeTriggerController trigger;

	void Start() {
        text = transform.Find("MiniatureHeader/MiniatureTitleCanvas/MiniatureTitleText").GetComponent<Text>();
        image = transform.Find("MiniatureIconCanvas/MiniatureIconImage").GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>("Icons/domain");
        trigger = transform.Find("MiniatureGazeTrigger").GetComponent<GazeTriggerController>();
        trigger.setCallbacks(onTriggerEnter, onTriggerComplete, onTriggerExit);
	}

    void onTriggerEnter() {
        Debug.Log("onTriggerEnter");
    }

    void onTriggerComplete() {
        Debug.Log("onTriggerComplete");
    }

    void onTriggerExit() {
        Debug.Log("onTriggerExit");
    }

}
