using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody2D))]
public class Knife : MonoBehaviour {
	
	[SerializeField]
	private float speed;

	private Rigidbody2D myRigidbody;

	public Vector2 direction;
	public static bool d;


	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate()
	{
		myRigidbody.velocity = direction * speed;
		if (direction.x == 1) {
			d = true;
		}
		else if(direction.x == -1)
		{
			d = false;
		}

	}
	// Update is called once per frame
	void Update () {
	
	}

	public void Initialize(Vector2 direction)
	{
		this.direction = direction;
	}
	void OnBecameInvisible()
	{
		Destroy (gameObject);
	}
}
