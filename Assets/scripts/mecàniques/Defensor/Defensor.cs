﻿using System.Collections;
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
    [SerializeField]
    private AudioClip destrossarBoles;
    [SerializeField]
    private AudioClip destrossarColleccionables;
    
    //public int NombreVides;

    // Start is called before the first frame update
    void Start()
    {
        GS = FindObjectOfType<GameSetUp>();
        HUDVides.maxValue = GS.videsPartida;

        vides = GS.videsPartida;
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
            GetComponent<AudioSource>().PlayOneShot(destrossarColleccionables, 1f);
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

    public void setVisibility(bool visiblity)
    {
        if(PV.IsMine)
            controller.SetVisibility(visiblity);
    } 
}
