using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Moviment : MonoBehaviourPunCallbacks, IPunObservable
{
    private Rigidbody rigidbody;
    [SerializeField]
    private PhotonView PV;
    [SerializeField]
    [Range(9000, 14000)]
    public float MaxSpeed;

    private float horizontalInput;
    private float verticalInput;

    [Range(4000, 6000)]
    public float velocity;

    [SerializeField]
    private camera camera;

    private Vector3 dir;

    [SerializeField]
    private JoystickVirtual joystick;


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (PV.IsMine) {
            horizontalInput = 0;
            verticalInput = 0;
        #if UNITY_STANDALONE || PLATFORM_STANDALONE    
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
        #endif

            horizontalInput += joystick.InputDirection.x;
            verticalInput += joystick.InputDirection.y;
            dir = camera.transform.right * horizontalInput + camera.transform.forward * verticalInput*3;
            rigidbody.velocity = velocity * new Vector3(dir.x, 0, dir.z);
        }
    }

    public void FixedUpdate()
    {
        if (dir != Vector3.zero)
        {
            GetComponent<Transform>().forward = new Vector3(rigidbody.velocity.x,
                GetComponent<Transform>().forward.y, rigidbody.velocity.z);
            GetComponent<Transform>().Rotate(new Vector3(-90, -90, 0));
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
