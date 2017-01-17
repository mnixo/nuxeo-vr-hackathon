using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniatureExplorerController : MonoBehaviour {

    public GameObject miniaturePrefab;

	void Start() {
        float distance = 4.5f;
        float vAngleIncrement = 18 * Mathf.Deg2Rad;
        float hAngleIncrement = 20 * Mathf.Deg2Rad;
        //float vAngle;
        //float hAngle;
        GameObject obj;

        //hAngle = hAngleIncrement * 0;

        for (float hAngle = -hAngleIncrement * 2; hAngle < hAngleIncrement * 3; hAngle += hAngleIncrement) {
            for (float vAngle = 0.0f; vAngle < vAngleIncrement * 3; vAngle += vAngleIncrement) {
                obj = Instantiate(miniaturePrefab);
                obj.transform.parent = transform;
                obj.transform.localPosition = new Vector3(0.0f, Mathf.Sin(vAngle) * distance, Mathf.Cos(vAngle) * distance);
                obj.transform.eulerAngles = new Vector3(-vAngle * Mathf.Rad2Deg, 0.0f, 0.0f);
                obj.transform.RotateAround(Vector3.zero, Vector3.up, hAngle * Mathf.Rad2Deg);
            }
        }

        /*vAngle = vAngleIncrement * 0;
        obj = Instantiate(miniaturePrefab);
        obj.transform.parent = transform;
        obj.transform.localPosition = new Vector3(0.0f, Mathf.Sin(vAngle) * distance, Mathf.Cos(vAngle) * distance);
        obj.transform.eulerAngles = new Vector3(-vAngle * Mathf.Rad2Deg, 0.0f, 0.0f);
        obj.transform.RotateAround(Vector3.zero, Vector3.up, hAngle * Mathf.Rad2Deg);

        vAngle = vAngleIncrement * 1;
        obj = Instantiate(miniaturePrefab);
        obj.transform.parent = transform;
        obj.transform.localPosition = new Vector3(0.0f, Mathf.Sin(vAngle) * distance, Mathf.Cos(vAngle) * distance);
        obj.transform.eulerAngles = new Vector3(-vAngle * Mathf.Rad2Deg, 0.0f, 0.0f);
        obj.transform.RotateAround(Vector3.zero, Vector3.up, hAngle * Mathf.Rad2Deg);

        vAngle = vAngleIncrement * 2;
        obj = Instantiate(miniaturePrefab);
        obj.transform.parent = transform;
        obj.transform.localPosition = new Vector3(0.0f, Mathf.Sin(vAngle) * distance, Mathf.Cos(vAngle) * distance);
        obj.transform.eulerAngles = new Vector3(-vAngle * Mathf.Rad2Deg, 0.0f, 0.0f);
        obj.transform.RotateAround(Vector3.zero, Vector3.up, hAngle * Mathf.Rad2Deg);

        vAngle = vAngleIncrement * 3;
        obj = Instantiate(miniaturePrefab);
        obj.transform.parent = transform;
        obj.transform.localPosition = new Vector3(0.0f, Mathf.Sin(vAngle) * distance, Mathf.Cos(vAngle) * distance);
        obj.transform.eulerAngles = new Vector3(-vAngle * Mathf.Rad2Deg, 0.0f, 0.0f);
        obj.transform.RotateAround(Vector3.zero, Vector3.up, hAngle * Mathf.Rad2Deg);*/
	}
	
}
