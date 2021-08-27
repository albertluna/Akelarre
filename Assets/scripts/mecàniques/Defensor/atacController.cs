using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class AtacController : RolController
{
    #region variables
    [Header("GameObjects a controlar")]
    //Llista de punts de creació del objecte pedra
    [SerializeField]
    private Transform[] creators;
    [SerializeField]
    private GameObject pedra;
    [SerializeField]
    private Defensor defensor;
    private int vides;
    [SerializeField]
    private Slider HUDVides;
    
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
        creators = gameSetup.llistaCreadorsAtac.GetComponentsInChildren<Transform>();
        //Es setteja els valors d'instanciar atacs en funcio de la partida
        timer = gameSetup.tempsOffsetInicial;
        maxEspera = gameSetup.tempsMaximAtac;
        minEspera = gameSetup.tempsMinimAtac;
        velocitat = gameSetup.velocitatBoles;

        vides = gameSetup.GetVides();
        HUDVides.maxValue = vides;
        HUDVides.value = vides;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine) {
            //https://stackoverflow.com/questions/33182283/how-do-i-create-random-game-objects-at-runtime-in-unity-2d-using-c
            if (timer >= 0) timer -= Time.deltaTime;
            else
            {
                int posicio = Random.Range(1, creators.Length);
                photonView.RPC("RPC_InstanciarAtac", RpcTarget.All, posicio);
                timer = Random.Range(minEspera, maxEspera);
            }
        }
    }

    /// <summary>
    /// Funció per instanciar les boles a tots els jugadors
    /// </summary>
    /// <param name="posicio">posicio a la llista de creadors on s'ha d'instanciar</param>
    [PunRPC]
    private void RPC_InstanciarAtac(int posicio)
    {
        MovimentAtac instancia = Instantiate(pedra, creators[posicio].position, Quaternion.identity,
            gameSetup.llistaBoles.transform).GetComponent<MovimentAtac>();
        instancia.SetVelocitat(velocitat);
        //Condicional per saber si fer invisible o no les boles d'atac
        if (!isVisible && photonView.IsMine) instancia.EliminarBola();
    }

    /// <summary>
    /// Funció per mostrar que s'ha perdut la partida
    /// </summary>
    [PunRPC]
    private void RPC_PartidaPerduda()
    {
        gameSetup.FiPartida(false);
    }

    /// <summary>
    /// Funció per restar una vida i activar la pantalla si es perd la partida
    /// </summary>
    public void UnaVidaMenys()
    {
        vides--;
        if (photonView.IsMine)
        {
            HUDVides.value = vides;
            if (vides <= 0)
            {
                //Crida de la funció per acabar la partida a tots els jugadors
                photonView.RPC("RPC_PartidaPerduda", RpcTarget.All);
            }
        }
    }

    public void SetVisibilitat(bool visible) { isVisible = visible; }
}
