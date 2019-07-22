using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EF_GoldMove : MonoBehaviour {

	public Transform toTarget;

	void Start () {
		toTarget = GameObject.Find ("ToTarget").transform;
	}

	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, toTarget.position, 10.0f * Time.deltaTime);
	}
}
