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
    public GameObject[] llistaColleccionables;
    public PhotonView PV;
    public Camera camera;
    public GameSetUp GS;

    // Start is called before the first frame update
    void Start()
    {
        pocio.Comencar();
        PV = GetComponent<PhotonView>();
        GS = FindObjectOfType<GameSetUp>();

        if (!PV.IsMine)
        {
            Destroy(hud.gameObject);
            Destroy(camera.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.F))
        {
            NouColleccionable();
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
            Destroy(colleccionable);
        }
        colleccionable = Instantiate(nouColleccionable.gameObject,
            creadorColleccionables.transform.position, Quaternion.identity, creadorColleccionables.transform); ;
    }

    public void ClicarMaterial()
    {
        if (colleccionable != null)
        {
            Debug.Log("MAterial CLicat - desplaçar a la pocio i seguir camí");
            if (pocio.esCollecicionableCorrecte(colleccionable.GetComponent<Colleccionable>()))
            {
                Debug.Log("Seguent amb " + colleccionable.GetComponent<Colleccionable>().color);
                pocio.Seguent();
            }
            else
            {
                pocio.Comencar();
            }
            hud.actualitzarProgres();
            Destroy(colleccionable);
        }
    }

    /*[PunRPC]
    private void RPC_Victoria()
    {
        ScenesManager.Load(ScenesManager.Scene.MenuMultijugador);
    }*/
}
