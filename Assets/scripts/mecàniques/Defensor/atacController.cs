using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class atacController : MonoBehaviour
{
    public GameObject AC;

    public Transform[] creators;
    public GameObject bullet;
    public float timer;
    public float maxEspera;
    public float minEspera;
    //public GameObject llistaCreators;
    public GameObject instanciador;
    public PhotonView PV;
    public Defensor defensor;
    public GameSetUp GS;
    public bool isVisible;

    /// <summary> array de gameobjects
    /// https://stuartspixelgames.com/2017/08/02/make-all-of-objects-children-into-an-array-unity-c/
    /// </summary>
    /// 
    void Start()
    {
        AC = GameObject.Find("creadorsAtac");
        instanciador = GameObject.Find("instanciador");
        creators = AC.GetComponentsInChildren<Transform>();
        PV = GetComponent<PhotonView>();
        GS = FindObjectOfType<GameSetUp>();
        //defensor = GetComponent<Defensor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine) {
            //https://stackoverflow.com/questions/33182283/how-do-i-create-random-game-objects-at-runtime-in-unity-2d-using-c
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                int posicio = Random.Range(1, creators.Length);
                PV.RPC("RPC_instanciarAtac", RpcTarget.All, posicio);
                timer = Random.Range(minEspera, maxEspera);
            }
        }
    }

    [PunRPC]
    private void RPC_instanciarAtac(int posicio)
    {
        GameObject instancia =
            Instantiate(bullet, creators[posicio].position, Quaternion.identity, instanciador.transform);
        //Condicional per saber sifer invisible o no les boles d'atac
        if (!isVisible && PV.IsMine) instancia.GetComponent<MovimentAtac>().EliminarBola();
        //instancia.GetComponent<MovimentAtac>().defensor = defensor;
    }


    public void PartidaPerduda()
    {
        PV.RPC("RPC_PartidaPerduda", RpcTarget.All);
    }
    [PunRPC]
    private void RPC_PartidaPerduda()
    {
        GS.FiPartida(false);
    }

    public void SetVisibility(bool visible) { isVisible = visible;
        Debug.Log("Visibilitat = " + visible);
    }
}
