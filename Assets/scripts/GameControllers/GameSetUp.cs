using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameSetUp : MonoBehaviour
{
    #region variables
    public PhotonPlayer player;

    public ConstructorController constructor;
    public Defensor defensor;
    public recollectorController recollector;


    public GameObject GameOver;
    public GameObject Victory;

    public int videsPartida;


    public Transform[] spawnPoints;

    public Colleccionable[] llistaColleccionables;

    //Variable per borrar la llista de la pocio
    public GameObject HUD;
    [Header("Referència a l'atac")]
    /// <summary>
    /// Varaible per indicar si el defensor pot veure les boles d'atac
    /// </summary>
    public bool atacVisible;
    public float initialOffsetTimer;
    public float minimTime;
    public float maximTime;
    public float velocitatBoles;

    /// <summary>
    /// Variable per indicar si el recollector veu en blanc i negre
    /// </summary>
    public bool grisRecollector;
    #endregion variables

    void Start()
    {
        GameOver.SetActive(false);
        Victory.SetActive(false);
        Time.timeScale = 1;
    }


    public int getSpawnpointLength() { return spawnPoints.Length; }

    public void getRols()
    {
        constructor = FindObjectOfType<ConstructorController>();
        defensor = FindObjectOfType<Defensor>();
        recollector = FindObjectOfType<recollectorController>();

        //s'indica si el defensor pot veure les boles o no
        if (defensor != null) defensor.setVisibility(atacVisible);
        if (grisRecollector) recollector.SetBiN();
        if (constructor == null || !constructor.PV.IsMine) Destroy(HUD);
    }

    public void OnBotoVictoria()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.LoadLevel(ScenesManager.GetScene(ScenesManager.Scene.MapaNivells));
        }
    }

    /// <summary>
    /// funcio per indicar que s'ha acabat la partida per a tots els jugadors
    /// </summary>
    /// <param name="estat">si es true es guanya la partida, si es false es perd</param>
    public void FiPartida(bool estat)
    {
        PhotonNetwork.MinimalTimeScaleToDispatchInFixedUpdate = 0;
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
