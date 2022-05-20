using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Moviment : MonoBehaviourPunCallbacks, IPunObservable
{
    #region variables
    private Rigidbody rigidbody;
    private Transform transform;

    [SerializeField]
    [Range(4000, 6000)]
    private float velocitat;
    //Punter al personatge que es controla, al joystick i la càmera
    [SerializeField]
    RolController controller;
    [SerializeField]
    private JoystickVirtual joystick;
    [SerializeField]
    private CameraMoviment camera;
    #endregion

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        
        if (controller.photonView.IsMine) camera.SetJugador(this);
    }

    public void FixedUpdate()
    {
        if (controller.photonView.IsMine)
        {
            //S'obté la informació dels inputs
            float horizontalInput = 0;
            float verticalInput = 0;
            #if UNITY_STANDALONE || PLATFORM_STANDALONE
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            #endif
            horizontalInput += joystick.InputDirection.x;
            verticalInput += joystick.InputDirection.y;
            //Es calcula a direcció i velocitat amb la que s'ha de moure
            Vector3 direccio = camera.transform.right * horizontalInput + camera.transform.forward * verticalInput * 3;
            rigidbody.velocity = velocitat * new Vector3(direccio.x, 0, direccio.z);
            //Es rota el personatge mirant cap on es mou
            if (direccio != Vector3.zero) {
                transform.forward = new Vector3(rigidbody.velocity.x, transform.forward.y, rigidbody.velocity.z);
                //transform.Rotate(new Vector3(0, -90, 0));
            }
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
