﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Rendering.PostProcessing;

public class GameSetUp : MonoBehaviour
{
    #region variables
    //Punters als 3 rols en escena
    public ConstructorController constructor;
    public Defensor defensor;
    public RecollectorController recollector;
    //Panells amb la pantalla de game over i la de victòria
    [SerializeField]
    private GameObject gameOver;
    [SerializeField]
    private GameObject victory;

    //Variable per borrar la llista de la pocio
    [SerializeField]
    private GameObject HudLlistaPocio;

    [Header("Posició d'origen de cada rol")]
    public Transform[] spawnPoints;   

    [Header("Referència a l'atac")]
    [SerializeField]
    private int videsPartida;
    
    [Range(5, 30)]
    public float tempsOffsetInicial;
    [Range(5, 20)]
    public float tempsMinimAtac;
    [Range(10, 30)]
    public float tempsMaximAtac;
    [Range(0.03f, 0.20f)]
    public float velocitatBoles;

    //Variable per indicar si el rol veu en blanc i negre
    [Header("Referència a la visibilitat")]
    public bool grisRecollector;
    public bool grisConstructor;
    //Variable per indicar si el recol·lector veu les boles a col·leccionar
    private bool colleccionablesInvisibles;
    //Variable per indicar si el defensor pot veure les boles d'atac
    [SerializeField]
    private bool atacVisible;

    //GameObjects amb tota les llistes de col·leccionables i atacs
    [Header("Llistes inicials")]
    public GameObject llistesRecollector;
    public GameObject llistaCreadorsAtac;
    public GameObject llistaBoles;
    #endregion variables

    void Awake()
    {
        gameOver.SetActive(false);
        victory.SetActive(false);
        Time.timeScale = 1;
        PhotonNetwork.MinimalTimeScaleToDispatchInFixedUpdate = 0;
    }

    /// <summary>
    /// Funció per obtenir la referència de tots els jugadors
    /// i actualitzar la seva visibilitat a la del nivell
    /// </summary>
    public void GetRols()
    {
        constructor = FindObjectOfType<ConstructorController>();
        defensor = FindObjectOfType<Defensor>();
        recollector = FindObjectOfType<RecollectorController>();

        //s'indica si el defensor pot veure les boles o no
        if (defensor != null) defensor.SetVisibilitat(atacVisible);
        if (recollector != null) recollector.isInvisible = colleccionablesInvisibles;
        //s'indica si els rols han de veure en blanc i negre
        if (recollector != null) if(grisRecollector && recollector.photonView.IsMine) SetBlancNegre();
        if (constructor != null) if(grisConstructor && constructor.photonView.IsMine) SetBlancNegre();
        //Es destrueix la llista de la pocio pels no-constructors
        if (constructor == null || !constructor.photonView.IsMine) if(HudLlistaPocio!=null) Destroy(HudLlistaPocio);
    }

    /// <summary>
    /// Funció que es crida quan es clica el botó de tornar un cop has guanyat o perdut
    /// </summary>
    public void OnBotoVictoria()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.LoadLevel(ScenesManager.GetScene(ScenesManager.Scene.MapaNivells));
        }
    }

    /// <summary>
    /// Funció per indicar que s'ha acabat la partida per a tots els jugadors
    /// </summary>
    /// <param name="estat">si es true es guanya la partida, si es false es perd</param>
    public void FiPartida(bool estat)
    {
        PhotonNetwork.MinimalTimeScaleToDispatchInFixedUpdate = 0;
        Time.timeScale = 0;
        if (estat)
        {
            victory.SetActive(true);
        }
        else
        {
            gameOver.SetActive(true);
        }
    }

    /// <summary>
    /// Funció per transformar la visió de la càmera a blanc i negre
    /// </summary>
    private void SetBlancNegre()
    {
        PostProcessVolume postpo = FindObjectOfType<PostProcessVolume>();
        ColorGrading color;
        if (postpo.profile.TryGetSettings<ColorGrading>(out color))
        {
            color.saturation.value = -100;
        }
    }

    public int GetVides() { return videsPartida;}
    public int GetSpawnpointLength() { return spawnPoints.Length; }
}
