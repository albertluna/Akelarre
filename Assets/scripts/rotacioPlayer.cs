using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotacioPlayer : MonoBehaviour
{
    public GameObject camera;
    public float MaxSpeed;
    public Rigidbody rigidbody;
    public Vector3 intermig;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        var forward = camera.transform.forward;
        var right = camera.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //this is the direction in the world space we want to move:
        Vector3 desiredMoveDirection = -right * verticalInput + forward * horizontalInput;



        //now we can apply the movement:
        //transform.Translate(desiredMoveDirection * velocity * Time.deltaTime);
        //Debug.Log("Intermig = " + intermig + ". Desired = " + desiredMoveDirection);
        /*if(Vector3.Dot(intermig.normalized, desiredMoveDirection.normalized) == -1)
        {
            Debug.Log("CONTRARI");
            desiredMoveDirection += right;
        }*/

        if (desiredMoveDirection.normalized.magnitude > 0.1)
        {
            //Debug.Log("distancia " + Vector3.Distance(intermig, desiredMoveDirection));
            //intermig = Vector3.Lerp(intermig, desiredMoveDirection, 0.1f);
            rigidbody.MoveRotation(Quaternion.LookRotation(desiredMoveDirection, Vector3.up));
        }

        if (rigidbody.velocity.magnitude > MaxSpeed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * MaxSpeed;
        }

    }
}
