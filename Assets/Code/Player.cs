using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction lookAction;

    private float cameraXRotation;
    private Vector3 velocity;
    
    public PlayerInput playerInputAction;
    public float speed = 10f;
    public float gravity = -9.8f;
    public CharacterController controller;
    public Transform camera;
    public float mouseSensitivity = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
        moveAction = playerInputAction.currentActionMap.FindAction("Movement");
        lookAction = playerInputAction.currentActionMap.FindAction("look");
    }

    // Update is called once per frame
    void Update()
    {
        //y = WS, x =A D
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        Vector3 moveAmount = transform.right * moveInput.x + transform.forward * moveInput.y;
        moveAmount *= speed * Time.deltaTime;

        //gravity auf Y
        velocity.x = moveAmount.x;
        velocity.y += gravity * Time.deltaTime * Time.deltaTime;
        velocity.z = moveAmount.z;
        
        
        if (controller.Move(velocity) == CollisionFlags.Below);
        {
            velocity.y = 0f;  
        }
        
        Vector2 lookInput = lookAction.ReadValue<Vector2>();

        cameraXRotation -= lookInput.y * mouseSensitivity;
        cameraXRotation = Mathf.Clamp(cameraXRotation, 0, 80);
        camera.eulerAngles = new Vector3(cameraXRotation, camera.eulerAngles.y, camera.eulerAngles.z);
        
        //player rotation
        transform.Rotate(0f, lookInput.x, 0f);




        //Kamera position aus der Rotation berechenen
        //x= cos
        //y = sin
        //Winkel muss in Radiant umgewandelt werden
        //float angle = cameraXRotation * Mathf.Deg2Rad;
        //float z = -Mathf.Cos(angle);
        //float y = Mathf.Sin(angle);
        //camera.localPosition = new Vector3(camera.localPosition.x, y, z) * 10f;


        //camera.Rotate(lookInput.y, 0f, 0f, Space.World);

    }
    
    
    
    
    
}

