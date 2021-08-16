using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Rendering.PostProcessing;

public class GameSetUp : MonoBehaviour
{
    #region variables
    public PhotonPlayer player;

    public ConstructorController constructor;
    public Defensor defensor;
    public recollectorController recollector;


    public GameObject GameOver;
    public GameObject Victory;



    public Transform[] spawnPoints;

    public Colleccionable[] llistaColleccionables;

    //Variable per borrar la llista de la pocio
    public GameObject HUD;

    [Header("Referència a l'atac")]
    public int videsPartida;
    /// <summary>
    /// Varaible per indicar si el defensor pot veure les boles d'atac
    /// </summary>
    public bool atacVisible;
    public float initialOffsetTimer;
    public float minimTime;
    public float maximTime;
    [Range(0.05f, 0.2f)]
    public float velocitatBoles;

    /// <summary>
    /// Variable per indicar si el rol veu en blanc i negre
    /// </summary>
    [Header("Referència a la visibilitat")]
    public bool grisRecollector;
    public bool grisConstructor;
    #endregion variables

    void Awake()
    {
        GameOver.SetActive(false);
        Victory.SetActive(false);
        Time.timeScale = 1;
        PhotonNetwork.MinimalTimeScaleToDispatchInFixedUpdate = 0;
    }

    public int getSpawnpointLength() { return spawnPoints.Length; }

    public void getRols()
    {
        constructor = FindObjectOfType<ConstructorController>();
        defensor = FindObjectOfType<Defensor>();
        recollector = FindObjectOfType<recollectorController>();

        //s'indica si el defensor pot veure les boles o no
        if (defensor != null) defensor.setVisibility(atacVisible);
        //s'indica si els rols han de veure en blanc i negre
        if (recollector != null) if(grisRecollector && recollector.PV.IsMine) setBiN();
        if (constructor != null) if(grisConstructor && constructor.PV.IsMine) setBiN();
        //Es destrueix la llista de la pocio pels no-constructors
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

    //Funcio per transformar la visio de la camera a blanc i negre
    private void setBiN()
    {
        PostProcessVolume postpo = FindObjectOfType<PostProcessVolume>();
        ColorGrading color;
        if (postpo.profile.TryGetSettings<ColorGrading>(out color))
        {
            color.saturation.value = -100;
        }
    }
}
