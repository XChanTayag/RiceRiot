using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour {

	Text Player1HPTxt;
	public static int HP;
	// Use this for initialization
	void Start () {
	}
	void Awake()
	{
		Player1HPTxt = GetComponent<Text> ();
		HP = 10;
	}
	// Update is called once per frame
	void Update () {
		Player1HPTxt.text = HP.ToString();
	}
}
