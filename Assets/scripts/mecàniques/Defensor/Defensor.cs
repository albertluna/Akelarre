using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Defensor : MonoBehaviour
{
    public GameSetUp GS;
    [SerializeField]
    private PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        GS = FindObjectOfType<GameSetUp>();
        PV = GetComponent<PhotonView>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Colleccionable"))&&PV.IsMine)
        {
            Destroy(collision.gameObject);
        }        
    }

    public void UnaVidaMenys()
    {
        if (PV.IsMine) { PV.RPC("RPC_unaVidaMenys", RpcTarget.All); Debug.Log("RESTA VIDA"); }
    }

    [PunRPC]
    private void RPC_unaVidaMenys()
    {
        GS.restaVida();
    }
}
