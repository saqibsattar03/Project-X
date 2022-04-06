using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookAt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PerspectiveCamera();
    }

    private void PerspectiveCamera() 
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float midPoint = (transform.position - Camera.main.transform.position).magnitude * 0.5f;
        transform.LookAt(mouseRay.origin + mouseRay.direction * midPoint);
    }

    private void OrthoGraphicCamera() 
    {
        transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
