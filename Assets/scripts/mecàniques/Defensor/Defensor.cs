using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Defensor : MonoBehaviour
{
    public GameSetUp GS;
    [SerializeField]
    private PhotonView PV;
    [SerializeField]
    private SphereCollider collider;
    [SerializeField]
    private Casa casa;
    [SerializeField]
    private int vides;

    // Start is called before the first frame update
    void Start()
    {
        GS = FindObjectOfType<GameSetUp>();
        PV = GetComponent<PhotonView>();
        vides = GS.videsPartida;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if ((collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Colleccionable")))//&&PV.IsMine)
        {
            Destroy(collision.gameObject);
        }        
    }

    public void UnaVidaMenys()
    {
        vides--;
        if (vides == 0)
        {
            GS.PartidaPerduda(PV);
        }
    }
}
