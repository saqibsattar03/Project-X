using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float cameraHeight;
    [SerializeField] private float cameraDistance;
    [SerializeField] private float cameraAngle;
    [SerializeField] private float smoothSpeed = 0.5f;
    private Vector3 refVelocity;

    // Start is called before the first frame update
    void Start()
    {
        HandleCamera();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        HandleCamera();
    }

    protected void HandleCamera() 
    {
        if (!target) 
        {
            //if camera doesn't find player, return back 
            return;
        }

        // Build World position vector
        Vector3 worldPosition = (Vector3.forward * -cameraDistance) + (Vector3.up * cameraHeight);

        //Build rotated angle

        Vector3 rotatedVector = Quaternion.AngleAxis(cameraAngle, Vector3.up) * worldPosition;

		//Move camera position

		Vector3 flatTargetPosition = target.position;
		flatTargetPosition.y = 0.0f;
		Vector3 finalPosition = flatTargetPosition + rotatedVector;


        //smoothing the camera rotation 
        transform.position = Vector3.SmoothDamp(transform.position, finalPosition,ref refVelocity, smoothSpeed);
        transform.LookAt(target.position);

	}
}
