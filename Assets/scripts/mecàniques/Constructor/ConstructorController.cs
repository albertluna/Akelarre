using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ConstructorController : MonoBehaviour
{
    public GameObject creadorColleccionables;
    public GameObject colleccionable;
    public GameObject colleccionablesExterns;
    public Pocio pocio;
    public HUD_Constructor hud;
    public GameObject botoClicarColleccionable;
    public GameObject[] llistaColleccionables;
    public PhotonView PV;
    public Camera camera;
    public GameSetUp GS;

    // Start is called before the first frame update
    void Start()
    {
        pocio = FindObjectOfType<Pocio>();
        pocio.Comencar();
        PV = GetComponent<PhotonView>();
        GS = FindObjectOfType<GameSetUp>();
        hud = FindObjectOfType<HUD_Constructor>();


        if (!PV.IsMine)
        {
            Destroy(hud.gameObject);
            Destroy(camera.gameObject);
            Destroy(botoClicarColleccionable);
        }
    }
    private void Awake()
    {
        hud = FindObjectOfType<HUD_Constructor>();
        if (!PV.IsMine)
        {
            Destroy(hud.gameObject);
        }

    }

    public void NouColleccionable()
    {
        if (colleccionable != null)
        {
            Destroy(colleccionable);
        }
        colleccionable = Instantiate(colleccionablesExterns, creadorColleccionables.transform);
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

    public void NouColleccionable(Colleccionable nouColleccionable)
    {
        if (colleccionable != null)
        {
            Destroy(colleccionable.gameObject);
        }
        colleccionable = Instantiate(nouColleccionable.gameObject,
            creadorColleccionables.transform.position, Quaternion.identity, creadorColleccionables.transform);
    }

    public void ClicarMaterial()
    {
        if (colleccionable != null)
        {
            if (pocio.esCollecicionableCorrecte(colleccionable.GetComponent<Colleccionable>()))
            {
                pocio.Seguent();
                if(pocio.EsUltim() && PV.IsMine)
                {
                    PV.RPC("RPC_PartidaGuanyada", RpcTarget.All);
                }
            }
            else
            {
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
