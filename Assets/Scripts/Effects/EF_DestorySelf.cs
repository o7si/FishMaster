using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EF_DestorySelf : MonoBehaviour {

	public float delay = 1f;

	private void Start() {
		Destroy (gameObject, delay);
	}

}
