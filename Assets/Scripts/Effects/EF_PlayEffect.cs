using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EF_PlayEffect : MonoBehaviour {

	public GameObject[] effectPrefabs;

	public void PlayEffect() {
		foreach (GameObject effectPrefab in effectPrefabs) {
			Instantiate (effectPrefab);
		}
	}

}
