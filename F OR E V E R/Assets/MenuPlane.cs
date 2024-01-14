using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlane : MonoBehaviour
{
   public GameObject Plane;
    [SerializeField] public GameObject PlaneGraphics;

    //Rotation
    float rotationSpeed;
    public float maxRotationSpeed;
    

    public float forwardSpeed = 25f, strafeSpeed = 7.5f; //hoverSpeed = 5f

    private float activeForwardSpeed, activeStrafeSpeed; //activeHoverSpeed

    //How fast the plane goes from 0 to moving
    private float strafeAcceleration = 2f;

    private Rigidbody rb;

    Quaternion targetRotation;

    void Start()
    {
        
    rb = this.gameObject.GetComponent<Rigidbody>();        
    }

    void FixedUpdate()
    {
        if(PlaneGraphics)
        {
        var hInput = Input.GetAxis("Horizontal");
        rotationSpeed = maxRotationSpeed * hInput;

        rb.velocity = new Vector3(hInput * activeForwardSpeed, 0f, forwardSpeed);

        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal")* strafeSpeed, strafeAcceleration * Time.fixedDeltaTime);
          
        //activeHoverSpeed = Input.GetAxisRaw("Hover") * hoverSpeed;

        rb.MovePosition(transform.position + transform.right * activeStrafeSpeed * Time.fixedDeltaTime);   
        }
    }

    private void Update()
    {
        var hInput = Input.GetAxis("Horizontal");
        //Turning behaviour
        targetRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.AngleAxis(-50 * Mathf.Sign(hInput), Vector3.forward), Mathf.Abs(rotationSpeed / maxRotationSpeed));
        if(PlaneGraphics)
        {
        PlaneGraphics.transform.localRotation = Quaternion.Slerp(PlaneGraphics.transform.localRotation, targetRotation,Time.deltaTime * 4);
        }
    }   
}
