using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchController : MonoBehaviour {

    public GameObject miniaturePrefab;

    GazeTriggerController searchTrigger, trigger1, trigger2, trigger3;
    GameObject example1, example2, example3;
    NuxeoEntity entity1, entity2, entity3;

	void Awake() {
        searchTrigger = transform.Find("Trigger").GetComponent<GazeTriggerController>();
        searchTrigger.setCallbacks(doNothing, executeSearch, doNothing);
        trigger1 = transform.Find("Example1/GazeTrigger").GetComponent<GazeTriggerController>();
        trigger2 = transform.Find("Example2/GazeTrigger").GetComponent<GazeTriggerController>();
        trigger3 = transform.Find("Example3/GazeTrigger").GetComponent<GazeTriggerController>();
        trigger1.setCallbacks(doNothing, clearExample1, doNothing);
        trigger2.setCallbacks(doNothing, clearExample2, doNothing);
        trigger3.setCallbacks(doNothing, clearExample3, doNothing);
        example1 = transform.Find("Example1/Example").gameObject;
        example2 = transform.Find("Example2/Example").gameObject;
        example3 = transform.Find("Example3/Example").gameObject;
	}

    void doNothing() {

    }

    void clearExample1() {
        foreach (Transform child in example1.transform) {
            Destroy(child.gameObject);
        }
        entity1 = null;
    }

    void clearExample2() {
        foreach (Transform child in example2.transform) {
            Destroy(child.gameObject);
        }
        entity2 = null;
    }

    void clearExample3() {
        foreach (Transform child in example3.transform) {
            Destroy(child.gameObject);
        }
        entity3 = null;
    }

    void setMiniatureFromEntity(NuxeoEntity entity, GameObject example) {
        GameObject miniature = Instantiate(miniaturePrefab);
        miniature.transform.parent = example.transform;
        miniature.transform.localPosition = Vector3.zero;
        miniature.transform.localEulerAngles = Vector3.up * -90;
        miniature.transform.localScale = Vector3.one * 0.5f;
        miniature.GetComponent<MiniatureController>().setEntity(entity);
        miniature.GetComponent<MiniatureController>().setExplorer(null);
    }

    public void addEntity(NuxeoEntity entity) {
        if (entity1 == null) {
            entity1 = entity;
            setMiniatureFromEntity(entity, example1);
        } else if (entity2 == null) {
            entity2 = entity;
            setMiniatureFromEntity(entity, example2);
        } else if (entity3 == null) {
            entity3 = entity;
            setMiniatureFromEntity(entity, example3);
        }
    }

    void executeSearch() {

    }
	
}
