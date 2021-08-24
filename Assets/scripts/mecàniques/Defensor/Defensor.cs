using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Defensor : MonoBehaviour
{
    /*public GameSetUp GS;
    [SerializeField]
    private PhotonView PV;*/
    [SerializeField]
    private int vides;
    [SerializeField]
    private AtacController controller;
    [SerializeField]
    private Slider HUDVides;
    [SerializeField]
    private AudioClip destrossarBoles;
    [SerializeField]
    private AudioClip destrossarColleccionables;
    
    void Start()
    {
        vides = controller.gameSetup.GetVides();

        HUDVides.maxValue = vides;

        HUDVides.maxValue = vides;
        HUDVides.value = vides;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))//&&PV.IsMine)
        {
            GetComponent<AudioSource>().PlayOneShot(destrossarBoles, 1f);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Colleccionable"))
        {
            Colleccionable colleccionable = collision.GetComponentInParent<Colleccionable>();
            colleccionable.parent.EliminarColleccionable(colleccionable);
            GetComponent<AudioSource>().PlayOneShot(destrossarColleccionables, 1f);
        }
    }

    public void UnaVidaMenys()
    {
        vides--;
        if (controller.photonView.IsMine)
        {
            HUDVides.value = vides;
            if (vides <= 0)
            {
                controller.PartidaPerduda();
            }
        }
    }

    public void SetVisibilitat(bool visiblitat)
    {
        if(controller.photonView.IsMine)
            controller.SetVisibilitat(visiblitat);
    } 
}
