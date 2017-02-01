using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniatureController : MonoBehaviour {

    Text text;
    Image image;
    RectTransform imageTransform;
    MiniatureExplorerController explorer;
    GazeTriggerController trigger;
    float iconAngleValue = 0.0f;

    NuxeoEntity entity;

	void Awake() {
        text = transform.Find("MiniatureHeader/MiniatureTitleCanvas/MiniatureTitleText").GetComponent<Text>();
        image = transform.Find("MiniatureIconCanvas/MiniatureIconImage").GetComponent<Image>();
        imageTransform = transform.Find("MiniatureIconCanvas/MiniatureIconImage").GetComponent<RectTransform>();
        trigger = transform.Find("MiniatureGazeTrigger").GetComponent<GazeTriggerController>();
        trigger.setCallbacks(onTriggerEnter, onTriggerComplete, onTriggerExit);
	}

    void FixedUpdate() {
        imageTransform.localEulerAngles = new Vector3(0.0f, 0.0f, Mathf.Sin(iconAngleValue += 0.05f) * 20);
    }

    public NuxeoEntity getEntity() {
        return entity;
    }

    public void setEntity(NuxeoEntity entity) {
        this.entity = entity;
        trigger.enabled = entity != null;
        image.enabled = entity != null;
        image.sprite = entity != null ? getSpriteFromEntityType(entity.type) : null;
        if (entity != null) {
            text.text = entity.type != "Root" ? entity.title : entity.type;
        } else {
            text.text = null;
        }
    }

    private Sprite getSpriteFromEntityType(string type) {
        switch (type) {
            case "Root":
            case "Domain":
                return Resources.Load<Sprite>("Icons/domain");
            case "SectionRoot":
            case "TemplateRoot":
            case "WorkspaceRoot":
            case "Workspace":
                return Resources.Load<Sprite>("Icons/workspace");
            case "Folder":
                return Resources.Load<Sprite>("Icons/folder");
            case "Note":
                return Resources.Load<Sprite>("Icons/note");
            case "Picture":
                return Resources.Load<Sprite>("Icons/picture");
            case "Video":
                return Resources.Load<Sprite>("Icons/Video");
            case "3D":
                return Resources.Load<Sprite>("Icons/3d");    
            default:
                return Resources.Load<Sprite>("Icons/file");

        }
    }

    public void setExplorer(MiniatureExplorerController explorer) {
        this.explorer = explorer;
    }

    void onTriggerEnter() {
        
    }

    void onTriggerComplete() {
        if (explorer != null) {
            explorer.triggerMiniature(this);
        }
    }

    void onTriggerExit() {
        
    }

}
