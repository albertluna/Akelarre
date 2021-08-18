using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ConstructorController : RolController
{
    [Header("GameObjects a control·lar")]
    [SerializeField]
    private GameObject creadorColleccionables;
    [SerializeField]
    private GameObject colleccionable;
    [SerializeField]
    private Pocio pocio;
    [SerializeField]
    public HUD_Constructor hud;

    [Header("Audios")]
    [SerializeField]
    private AudioSource audioConstructor;
    [SerializeField]
    private AudioClip colleccionableCorrecte;
    [SerializeField]
    private AudioClip colleccionableErroni;

    protected override void Start()
    {
        base.Start();
        pocio = FindObjectOfType<Pocio>();
        pocio.Comencar();
        hud = FindObjectOfType<HUD_Constructor>();
    }

    public void EnviarColleccionable(string colleccionable)
    {
        PV.RPC("RPC_CrearColleccionable", RpcTarget.All, colleccionable);
    }

    [PunRPC]
    private void RPC_CrearColleccionable(string nouColleccionable)
    {
        NouColleccionable(recollectorController.escollirColleccionable(nouColleccionable));
    }

    /// <summary>
    /// Funcio que introdueix el colleccionable agafat pel recollector i el posa a la pantalla del constructor
    /// </summary>
    /// <param name="nouColleccionable">el tipus del nou colleccionable</param>
    public void NouColleccionable(Colleccionable nouColleccionable)
    {
        if (colleccionable != null) Destroy(colleccionable.gameObject);
        colleccionable = Instantiate(nouColleccionable.gameObject,
            creadorColleccionables.transform.position, Quaternion.identity, creadorColleccionables.transform);
        //Es reprodueix l'audio d'aparicio de colleccionable
        if(PV.IsMine) audioConstructor.PlayOneShot(audioConstructor.clip, 1f);
    }

    public void ClicarMaterial()
    {
        if (colleccionable != null)
        {
            if (pocio.esCollecicionableCorrecte(colleccionable.GetComponent<Colleccionable>()))
            {
                audioConstructor.PlayOneShot(colleccionableCorrecte, 1f);
                pocio.Seguent();
                if(pocio.EsUltim() && PV.IsMine) PV.RPC("RPC_PartidaGuanyada", RpcTarget.All);
            }
            else
            {
                audioConstructor.PlayOneShot(colleccionableErroni, 1f);
                pocio.Comencar();
            }
            hud.actualitzarProgres();
            Destroy(colleccionable);
        }
    }

    [PunRPC]
    private void RPC_PartidaGuanyada()
    {
        GS.FiPartida(true);
    }    
}
