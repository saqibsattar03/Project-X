using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    void Rotate() 
    {
        float mouseX = Input.GetAxis("Mouse X") * 100 * Time.deltaTime;
        parent.Rotate(Vector3.up, mouseX);
    }
}
