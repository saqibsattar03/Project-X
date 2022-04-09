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
	[SerializeField] float attackRate = 2f;
	[SerializeField] float nextAttackTime = 0f;
	[SerializeField] float normalSpeed = 0.04f;
	[SerializeField] float runningSpeed = 0.04f;
	public float demo;


	// Start is called before the first frame update
	void Start()
	{
		InstantiateCharacter(100, 10, 2);
		playerRb = GetComponent<Rigidbody>();
		mouseSensitivity = 400;
	}

	//Update is called once per frame
	private void Update()
	{
		Grounded();
		Jump();
		PlayerMovement();
		PlayerAtatck();
		Debug.Log(Time.time);
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
		Debug.Log(transform.position += movement * Speed());
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
				anim.SetTrigger("attack");
				//Light Attack
				Attack(this.damage, enemyLayers);
				nextAttackTime = Time.time + 1f / attackRate;
			}
			if (Input.GetMouseButton(1))
			{
				//Heavy Attack
				Attack(this.damage * this.doubleDamage, enemyLayers);
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
}
