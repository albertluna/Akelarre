using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ConstructorController : RolController
{
    #region variables
    [Header("GameObjects a control·lar")]
    [SerializeField]
    private GameObject creadorColleccionables;
    [SerializeField]
    private GameObject colleccionable;
    [SerializeField]
    public HUD_Constructor hud;

    [Header("Audios")]
    [SerializeField]
    private AudioSource audioConstructor;
    [SerializeField]
    private AudioClip colleccionableCorrecte;
    [SerializeField]
    private AudioClip colleccionableErroni;
    #endregion

    protected override void Start()
    {
        base.Start();
        hud = FindObjectOfType<HUD_Constructor>();
        if(hud != null) hud.pocio.Comencar();
    }

    /// <summary>
    /// Funció per afegir a la vista del constructor el col·leccionable agafat pel recol·lector
    /// </summary>
    /// <param name="colleccionable">Color del col·leccionable a enviar</param>
    public void EnviarColleccionable(string colleccionable)
    {
        PV.RPC("RPC_CrearColleccionable", RpcTarget.All, colleccionable);
    }

    /// <summary>
    /// Funció per afegir a tothom un col·lecccionable en el mapa
    /// </summary>
    /// <param name="nouColleccionable">Color del col·leccionable</param>
    [PunRPC]
    private void RPC_CrearColleccionable(string nouColleccionable)
    {
        NouColleccionable(RecollectorController.escollirColleccionable(nouColleccionable));
    }

    /// <summary>
    /// Funció que introdueix el colleccionable agafat pel recol·lector i el posa a la pantalla del constructor
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

    /// <summary>
    /// Funció que es crida quan el constructor clica el col·leccionable que
    /// li apareix a la pantalla, per continuar amb la poció
    /// </summary>
    public void ClicarMaterial()
    {
        if (colleccionable != null)
        {
            //Si s'ha introduit el col·leccionable correcte
            if (hud.pocio.esColleccionableCorrecte(colleccionable.GetComponent<Colleccionable>()))
            {
                audioConstructor.PlayOneShot(colleccionableCorrecte, 1f);
                hud.pocio.Seguent();
                if(hud.pocio.EsUltim() && PV.IsMine) PV.RPC("RPC_PartidaGuanyada", RpcTarget.All);
            }
            //Si s'ha equivocat de color
            else
            {
                audioConstructor.PlayOneShot(colleccionableErroni, 1f);
                hud.pocio.Comencar();
            }
            hud.actualitzarProgres();
            Destroy(colleccionable);
        }
    }

    /// <summary>
    /// Funció que es crida quan s'ha acabat la poció
    /// </summary>
    [PunRPC]
    private void RPC_PartidaGuanyada()
    {
        GS.FiPartida(true);
    }
}
