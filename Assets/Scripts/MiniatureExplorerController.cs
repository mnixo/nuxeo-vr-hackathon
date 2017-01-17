using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniatureExplorerController : MonoBehaviour {

    public GameObject miniaturePrefab;

	void Start() {
        float distance = 4.5f;
        float vAngleIncrement = 18.0f;
        float hAngleIncrement = 20.0f;
        float vAngleInital = -8.0f;
        int rows = 3;
        int columns = 2;
        GameObject obj;

        //hAngle = hAngleIncrement * 0;

        /*for (float hAngle = -hAngleIncrement * 2; hAngle < hAngleIncrement * 3; hAngle += hAngleIncrement) {
            for (float vAngle = 0.0f; vAngle < vAngleIncrement * 3; vAngle += vAngleIncrement) {
                obj = Instantiate(miniaturePrefab);
                obj.transform.parent = transform;
                obj.transform.localPosition = new Vector3(0.0f, Mathf.Sin(vAngle) * distance, Mathf.Cos(vAngle) * distance);
                obj.transform.eulerAngles = new Vector3(-vAngle * Mathf.Rad2Deg, 0.0f, 0.0f);
                obj.transform.RotateAround(Vector3.zero, Vector3.up, hAngle * Mathf.Rad2Deg);
            }
        }*/

        for (int r = rows - 1; r >= 0; r--) {
            for (int c = -columns; c <= columns; c++) {
                obj = Instantiate(miniaturePrefab);
                obj.transform.parent = transform;
                obj.transform.localPosition = new Vector3(0.0f, 0.0f, distance);
                obj.transform.RotateAround(Vector3.zero, Vector3.left, r * vAngleIncrement + vAngleInital);
                obj.transform.RotateAround(Vector3.zero, Vector3.up, c * hAngleIncrement);
            }
        }

	}
	
}
