using System.Collections;
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

    [SerializeField]
    private GameObject GameOver;
    [SerializeField]
    private GameObject Victory;

    //Variable per borrar la llista de la pocio
    [SerializeField]
    private GameObject HUD;

    [Header("Posició d'origen de cada rol")]
    public Transform[] spawnPoints;

    [Header("Referència a la recol·lecció")]
    [SerializeField]
    private bool colleccionablesInvisibles;

    [Header("Referència a l'atac")]
    public int videsPartida;
    /// Variable per indicar si el defensor pot veure les boles d'atac
    public bool atacVisible;
    public float initialOffsetTimer;
    public float minimTime;
    public float maximTime;
    [Range(0.03f, 0.20f)]
    public float velocitatBoles;

    /// <summary>
    /// Variable per indicar si el rol veu en blanc i negre
    /// </summary>
    [Header("Referència a la visibilitat")]
    public bool grisRecollector;
    public bool grisConstructor;

    /// <summary>
    /// GameObjects amb tota les llistes de col·leccionables i atacs
    /// </summary>
    [Header("Llistes inicials")]
    public GameObject llistesRecollector;
    public GameObject llistaCreadorsAtac;
    public GameObject llistaBoles;
    #endregion variables

    void Awake()
    {
        GameOver.SetActive(false);
        Victory.SetActive(false);
        Time.timeScale = 1;
        PhotonNetwork.MinimalTimeScaleToDispatchInFixedUpdate = 0;
    }

    public int getSpawnpointLength() { return spawnPoints.Length; }

    /// <summary>
    /// Funció per obtenir la referència de tots els jugadors
    /// i actualitzar la seva visibilitat a la del nivell
    /// </summary>
    public void getRols()
    {
        constructor = FindObjectOfType<ConstructorController>();
        defensor = FindObjectOfType<Defensor>();
        recollector = FindObjectOfType<RecollectorController>();
        /*Debug.LogError("Tenim constructor? " + constructor.name);
        Debug.LogError("Defensor? " + defensor.name);
        Debug.LogError("Recol? " + recollector.name);*/

        //s'indica si el defensor pot veure les boles o no
        if (defensor != null) defensor.setVisibility(atacVisible);
        if (recollector != null) recollector.isInvisible = colleccionablesInvisibles;
        //s'indica si els rols han de veure en blanc i negre
        if (recollector != null) if(grisRecollector && recollector.PV.IsMine) setBiN();
        if (constructor != null) if(grisConstructor && constructor.PV.IsMine) setBiN();
        //Es destrueix la llista de la pocio pels no-constructors
        if (constructor == null || !constructor.PV.IsMine) if(HUD!=null) Destroy(HUD);
    }

    /// <summary>
    /// Funció que es crida quan es clica el botó de tornar un cop has guanyat
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
            Victory.SetActive(true);
        }
        else
        {
            GameOver.SetActive(true);
        }
    }

    /// <summary>
    /// Funcio per transformar la visio de la camera a blanc i negre
    /// </summary>
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
