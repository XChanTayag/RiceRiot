using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Quit : MonoBehaviour {

	// Use this for initialization
	public Canvas canvas;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void QUIT()
	{
		canvas.enabled = false;
	}
	public void Exit()
	{
		Application.Quit ();
	}
}
