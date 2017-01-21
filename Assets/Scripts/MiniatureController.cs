using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniatureController : MonoBehaviour {

    Text text;
    Image image;
    MiniatureExplorerController explorer;
    GazeTriggerController trigger;

    NuxeoEntity entity;

	void Awake() {
        text = transform.Find("MiniatureHeader/MiniatureTitleCanvas/MiniatureTitleText").GetComponent<Text>();
        image = transform.Find("MiniatureIconCanvas/MiniatureIconImage").GetComponent<Image>();
        trigger = transform.Find("MiniatureGazeTrigger").GetComponent<GazeTriggerController>();
        trigger.setCallbacks(onTriggerEnter, onTriggerComplete, onTriggerExit);
	}

    public NuxeoEntity getEntity() {
        return entity;
    }

    public void setEntity(NuxeoEntity entity) {
        this.entity = entity;
        trigger.enabled = entity != null;
        image.enabled = entity != null;
        image.sprite = entity != null ? Resources.Load<Sprite>("Icons/domain") : null;
        if (entity != null) {
            text.text = entity.type != "Root" ? entity.title : entity.type;
        } else {
            text.text = null;
        }
    }

    public void setExplorer(MiniatureExplorerController explorer) {
        this.explorer = explorer;
    }

    void onTriggerEnter() {
        
    }

    void onTriggerComplete() {
        explorer.triggerMiniature(this);
    }

    void onTriggerExit() {
        
    }

}
