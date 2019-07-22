using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFollow : MonoBehaviour {

	public RectTransform rect;

	void Update () {
		Vector2 screenPoint = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		Vector3 worldPoint = new Vector3 ();
		RectTransformUtility.ScreenPointToWorldPointInRectangle (rect, screenPoint, Camera.main, out worldPoint);
		float z = 0;
		if (worldPoint.x > transform.position.x) {
			z = -Vector3.Angle (Vector3.up, worldPoint - transform.position);
		} else {
			z = Vector3.Angle (Vector3.up, worldPoint - transform.position);
		}
		transform.localRotation = Quaternion.Euler (0, 0, z);
	}
}
