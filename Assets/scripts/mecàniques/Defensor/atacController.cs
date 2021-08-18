using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class atacController : RolController
{
    [Header("GameObjects a control·lar")]
    [SerializeField]
    private Transform[] creators;
    [SerializeField]
    private GameObject bullet;

    private float timer;
    private float maxEspera;
    private float minEspera;
    private float velocitat;
    //Bool per saber si les boles son visibles o no
    [SerializeField]
    private bool isVisible;

    /// <summary> array de gameobjects
    /// https://stuartspixelgames.com/2017/08/02/make-all-of-objects-children-into-an-array-unity-c/
    /// </summary>
    /// 
    protected override void Start()
    {
        base.Start();
        creators = GS.llistaCreadorsAtac.GetComponentsInChildren<Transform>();
        //Es setteja els valors d'instanciar atacs en funcio de la partida
        timer = GS.initialOffsetTimer;
        maxEspera = GS.maximTime;
        minEspera = GS.minimTime;
        velocitat = GS.velocitatBoles;
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine) {
            //https://stackoverflow.com/questions/33182283/how-do-i-create-random-game-objects-at-runtime-in-unity-2d-using-c
            if (timer >= 0) timer -= Time.deltaTime;
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
            Instantiate(bullet, creators[posicio].position, Quaternion.identity, GS.llistaBoles.transform);
        instancia.GetComponent<MovimentAtac>().velocitat = velocitat;
        //Condicional per saber sifer invisible o no les boles d'atac
        if (!isVisible && PV.IsMine) instancia.GetComponent<MovimentAtac>().EliminarBola();
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

    public void SetVisibility(bool visible) {
        isVisible = visible;
    }
}
