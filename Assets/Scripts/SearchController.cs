using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchController : MonoBehaviour {

    public GameObject miniaturePrefab;
    public MiniatureExplorerController explorer;

    GazeTriggerController searchTrigger, trigger1, trigger2, trigger3;
    GameObject example1, example2, example3;
    NuxeoEntity entity1, entity2, entity3;
    List<MiniatureController> searchResults;

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
        searchResults = new List<MiniatureController>();

        float hDistance = 13.0f;
        for (float hAngle = hDistance * -2; hAngle <= hDistance * 2; hAngle += hDistance) {
            GameObject sr = Instantiate(miniaturePrefab);
            sr.transform.parent = transform;
            sr.transform.localPosition = new Vector3(-1.0f, 1.0f, 0.0f);
            sr.transform.localEulerAngles = Vector3.up * -90;
            sr.transform.localScale = Vector3.one * 0.5f;
            sr.transform.RotateAround(Vector3.zero, Vector3.back, 18.0f);
            sr.transform.RotateAround(Vector3.zero, Vector3.up, hAngle);
            sr.GetComponent<MiniatureController>().setEntity(null);
            searchResults.Add(sr.GetComponent<MiniatureController>());
        }
        hDistance = 12.0f;
        for (float hAngle = hDistance * -2; hAngle <= hDistance * 2; hAngle += hDistance) {
            GameObject sr = Instantiate(miniaturePrefab);
            sr.transform.parent = transform;
            sr.transform.localPosition = new Vector3(-1.0f, 1.0f, 0.0f);
            sr.transform.localEulerAngles = Vector3.up * -90;
            sr.transform.localScale = Vector3.one * 0.5f;
            sr.transform.RotateAround(Vector3.zero, Vector3.back, 5.0f);
            sr.transform.RotateAround(Vector3.zero, Vector3.up, hAngle);
            sr.GetComponent<MiniatureController>().setEntity(null);
            searchResults.Add(sr.GetComponent<MiniatureController>());
        }
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
        for (int i = 0; i < searchResults.Count; i++) {
            searchResults[i].setEntity(null);
        }
        if (entity1 == null && entity2 == null && entity3 == null) {
            return;
        }
        string url = explorer.getBaseUrl() + "api/v1/path/@search?pageSize=10&fullText=";
        bool first = true;
        string[] parts;
        if (entity1 != null) {
            if (entity1.title != null) {
                parts = entity1.title.Split(new string[] { ".", " " }, StringSplitOptions.None);
                foreach (string part in parts) {
                    if (part.Length < 2) {
                        continue;
                    }
                    if (first) {
                        first = false;
                        url += part;
                    } else {
                        url += "%20OR%20" + part;
                    }
                }
            }
            if (entity1.description != null) {
                parts = entity1.description.Split(new string[] { ".", " " }, StringSplitOptions.None);
                foreach (string part in parts) {
                    if (part.Length < 2) {
                        continue;
                    }
                    if (first) {
                        first = false;
                        url += part;
                    } else {
                        url += "%20OR%20" + part;
                    }
                }
            }
        }
        if (entity2 != null) {
            if (entity2.title != null) {
                parts = entity2.title.Split(new string[] { ".", " " }, StringSplitOptions.None);
                foreach (string part in parts) {
                    if (part.Length < 2) {
                        continue;
                    }
                    if (first) {
                        first = false;
                        url += part;
                    } else {
                        url += "%20OR%20" + part;
                    }
                }
            }
            if (entity2.description != null) {
                parts = entity2.description.Split(new string[] { ".", " " }, StringSplitOptions.None);
                foreach (string part in parts) {
                    if (part.Length < 2) {
                        continue;
                    }
                    if (first) {
                        first = false;
                        url += part;
                    } else {
                        url += "%20OR%20" + part;
                    }
                }
            }
        }
        if (entity3 != null) {
            if (entity3.title != null) {
                parts = entity3.title.Split(new string[] { ".", " " }, StringSplitOptions.None);
                foreach (string part in parts) {
                    if (part.Length < 2) {
                        continue;
                    }
                    if (first) {
                        first = false;
                        url += part;
                    } else {
                        url += "%20OR%20" + part;
                    }
                }
            }
            if (entity3.description != null) {
                parts = entity3.description.Split(new string[] { ".", " " }, StringSplitOptions.None);
                foreach (string part in parts) {
                    if (part.Length < 2) {
                        continue;
                    }
                    if (first) {
                        first = false;
                        url += part;
                    } else {
                        url += "%20OR%20" + part;
                    }
                }
            }
        }
        Debug.Log(url);
        explorer.makeNuxeoApiRequest(url, fillResults);
    }

    void fillResults(string json) {
        JSONObject wrapper = new JSONObject(json);
        List<JSONObject> entries = wrapper.GetField("entries").list;
        for (int i = 0; i < searchResults.Count; i++) {
            NuxeoEntity entity = null;
            if (i < entries.Count) {
                entity = new NuxeoEntity(entries[i], explorer.getBaseUrl());
            }
            searchResults[i].GetComponent<MiniatureController>().setEntity(entity);
            searchResults[i].GetComponent<MiniatureController>().setExplorer(null);
        }
    }

}
