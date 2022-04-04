using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float playerSpeed = 2.0f;

    private Rigidbody playerRb;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   
        PlayerMovement();
    }

    protected void PlayerMovement() 
    {
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("w pressed");
            playerRb.AddForce(Vector3.forward * Time.deltaTime * playerSpeed, ForceMode.Impulse);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("s pressed");
            playerRb.AddForce(Vector3.back * Time.deltaTime * playerSpeed, ForceMode.Impulse);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("a pressed");
            playerRb.AddForce(Vector3.left * Time.deltaTime * playerSpeed, ForceMode.Impulse);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("d pressed");
            playerRb.AddForce(Vector3.right * Time.deltaTime * playerSpeed, ForceMode.Impulse);
        }
    }

    private void AimTowardMouse()
    {
		float mouseX = Input.GetAxis("Mouse X") * 150 * Time.deltaTime;
		transform.Rotate(Vector3.up, mouseX);
	}
}
