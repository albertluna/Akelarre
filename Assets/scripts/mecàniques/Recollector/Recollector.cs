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
            //GameObject.Find("ConnectionController").GetComponent<ConnexioConsRec>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Colleccionable"))
        {
            Debug.Log("Transportar colleccionable a constructor");

            Colleccionable colleccionable = collision.gameObject.GetComponent<Colleccionable>();
            colleccionable.parent.estaOcupat = false;
            Destroy(collision.gameObject);

        }
        else if (collision.gameObject.CompareTag("Bullet") && vides > 0)
        {
            vides--;
            Destroy(collision.gameObject);
        }
    }
}
