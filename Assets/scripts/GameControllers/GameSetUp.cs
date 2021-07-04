using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameSetUp : MonoBehaviour
{
    public PhotonPlayer player;

    public ConstructorController constructor;
    public Defensor defensor;
    public Recollector recollector;


    public GameObject GameOver;
    public GameObject Victory;

    public int videsPartida;


    public Transform[] spawnPoints;

    public Colleccionable[] llistaColleccionables;

    void Start()
    {
        GameOver.SetActive(false);
        Victory.SetActive(false);
    }


    public int getSpawnpointLength() { return spawnPoints.Length; }

    public void getRols()
    {
        constructor = FindObjectOfType<ConstructorController>();
        defensor = FindObjectOfType<Defensor>();
        recollector = FindObjectOfType<Recollector>();
    }

    public void PartidaPerduda(PhotonView PV)
    {
        Debug.Log("GAME OVER");
        PV.RPC("RPC_FiPartida", RpcTarget.All, false);

    }

    public void PartidaGuanyada(PhotonView PV)
    {
        Debug.Log("Victoria");
        PV.RPC("RPC_FiPartida", RpcTarget.All, true);
    }

    public void OnBotoVictoria()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(ScenesManager.GetScene(ScenesManager.Scene.MapaNivells));
        }
    }

    /// <summary>
    /// funcio per indicar que s'ha acabat la partida per a tots els jugadors
    /// </summary>
    /// <param name="estat">si es true es guanya la partida, si es false es perd</param>
    [PunRPC]
    private void RPC_FiPartida(bool estat)
    {
        Time.timeScale = 0;
        if (estat)
        {
            Victory.SetActive(true);
        }
        else
        {
            GameOver.SetActive(true);
        }
    }
}
