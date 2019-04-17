using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPController2 : MonoBehaviour {

	Text Player2HPTxt;
	public static int HP;
	// Use this for initialization
	void Start () {
	}
	void Awake()
	{
		Player2HPTxt = GetComponent<Text> ();
		HP = 10;
	}
	// Update is called once per frame
	void Update () {
		Player2HPTxt.text = HP.ToString();
	}
}
