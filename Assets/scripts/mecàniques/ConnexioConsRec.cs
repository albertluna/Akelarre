using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class ConnexioConsRec : MonoBehaviour
{

    public string colleccionable;
    public PhotonView PV;
    public ConstructorController constructor;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        constructor = GetComponent<ConstructorController>();
    }


    public void enviarInfo(string colleccionable)
    {
        PV.RPC("RPC_sendColleccionable", RpcTarget.All, colleccionable);
    }

    [PunRPC]
    private void RPC_sendColleccionable(string colleccionable)
    {

        Debug.Log("ESTEM enviant merda. Quina? dOncs aquesta + " + colleccionable + ". Des de " + this.gameObject.name);
        if (constructor != null)
        {
            Debug.Log("ENVIA YEAHHH " + constructor.name);
            constructor.CrearColleccionable(colleccionable);
        }
    }
}
