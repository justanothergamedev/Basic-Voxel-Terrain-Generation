using UnityEngine;
using System.Collections;

public class FlyCam : MonoBehaviour {

    //CAN THE PLAYER MOVE THE CAMERA
    public bool canMove;

	//VARIABLES FOR CAMERA ROTATION
	public float lookSensitivity = 5f;
	public float yRotation;
	public float xRotation;
	public float currentYRotation;
	public float currentXRotation;
	public float xRotationY;
	public float yRotationX;
	public float lookSmoothDamp = 0.1f;

	//VARIABLES FOR CAMERA MOVEMENT
	public float movementSpeed = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown (KeyCode.Escape))
        {
            canMove = !canMove;
        }

        if (canMove)
        {
            //CAMERA ROTATION
            yRotation += Input.GetAxis ("Mouse X") * lookSensitivity;
            xRotation -= Input.GetAxis ("Mouse Y") * lookSensitivity;

            //xRotation = Mathf.Clamp(xRotation, -90, 90);

            currentXRotation = Mathf.SmoothDamp (currentXRotation, xRotation, ref xRotationY, lookSmoothDamp);
            currentYRotation = Mathf.SmoothDamp (currentYRotation, yRotation, ref yRotationX, lookSmoothDamp);

            transform.rotation = Quaternion.Euler (currentXRotation, currentYRotation, 0);


            //CAMERA MOVEMENT
            if (Input.GetKey (KeyCode.W))
            {
                transform.Translate (Vector3.forward * movementSpeed * Time.deltaTime);
            }

            if (Input.GetKey (KeyCode.A))
            {
                transform.Translate (Vector3.left * movementSpeed * Time.deltaTime);
            }

            if (Input.GetKey (KeyCode.S))
            {
                transform.Translate (Vector3.forward * -movementSpeed * Time.deltaTime);
            }

            if (Input.GetKey (KeyCode.D))
            {
                transform.Translate (Vector3.left * -movementSpeed * Time.deltaTime);
            }

            if (Input.GetKey (KeyCode.LeftShift))
            {
                movementSpeed = 20f;
            }
            else
            {
                movementSpeed = 10f;
            }
        }
	}
}
