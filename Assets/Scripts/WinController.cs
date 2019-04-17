using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WinController : MonoBehaviour {

	// Use this for initialization
	static Text winnertext;
	void Start () {
		Debug.Log (Application.loadedLevelName);
		if (Application.loadedLevelName == "LavaStage") {
			winnertext.color = Color.white;
		}
		else if(Application.loadedLevelName == "RiceRiot")
		{
			winnertext.color = Color.black;
		}
		else if(Application.loadedLevelName == "BeachStage")
		{
			winnertext.color = Color.black;
		}
	}
	void Awake()
	{
		winnertext = GetComponent<Text> ();
	}
	// Update is called once per frame
	void Update () {
		
	}

	public static void WinnerDecider1()
	{
		
		winnertext.text = "Player 2 Win";
	}
	public static void WinnerDecider2()
	{
		
		winnertext.text = "Player 1 Win";
	}
}
