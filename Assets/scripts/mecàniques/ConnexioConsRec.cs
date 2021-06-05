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
        if (constructor != null)
        {
            Debug.Log("ENVIA YEAHHH " + constructor.name);
            constructor.CrearColleccionable(colleccionable);
        }
    }
}
