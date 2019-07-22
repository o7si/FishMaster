using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EF_WaveMove : MonoBehaviour {

	public Vector3 temp;

	void Start () {
		temp = -transform.position;
	}

	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, temp, 10 * Time.deltaTime);
	}
}
