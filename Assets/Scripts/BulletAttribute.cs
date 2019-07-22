using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttribute : MonoBehaviour {

	public int speed;
	public int damage;
	public GameObject webPrefab;

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Border") {
			Destroy (gameObject);
		}
		if (collision.tag == "Fish") {
			GameObject web = Instantiate (webPrefab);
			web.transform.SetParent (transform.parent, false);
			web.transform.position = transform.position;
			web.GetComponent<WebAttribute> ().damage = damage;
			Destroy (gameObject);
		}
	}

}
