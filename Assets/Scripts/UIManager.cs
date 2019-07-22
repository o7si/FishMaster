using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
	
	public GameObject settingPanel;
	public void OnBackStartMenuButtonDown() {
		// TODO Save Game
		PlayerPrefs.SetInt("Gold", GameController.Instance.gold);
		PlayerPrefs.SetInt ("Lv", GameController.Instance.level);
		PlayerPrefs.SetFloat ("SmallTimer", GameController.Instance.smallTimer);
		PlayerPrefs.SetFloat ("BigTimer", GameController.Instance.bigTimer);
		PlayerPrefs.SetInt ("Exp", GameController.Instance.exp);
		PlayerPrefs.SetInt ("Music", AudioManager.Instance.music.isOn == true ? 1 : 0);
		UnityEngine.SceneManagement.SceneManager.LoadScene (0);
	}
	public void OnGameSettingButtonDown() {
		Time.timeScale = 0;
		settingPanel.SetActive (true);
	}
	public void OnCloseSettingButtonDown() {
		Time.timeScale = 1;
		settingPanel.SetActive (false);
	}
	public void OnMusicToggleDown() {
		AudioManager.Instance.SwitchMute ();
	}

}
