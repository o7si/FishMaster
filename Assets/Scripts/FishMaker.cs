using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMaker : MonoBehaviour {

	public Transform fishHolder;
	public Transform[] genPositions;
	public GameObject[] fishPrefabs;

	public float genWaitTime = 0.3f;
	public float waitTime = 0.5f;

	void Start () {
		InvokeRepeating ("FishGenerate", 0, genWaitTime);
	}

	void FishGenerate() {
		int posIndex = Random.Range (0, genPositions.Length);
		int fishIndex = Random.Range (0, fishPrefabs.Length);
		int maxNumber = fishPrefabs [fishIndex].GetComponent<FishAttribute> ().maxNumber;
		int maxSpeed = fishPrefabs [fishIndex].GetComponent<FishAttribute> ().maxSpeed;
		int number = Random.Range (maxNumber / 2 + 1, maxNumber);
		int speed = Random.Range (maxSpeed / 2, maxSpeed);
		int moveType = Random.Range (0, 2);		// 0: 直走鱼群; 1: 转向鱼群
		int angleOffset;
		int angleSpeed;
		if (moveType == 0) {
			// TODO 直行鱼群的生成
			angleOffset = Random.Range(-22, 22);
			StartCoroutine (GenForword (posIndex, fishIndex, number, speed, angleOffset));
		} else {
			// TODO 转向鱼群的生成
			if (Random.Range (0, 2) == 0) {
				angleSpeed = Random.Range (-15, -9);
			} else {
				angleSpeed = Random.Range (9, 15);
			}
			StartCoroutine (GenRotate (posIndex, fishIndex, number, speed, angleSpeed));
		}
	}

	IEnumerator GenForword(int posIndex, int fishIndex, int number, int speed, int angleOffset) {
		for (int i = 0; i < number; i++) {
			GameObject fish = Instantiate (fishPrefabs [fishIndex]);
			fish.transform.SetParent (fishHolder, false);
			fish.transform.localPosition = genPositions [posIndex].localPosition;
			fish.transform.localRotation = genPositions [posIndex].localRotation;
			fish.transform.Rotate (0, 0, angleOffset);
			fish.GetComponent<SpriteRenderer> ().sortingOrder += i;
			fish.AddComponent<EF_AutoMove> ().speed = speed;
			yield return new WaitForSeconds (waitTime);
		}
	}

	IEnumerator GenRotate(int posIndex, int fishIndex, int number, int speed, int angleSpeed) {
		for (int i = 0; i < number; i++) {
			GameObject fish = Instantiate (fishPrefabs [fishIndex]);
			fish.transform.SetParent (fishHolder, false);
			fish.transform.localPosition = genPositions [posIndex].localPosition;
			fish.transform.localRotation = genPositions [posIndex].localRotation;
			fish.GetComponent<SpriteRenderer> ().sortingOrder += i;
			fish.AddComponent<EF_AutoMove> ().speed = speed;
			fish.AddComponent<EF_AutoRotate> ().rotSpeed = angleSpeed;
			yield return new WaitForSeconds (waitTime);
		}
	}

}
