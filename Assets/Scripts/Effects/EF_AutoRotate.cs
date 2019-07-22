using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EF_AutoRotate : MonoBehaviour {

	public float rotSpeed = 10f;

	void Update () {
		transform.Rotate (Vector3.forward, rotSpeed * Time.deltaTime);
	}
}
