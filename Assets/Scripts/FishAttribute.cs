using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAttribute : MonoBehaviour {

	public int maxNumber;
	public int maxSpeed;
	public int hp;
	public int exp;
	public int gold;
	public GameObject diePrefab;
	public GameObject goldPrefab;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Border") {
			Destroy (gameObject);
		}
	}

	public void TakeDamage(int value) {
		hp -= value;
		if (hp <= 0) {
			GameController.Instance.gold += gold;
			GameController.Instance.exp += exp;
			if (transform.GetComponent<EF_PlayEffect> () != null) {
				transform.GetComponent<EF_PlayEffect> ().PlayEffect ();
			}
			GameObject die = Instantiate (diePrefab);
			die.transform.SetParent (transform.parent, false);
			die.transform.position = transform.position;
			die.transform.rotation = transform.rotation;
			GameObject goldGo = Instantiate (goldPrefab);
			goldGo.transform.SetParent (transform.parent, false);
			goldGo.transform.position = transform.position;
			goldGo.transform.rotation = transform.rotation;
			Destroy (gameObject);
		}
	}

}
