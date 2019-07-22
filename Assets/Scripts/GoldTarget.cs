using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldTarget : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Gold") {
			AudioManager.Instance.PlayEffectSound (AudioManager.Instance.getGoldClip);
			Destroy (collision.gameObject);
		}
	}

}
