using UnityEngine;

public class PlayerController : MonoBehaviour
{

   // [SerializeField] private float playerSpeed = 2.0f;

    private Rigidbody playerRb;
    public Animator anim;
    public bool isGrounded;
    public LayerMask layerMask;
	[SerializeField] private float mouseSensitivity;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
		mouseSensitivity = 400;
    }

    // Update is called once per frame
	private void Update()
	{
		Grounded();
		Jump();
		Move();
	}

	//protected void PlayerMovement() 
	//{
	//    if (Input.GetKey(KeyCode.W))
	//    {
	//        Debug.Log("w pressed");
	//        playerRb.AddForce(Vector3.forward * Time.deltaTime * playerSpeed, ForceMode.Impulse);
	//    }
	//    else if (Input.GetKey(KeyCode.S))
	//    {
	//        Debug.Log("s pressed");
	//        playerRb.AddForce(Vector3.back * Time.deltaTime * playerSpeed, ForceMode.Impulse);
	//    }

	//    else if (Input.GetKey(KeyCode.A))
	//    {
	//        Debug.Log("a pressed");
	//        playerRb.AddForce(Vector3.left * Time.deltaTime * playerSpeed, ForceMode.Impulse);
	//    }
	//    else if (Input.GetKey(KeyCode.D))
	//    {
	//        Debug.Log("d pressed");
	//        playerRb.AddForce(Vector3.right * Time.deltaTime * playerSpeed, ForceMode.Impulse);
	//    }
	//}

	private void AimTowardMouse()
    {
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		transform.Rotate(Vector3.up, mouseX);
	}

	protected void Jump() 
	{
		if (Input.GetKey(KeyCode.Space) && isGrounded) 
		{
			playerRb.AddForce(Vector3.up * 2, ForceMode.Impulse);
		}
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

	protected void Move() 
	{
		AimTowardMouse();
		float verticalAxis = Input.GetAxis("Vertical");
		float horizontalAxis = Input.GetAxis("Horizontal");

		Vector3 movement = transform.forward * verticalAxis + transform.right * horizontalAxis;
		movement.Normalize();

		transform.position += movement * 0.04f;

		anim.SetFloat("vertical", verticalAxis);
		anim.SetFloat("horizontal",horizontalAxis);
	}
}
