﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Recollector : MonoBehaviour
{
    private int vides;
    public PhotonView PV;
    public GameSetUp GS;
    public HUD_Recollector HUD;
    public recollectorController rc;
    

    // Start is called before the first frame update
    void Start()
    {
        vides = HUD.NombreVides();
        GS = FindObjectOfType<GameSetUp>();        
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Colleccionable") && PV.IsMine)
        {
            Debug.Log("Transportar colleccionable a constructor");

            Colleccionable colleccionable = collision.gameObject.GetComponentInParent<Colleccionable>();

            ConstructorController constructor = FindObjectOfType<ConstructorController>();
            constructor.EnviarColleccionable(colleccionable.color);
            //PV.RPC("RPC_destroyColleccionable", RpcTarget.All, collision.gameObject as Object);

            int index = rc.indexColleccionable(colleccionable.gameObject);
            if (index == -1) Debug.LogError("Fail");

            rc.deleteColleccionable(index);
        }
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && vides > 0)
        {
            vides--;
            HUD.ActualitzarVides(vides);
            Destroy(collision.gameObject);
        }
    }
}
