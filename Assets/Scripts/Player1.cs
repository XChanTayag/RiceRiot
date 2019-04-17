using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}
public class Player1 : MonoBehaviour {

	public Image img;
	public Image img2;
	public Image img3;
	public AudioSource aS;
	public AudioSource aS2;
	public AudioSource aS3;


	private Rigidbody2D myRigidbody;
	private Animator myAnimator;
	[SerializeField]
	private float movementSpeed;
	private bool facingLeft;
	private bool attack;
	[SerializeField]
	private GameObject knifePrefab;
	public Boundary boundary;

	[SerializeField]
	private Transform[] groundPoints;
	[SerializeField]
	private float groundRadius;
	[SerializeField]
	private LayerMask whatIsGround;


	private bool isGrounded;

	private int jumpCounter = 0;

	public BoxCollider2D midGround;

	private Transform transf;

	private float knockbackCount = 0f;

	private bool backHit;
	private bool frontHit;

	private float directions;

	private BoxCollider2D BoxCol2D;

	private float nextFire;
	private float fireRate;

	int hp;

	int imgCtr = 1;
	// Use this for initialization
	void Start () 
	{
		facingLeft = true;
		myRigidbody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
		transf = GetComponent<Transform> ();
		BoxCol2D = GetComponent<BoxCollider2D> ();
		fireRate = 0.30f;
		hp = 10;
		img.enabled = false;
		img2.enabled = false;
		img3.enabled = false;
	}

	void Update()
	{
		HandleInput ();
	}
	// Update is called once per frame
	void FixedUpdate ()
	{
		float horizontal = Input.GetAxis ("Horizontal");

		HandleMovement (horizontal);

		Flip (horizontal);

		HandleAttacks ();

		ResetValues ();

		isGrounded = IsGrounded ();
		/*myRigidbody.position = new Vector2 
		(
				Mathf.Clamp(myRigidbody.position.x, boundary.xMin, boundary.xMax),
				Mathf.Clamp(myRigidbody.position.y, boundary.yMin,boundary.yMax)
		);*/
	}

	private void HandleMovement(float horizontal)
	{
		if (knockbackCount <= 0) {
			if (!this.myAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Attack")) {
				myRigidbody.velocity = new Vector2 (horizontal * movementSpeed, myRigidbody.velocity.y); //x = -1, y = 0;
			}
			myAnimator.SetFloat ("speed", Mathf.Abs (horizontal));
		}
		else 
		{
			myRigidbody.velocity = new Vector2 (directions, myRigidbody.velocity.y);
			knockbackCount -= Time.deltaTime;
		}
	}
	private void HandleAttacks()
	{
		
		if (attack && !this.myAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Attack")) {
			myAnimator.SetTrigger ("attack");
			myRigidbody.velocity = Vector2.zero;

		}
		

	}

	private void HandleInput()
	{
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			attack = true;
		}
		if (Input.GetKeyDown (KeyCode.T)) 
		{
			ThrowKnife (0);
		}
		if (Input.GetKeyDown (KeyCode.W))
		{
			jumpCounter++;
			JumpHandler ();
		}
		if (Input.GetKeyDown (KeyCode.S))
		{
			if (isGrounded) {
				BoxCol2D.isTrigger = true;
			} else {
				BoxCol2D.isTrigger = false;
			}
			//midGround.enabled = false;
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene ("PickStage");
		}
	}

	private void Flip(float horizontal)
	{
		if (horizontal < 0 && facingLeft || horizontal > 0 && !facingLeft)
		{
			facingLeft = !facingLeft;

			Vector3 theScale = transform.localScale;
			theScale.x *= -1;

			transform.localScale = theScale;
		}
	}
	private void ResetValues()
	{
		attack = false;
	}

	public void ThrowKnife(int value)
	{
		if (value == 0) 
		{
			if (facingLeft) {
				if(Time.time > nextFire)
				{
					nextFire = Time.time + fireRate;
					GameObject tmp= (GameObject)Instantiate (knifePrefab, transform.position, Quaternion.Euler (new Vector3 (0, 0, -90)));
					tmp.GetComponent<Knife>().Initialize(Vector2.right);
				}
			} 
			else 
			{
				if (Time.time > nextFire) {
					nextFire = Time.time + fireRate;
					GameObject tmp = (GameObject)Instantiate (knifePrefab, transform.position, Quaternion.Euler (new Vector3 (0, 0, 90)));
					tmp.GetComponent<Knife> ().Initialize (Vector2.left);
				}
			}
		}

	}

	public void JumpHandler()
	{
		if (isGrounded) {
			jumpCounter = 0;
		}
		if (jumpCounter <= 1) {
			isGrounded = false;
			myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, 5f);
		}


	}
	private bool IsGrounded()
	{
		if (myRigidbody.velocity.y <= 0) 
		{
			foreach (Transform point in groundPoints) 
			{
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, whatIsGround);

				for(int i = 0; i< colliders.Length;i++)
				{
					if (colliders [i].gameObject != gameObject) 
					{
						return true;
					}
				}
			}
		}
		return false;
			
	}
	public virtual void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "MidGround") 
		{
			BoxCol2D.isTrigger = false;
		}
		if (other.tag == "BottomGround") {
			BoxCol2D.isTrigger = false;
			return;
		}
	}
	public virtual void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "EnemyKnife")
		{
			if(EnemyKnife.d == true)
			{
				directions = 5f;
			}
			else if(EnemyKnife.d == false)
			{
				directions = -5f;
			}
			Destroy (other.gameObject);
			Debug.Log ("tinamaan");
			knockbackCount = 0.5f;
		}
	}

	void OnBecameInvisible()
	{
		hp--;
		HPController.HP = hp;
		if (imgCtr == 1) {
			aS.Play ();
			img.enabled = true;
			imgCtr++;
		} else if (imgCtr == 2) {
			aS2.Play ();
			img2.enabled = true;
			imgCtr++;
		} else {
			aS3.Play ();
			img3.enabled = true;
			imgCtr = 1;
		}
		StartCoroutine (Respawn ());
		Debug.Log ("nasira tangina mo");
	}

	IEnumerator Respawn()
	{
		yield return new WaitForSeconds (5);
		float x = -4.95f;
		float y = 3.96f;

		myRigidbody.position = new Vector2 (x,y);

		img.enabled = false;
		img2.enabled = false;
		img3.enabled = false;
	}

}
