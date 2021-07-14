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
    private int vides;
    [SerializeField]
    private atacController controller;
    [SerializeField]
    private Slider HUDvides;
    public int NombreVides;

    // Start is called before the first frame update
    void Start()
    {
        HUDvides.maxValue = NombreVides;
        GS = FindObjectOfType<GameSetUp>();
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
        if (PV.IsMine)
        {
            HUDvides.value = vides;
            if (vides == 0)
            {
                controller.PartidaPerduda();
            }
        }
    }

    
}
