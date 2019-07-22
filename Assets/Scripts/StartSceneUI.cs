using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneUI : MonoBehaviour {

	public void NewGame() {
		PlayerPrefs.DeleteAll ();
		UnityEngine.SceneManagement.SceneManager.LoadScene (1);
	}

	public void ContinueGame() {
		UnityEngine.SceneManagement.SceneManager.LoadScene (1);
	}

}
