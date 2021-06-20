﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Moviment : MonoBehaviourPunCallbacks, IPunObservable
{
    private Rigidbody rigidbody;
    public PhotonView PV;
    private bool controllable = true;

    public float MaxSpeed;

    private float horizontalInput;
    private float verticalInput;

    public float velocity;
    public camera camera;
    Vector3 dir;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
        {
            Destroy(camera.gameObject);
        }
    }

    public override void OnEnable()
    {
        Start();
    }


    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine) {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            dir = camera.transform.forward + camera.transform.right;
            dir.Normalize();

            //animator.SetFloat("Speed", Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

            Vector3 tmp = camera.transform.right * horizontalInput + camera.transform.forward * verticalInput;
            rigidbody.velocity = velocity * new Vector3(tmp.x, 0, tmp.z);
        }
    }

    public void FixedUpdate()
    {
        if (!PV.IsMine || !controllable)
        {
            return;
        }

        //Quaternion rot = rigidbody.rotation * Quaternion.Euler(0, horizontalInput * MaxSpeed/100 * Time.fixedDeltaTime, 0);
        //GetComponent<Rigidbody>().MoveRotation(rot);

        //Vector3 force = (rot * Vector3.forward) * verticalInput * 10000.0f * MaxSpeed * Time.fixedDeltaTime;
        //GetComponent<Rigidbody>().AddForce(force);


        /*Vector3 projeccio = new Vector3();
        projeccio = (camera.transform.position - this.transform.position);
        projeccio.y = 0;
        Debug.Log("PROJECCIO = " + projeccio.normalized);
        Vector3 dir = new Vector3(horizontalInput, 0, verticalInput);

        float dif = Vector3.Angle(projeccio, new Vector3(1,0,0));*/

        var forward = camera.transform.forward;
        var right = camera.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //this is the direction in the world space we want to move:
        var desiredMoveDirection = right * verticalInput  - forward * horizontalInput;
        Debug.Log("Moviment " + desiredMoveDirection);

        //now we can apply the movement:
        //transform.Translate(desiredMoveDirection * velocity * Time.deltaTime);

        //Quaternion.Lo
        if (desiredMoveDirection.magnitude > 0.1)
        {
            GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(-desiredMoveDirection, Vector3.up));
        }
        //this.transform.LookAt(tmp);


        if (GetComponent<Rigidbody>().velocity.magnitude > MaxSpeed)
        {
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * MaxSpeed;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.transform.position);
        } else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
        }
    }
}
