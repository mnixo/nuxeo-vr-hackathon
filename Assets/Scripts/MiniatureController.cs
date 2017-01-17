using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniatureController : MonoBehaviour {

    Text text;
    Image image;
    MiniatureExplorerController explorer;
    GazeTriggerController trigger;
    string url;

	void Awake() {
        text = transform.Find("MiniatureHeader/MiniatureTitleCanvas/MiniatureTitleText").GetComponent<Text>();
        image = transform.Find("MiniatureIconCanvas/MiniatureIconImage").GetComponent<Image>();
        trigger = transform.Find("MiniatureGazeTrigger").GetComponent<GazeTriggerController>();
        trigger.setCallbacks(onTriggerEnter, onTriggerComplete, onTriggerExit);
        setText("(TEXT)");
        setImage(null);
	}

    public void setText(string val) {
        text.text = val;
    }

    public void setImage(string val) {
        image.enabled = val != null;
        image.sprite = val != null ? Resources.Load<Sprite>("Icons/" + val) : null;
    }

    public void setExplorer(MiniatureExplorerController explorer) {
        this.explorer = explorer;
    }

    public string getUrl() {
        return url;
    }

    public void setUrl(string val) {
        url = val;
    }

    void onTriggerEnter() {
        
    }

    void onTriggerComplete() {
        explorer.triggerMiniature(this);
    }

    void onTriggerExit() {
        
    }

}
