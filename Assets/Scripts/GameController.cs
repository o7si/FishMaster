using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour {

	private static GameController _instance;
	public static GameController Instance {
		get { 
			return _instance;
		}
	}
		
	private void Awake() {
		_instance = this;
	}

	public GameObject[] guns;
	private int[] costs = { 5, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
	private string[] lvNames = { "新手", "入门", "钢铁", "青铜", "白银", "黄金", "白金", "钻石", "大师", "宗师" };
	private int costIndex = 0;
	public Transform bulletHolder;
	public GameObject[] bulletGroup1;
	public GameObject[] bulletGroup2;
	public GameObject[] bulletGroup3;
	public GameObject[] bulletGroup4;
	public GameObject[] bulletGroup5;

	public Text costText;
	public Text goldText;
	public Text lvText;
	public Text lvNameText;
	public Text smallTimerText;
	public Text bigTimeText;
	public Button bigTimerButton;
	public Button settingButton;
	public Button backButton;
	public Slider expSlider;

	public GameObject lvUpTip;
	public GameObject lvUpEffect;
	public GameObject bigTimeEndEffect;
	public GameObject changeEffect;
	public GameObject waveEffect;
	public Image background;
	public Sprite[] bgSprites;

	public int backIndex = 0;
	public int level = 1;
	public int gold = 500;
	public int exp = 0;
	public const int bigTimerWait = 180;
	public const int smallTimerWait = 45;
	public float bigTimer = bigTimerWait;
	public float smallTimer = smallTimerWait;

	void Start() {
		gold = PlayerPrefs.GetInt ("Gold", gold);
		level = PlayerPrefs.GetInt ("Lv", level);
		exp = PlayerPrefs.GetInt ("Exp", exp);
		bigTimer = PlayerPrefs.GetFloat ("BigTimer", bigTimer);
		smallTimer = PlayerPrefs.GetFloat ("SmallTimer", smallTimer);

		goldColor = goldText.color;
		costText.text = "$" + costs [costIndex];
	}
	void Update() {
		bigTimer -= Time.deltaTime;
		smallTimer -= Time.deltaTime;
		if (smallTimer <= 0) {
			smallTimer = smallTimerWait;
			gold += 100;
		}
		if (bigTimer <= 0 && bigTimerButton.gameObject.activeSelf == false) {
			bigTimeText.gameObject.SetActive (false);
			bigTimerButton.gameObject.SetActive (true);
		}
		ChangeButtetCost ();
		Fire ();
		UpdateUI ();
	}

	void UpdateUI() {
		// 升级所需经验=1000+200*当前等级
		while(exp >= 1000 + 200 * level) {
			exp = exp - (1000 + 200 * level);
			level++;
			if (level / 20 > backIndex) {
				Instantiate (waveEffect);
				AudioManager.Instance.PlayEffectSound (AudioManager.Instance.waveClip);
				backIndex = level / 20;
				backIndex = backIndex > 3 ? 0 : backIndex;
				background.sprite = bgSprites[backIndex];
			}
			Instantiate (lvUpEffect);
			AudioManager.Instance.PlayEffectSound (AudioManager.Instance.lvUpClip);
			lvUpTip.transform.Find ("Text").GetComponent<Text> ().text = level.ToString ();
			lvUpTip.SetActive (true);
			StartCoroutine (NoActiveSelf (2.0f));
		}
		goldText.text = "$" + gold.ToString ();
		lvText.text = level.ToString ();
		if (level / 10 <= 9) {
			lvNameText.text = lvNames [level / 10];
		} else {
			lvNameText.text = lvNames [9];
		}
		smallTimerText.text = " " + (int)(smallTimer / 10) + "  " + (int)(smallTimer % 10);
		bigTimeText.text = (int)bigTimer + "s";
		expSlider.value = (float)exp / (1000 + 200 * level);
	}

	IEnumerator NoActiveSelf(float time) {
		yield return new WaitForSeconds (time);
		lvUpTip.SetActive (false);
	}

	void Fire() {
		GameObject[] bulletGroup = null;
		if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false) {
			if (gold < costs [costIndex]) {
				StartCoroutine (GoldNotEnough ());
				return;
			}
			switch (costIndex / 4) {
			case 0:
				bulletGroup = bulletGroup1;
				break;
			case 1:
				bulletGroup = bulletGroup2;
				break;
			case 2:
				bulletGroup = bulletGroup3;
				break;
			case 3:
				bulletGroup = bulletGroup4;
				break;
			case 4:
				bulletGroup = bulletGroup5;
				break;
			}
			int index = level % 10;
			gold -= costs [costIndex];
			GameObject bullet = Instantiate (bulletGroup[index]);
			AudioManager.Instance.PlayEffectSound (AudioManager.Instance.fireClip);
			bullet.transform.SetParent (bulletHolder, false);
			bullet.transform.position = guns [costIndex / 4].transform.Find ("FirePosition").transform.position;
			bullet.transform.rotation = guns [costIndex / 4].transform.Find ("FirePosition").transform.rotation;
			bullet.GetComponent<BulletAttribute> ().damage = costs [costIndex];
			bullet.AddComponent<EF_AutoMove> ().dir = Vector3.up;
			bullet.GetComponent<EF_AutoMove> ().speed = bullet.GetComponent<BulletAttribute> ().speed;
		}
	}

	void ChangeButtetCost() {
		if (Input.GetKeyDown(KeyCode.D)) {
			OnReduceButtonDown ();
		}
		if (Input.GetKeyDown(KeyCode.F)) {
			OnAddButtonDown ();
		}
	}

	public void OnAddButtonDown() {
		Instantiate (changeEffect);
		AudioManager.Instance.PlayEffectSound (AudioManager.Instance.changeGunClip);
		guns [costIndex / 4].SetActive (false);
		costIndex++;
		costIndex = costIndex >= costs.Length ? costs.Length - 1 : costIndex;
		guns [costIndex / 4].SetActive (true);
		costText.text = "$" + costs [costIndex];
	}

	public void OnReduceButtonDown() {
		Instantiate (changeEffect);
		AudioManager.Instance.PlayEffectSound (AudioManager.Instance.changeGunClip);
		guns [costIndex / 4].SetActive (false);
		costIndex--;
		costIndex = costIndex < 0 ? 0 : costIndex;
		guns [costIndex / 4].SetActive (true);
		costText.text = "$" + costs [costIndex];
	}

	public void OnBigTimerButtonDown() {
		Instantiate (bigTimeEndEffect);
		gold += 1000;
		bigTimerButton.gameObject.SetActive (false);
		bigTimer = bigTimerWait;
		bigTimeText.gameObject.SetActive (true);
	}

	public Color goldColor;
	IEnumerator GoldNotEnough() {
		goldText.color = Color.red;
		yield return new WaitForSeconds (0.5f);
		goldText.color = goldColor;
	}

}
