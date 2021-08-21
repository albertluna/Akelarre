using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class atacController : RolController
{
    #region variables
    [Header("GameObjects a control·lar")]
    [SerializeField]
    private Transform[] creators;
    [SerializeField]
    private GameObject bullet;

    //Referència al tempo de l'atac
    private float timer;
    private float maxEspera;
    private float minEspera;
    private float velocitat;
    //Bool per saber si les boles son visibles o no
    private bool isVisible;
    #endregion

    /// <summary> array de gameobjects
    /// https://stuartspixelgames.com/2017/08/02/make-all-of-objects-children-into-an-array-unity-c/
    /// </summary>
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

    /// <summary>
    /// Funció per instanciar les boles
    /// </summary>
    /// <param name="posicio">posicio a la llista de creadors on s'ha d'instanciar</param>
    [PunRPC]
    private void RPC_instanciarAtac(int posicio)
    {
        GameObject instancia =
            Instantiate(bullet, creators[posicio].position, Quaternion.identity, GS.llistaBoles.transform);
        instancia.GetComponent<MovimentAtac>().SetVelocitat(velocitat);
        //Condicional per saber si fer invisible o no les boles d'atac
        if (!isVisible && PV.IsMine) instancia.GetComponent<MovimentAtac>().EliminarBola();
    }

    /// <summary>
    /// Funció per cridar quan s'ha acabat la vida de la casa
    /// </summary>
    public void PartidaPerduda()
    {
        PV.RPC("RPC_PartidaPerduda", RpcTarget.All);
    }

    /// <summary>
    /// Funció per mostrar que s'ha perdut la partida
    /// </summary>
    [PunRPC]
    private void RPC_PartidaPerduda()
    {
        GS.FiPartida(false);
    }

    /// <summary>
    /// Funció set de la variable isVisible
    /// </summary>
    /// <param name="visible">valor de la variable</param>
    public void SetVisibility(bool visible) {
        isVisible = visible;
    }
}
