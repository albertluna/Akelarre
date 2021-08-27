using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ConstructorController : RolController
{
    #region variables
    [Header("GameObjects a controlar")]
    //Posició on es crea els col·leccionables recollits pel recol·lector
    [SerializeField]
    private GameObject creadorColleccionables;
    //Punter al col·leccionable de la vista
    [SerializeField]
    private GameObject colleccionable;
    [Header("Gestió del HUD")]
    //HUD de la llista de la poció
    private Slider llista;
    private Pocio pocio;

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
        if (photonView.IsMine)
        {
            llista = gameSetup.GetHudLlista().GetComponentInChildren<Slider>();
            pocio = gameSetup.GetPocio();
            llista.maxValue = pocio.GetLlargadaLlista();
            pocio.Comencar();
        }
    }

    /// <summary>
    /// Funció per afegir a la vista del constructor el col·leccionable agafat pel recol·lector
    /// </summary>
    /// <param name="colleccionable">Color del col·leccionable a enviar</param>
    public void EnviarColleccionable(string colleccionable)
    {
        photonView.RPC("RPC_CrearColleccionable", RpcTarget.All, colleccionable);
    }

    /// <summary>
    /// Funció per afegir a tothom un col·lecccionable a la terrasa de la casa
    /// </summary>
    /// <param name="nouColleccionable">Color del col·leccionable</param>
    [PunRPC]
    private void RPC_CrearColleccionable(string nouColleccionable)
    {
        NouColleccionable(gameSetup.recollector.EscollirColleccionable(nouColleccionable));
    }

    /// <summary>
    /// Funció que introdueix el colleccionable agafat pel recol·lector i el posa a la pantalla del constructor
    /// </summary>
    /// <param name="nouColleccionable">el tipus del nou colleccionable</param>
    private void NouColleccionable(Colleccionable nouColleccionable)
    {
        //S'elimina el col·leccionable anterior i s'instancia el nou
        if (colleccionable != null) Destroy(colleccionable.gameObject);
        colleccionable = Instantiate(nouColleccionable.gameObject,
            creadorColleccionables.transform.position, Quaternion.identity, creadorColleccionables.transform);
        //Es reprodueix l'audio d'aparicio de colleccionable
        if(photonView.IsMine) audioConstructor.PlayOneShot(audioConstructor.clip, 1f);
    }

    /// <summary>
    /// Funció per gestionar quan s'insereix un nou col·leccionable a la poció
    /// </summary>
    public void ClicarMaterial()
    {
        if (colleccionable != null)
        {
            //Si s'ha introduït el col·leccionable correcte
            if (pocio.EsColleccionableCorrecte(colleccionable.GetComponent<Colleccionable>()))
            {
                audioConstructor.PlayOneShot(colleccionableCorrecte, 1f);
                pocio.Seguent();
                if (pocio.EsUltim() && photonView.IsMine) photonView.RPC("RPC_PartidaGuanyada", RpcTarget.All);
            }
            //Si s'ha equivocat de color
            else
            {
                audioConstructor.PlayOneShot(colleccionableErroni, 1f);
                pocio.Comencar();
            }
            //S'actualitza el HUD en funció de com va la poció
            llista.value = pocio.GetIndex();
            photonView.RPC("RPC_EliminarColleccionable", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_EliminarColleccionable()
    {
        Destroy(colleccionable);
    }

    /// <summary>
    /// Funció que es crida quan s'ha acabat la poció
    /// </summary>
    [PunRPC]
    private void RPC_PartidaGuanyada()
    {
        gameSetup.FiPartida(true);
    }
}
