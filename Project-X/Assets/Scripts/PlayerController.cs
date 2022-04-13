using UnityEngine;

public class PlayerController : Character
{

   // [SerializeField] private float playerSpeed = 2.0f;

    private Rigidbody playerRb;
	private bool isRunning;  


	[SerializeField] private Animator anim;
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask layerMask;
	[SerializeField] private float mouseSensitivity;
	[SerializeField] private float attackRate = 2f;
	[SerializeField] private float nextAttackTime = 0f;
	[SerializeField] private float normalSpeed = 0.2f;
	[SerializeField] private float runningSpeed = 0.04f;


	// Start is called before the first frame update
	void Start()
	{
		InstantiateCharacter(100,100, 2);
		playerRb = GetComponent<Rigidbody>();
		mouseSensitivity = 400;
	}

	//Update is called once per frame
	void Update()
	{
		Grounded();
		Jump();
		PlayerMovement();
		PlayerAtatck();
		PauseGame();
	}

	protected void Grounded()
	{
		if (Physics.CheckSphere(transform.position + Vector3.down, 0.2f, layerMask))
		{

			isGrounded = true;
			Debug.Log("grounded " + isGrounded);
		}
		else 
		{
			isGrounded = false;
			Debug.Log("grounded " + isGrounded);
		}
			
		anim.SetBool("jump", !isGrounded);
	}

	protected void PlayerMovement() 
	{
		AimTowardMouse();
		float verticalAxis = Input.GetAxis("Vertical");
		float horizontalAxis = Input.GetAxis("Horizontal");

		Vector3 movement = transform.forward * verticalAxis + transform.right * horizontalAxis;
		movement.Normalize();

		transform.position += movement * Speed(); 
		//Debug.Log(transform.position += movement * Speed());
		if (isRunning)
		{
			anim.SetTrigger("isRunning");
		}
		else 
		{
			anim.SetFloat("vertical", verticalAxis, 0.1f, Time.deltaTime);
			anim.SetFloat("horizontal", horizontalAxis, 0.1f, Time.deltaTime);
		}
	}

	protected void AimTowardMouse()
	{
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		transform.Rotate(Vector3.up, mouseX);
	}

	protected void PlayerAtatck()
	{
		// avoid spamming attack

		if (Time.time >= nextAttackTime)
		{
			if (Input.GetMouseButton(0))
			{
				//Light Attack
				;
				Attack("l_attack", 0, 0.25f, attackPoint, enemyLayers, this.damage,0, "Sword Swing", "Enemy Impact", "Enemy Scream");
				nextAttackTime = Time.time + 1f / attackRate;
			}
			if (Input.GetMouseButton(1))
			{
				//Heavy Attack
				
				Attack("h_attack", 1, 0.4f, attackPoint, enemyLayers, this.damage * this.doubleDamage, 1, "Sword Swing", "Enemy Impact", "Enemy Scream") ;
				nextAttackTime = Time.time + 1f / attackRate;
			}
		}
	}
	protected void Jump()
	{
		if (Input.GetKey(KeyCode.Space) && isGrounded)
		{
			anim.SetBool("jump", true);
			playerRb.AddForce(Vector3.up * 2, ForceMode.Impulse);

		}
	}
	protected float Speed() 
	{
		if (Input.GetKey(KeyCode.Q))
		{
			isRunning = true;
			return normalSpeed;
		}
		else
		{
			isRunning = false;
			return normalSpeed;
		}
	}

	protected void PauseGame() 
	{
		if (Input.GetKey(KeyCode.Escape)) 
		{
			GameManager.instance.PauseGame();
		}
	}

	protected void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "box")
		{
			GameManager.instance.chestPrefab.SetActive(false);
			GameManager.instance.winnerPanel.SetActive(true);
		}
	}
}
