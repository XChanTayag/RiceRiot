using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WINLOSE : MonoBehaviour {

	// Use this for initialization
	int flag;
	void Start () {
		flag = 0;	
	}
	
	// Update is called once per frame
	void Update () {
		if (flag == 0) {
			if (HPController.HP == 0) {
				Debug.Log ("Player 1 WIN");
				WinController.WinnerDecider1 ();
				StartCoroutine (Back());
				flag++;
			} else if (HPController2.HP == 0) {
				Debug.Log ("Player 2 WIN");
				WinController.WinnerDecider2 ();
				StartCoroutine (Back());
				flag++;
			}
		}
			
	}
	IEnumerator Back()
	{
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene ("PickStage");
	}
}
