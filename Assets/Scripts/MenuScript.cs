using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuScript : MonoBehaviour {

	// Use this for initialization
	public Canvas canvas;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnStartGame(string name)
	{
		SceneManager.LoadScene (name);
	}

	public void AppQuit()
	{
		canvas.enabled = true;
		//Application.Quit ();
	}
}
