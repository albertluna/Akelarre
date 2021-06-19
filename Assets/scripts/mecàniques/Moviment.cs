using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Moviment : MonoBehaviourPunCallbacks, IPunObservable
{
    private Rigidbody rigidbody;
    public PhotonView PV;
    private bool controllable = true;

    public float MaxSpeed = 0.2f;

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
        if (!PV.IsMine)
        {
            return;
        }

        if (!controllable)
        {
            return;
        }

        Quaternion rot = rigidbody.rotation * Quaternion.Euler(0, horizontalInput * MaxSpeed * Time.fixedDeltaTime, 0);
        GetComponent<Rigidbody>().MoveRotation(rot);

        Vector3 force = (rot * Vector3.forward) * verticalInput * 1000.0f * MaxSpeed * Time.fixedDeltaTime;
        GetComponent<Rigidbody>().AddForce(force);

        if (GetComponent<Rigidbody>().velocity.magnitude > (MaxSpeed * 1000.0f))
        {
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * MaxSpeed * 1000.0f;
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
