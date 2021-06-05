using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Recollector : MonoBehaviour
{
    Rigidbody rb;
    Collider colider;
    public int vides;
    public PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        colider = GetComponent<CapsuleCollider>();
        vides = 3;
        PV = GetComponent<PhotonView>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Colleccionable"))
        {
            Debug.Log("Transportar colleccionable a constructor");

            Colleccionable colleccionable = collision.gameObject.GetComponent<Colleccionable>();
            PV.RPC("RPC_sendColleccionable", RpcTarget.All, colleccionable.color);

            colleccionable.parent.estaOcupat = false;
            Destroy(collision.gameObject);

        } else if(collision.gameObject.CompareTag("Bullet") && vides > 0)
        {
            vides--;
            Destroy(collision.gameObject);            
        }
    }

    [PunRPC]
    private void RPC_sendColleccionable(string colleccionable)
    {
        Debug.Log("ENVIA YEAHHH");
        ConstructorController.CrearColleccionable(colleccionable);
    }

}
