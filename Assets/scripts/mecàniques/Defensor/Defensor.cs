using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Defensor : MonoBehaviour
{
    public GameSetUp GS;
    [SerializeField]
    private PhotonView PV;
    [SerializeField]
    public int vides;
    [SerializeField]
    private atacController controller;
    [SerializeField]
    private Slider HUDVides;
    //public int NombreVides;

    // Start is called before the first frame update
    void Start()
    {
        GS = FindObjectOfType<GameSetUp>();
        HUDVides.maxValue = GS.videsPartida;

        vides = GS.videsPartida;
        if (!PV.IsMine)
        {
            Destroy(HUDVides.gameObject);
        }
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
        if (PV.IsMine)
        {
            HUDVides.value = vides;
            if (vides <= 0)
            {
                controller.PartidaPerduda();
            }
        }
    }

    
}
