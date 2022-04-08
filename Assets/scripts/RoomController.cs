using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Audio;

public class RoomController : MonoBehaviourPunCallbacks, IInRoomCallbacks {

    private static RoomController room;
    private PhotonView PV;
    
    private string currentScene;
    private PhotonPlayer photonPlayer;

    //Nom de la persona seleccionada
    public Text text;

    //Els tres botons dels rols
    [SerializeField]
    private ButtonRolController constructor;
    [SerializeField]
    private ButtonRolController defensor;
    [SerializeField]
    private ButtonRolController recollector;
    [SerializeField]
    private GameObject PanelMapa;
    [SerializeField]
    private GameObject PanelCinematica;
    [SerializeField]
    private GameObject PanelConfiguracio;
    [SerializeField]
    private AudioMixer mixer;
    [SerializeField]
    private GameObject PanellDificultat;
    [SerializeField]
    private Slider dificultat;

    private int nivellSeleccionat;

    private bool esDinsConfig;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        PhotonNetwork.AutomaticallySyncScene = true;
        photonPlayer = this.GetComponent<PhotonPlayer>();
    }

    private void Awake()
    {
        if(RoomController.room == null)
        {
            RoomController.room = this;
        } else
        {
            if(RoomController.room != this)
            {
                Destroy(RoomController.room.gameObject);
                RoomController.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    /*public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        /*photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        myNumberInRoom = playersInRoom;
        PhotonNetwork.Nickname = myNumberInRoom.ToString();
    }*/

    #region Obrir nivells

    /// <summary>
    /// Funció que es crida quan es clica un dels botons de nivells i es selecciona quin nivell s'obre
    /// </summary>
    /// <param name="nivell">Numero de nivell de 0 a 3</param>
    public void OnNivellButtonClicked(int nivell)
    {
        if(constructor.isSelected && defensor.isSelected && recollector.isSelected)
        {
            if (PhotonNetwork.IsMasterClient && PV.IsMine)
            {
                nivellSeleccionat = nivell;
                PanelMapa.SetActive(false);
                PanellDificultat.SetActive(true);
                
            }
        } else
        {
            text.text = "No estan tots els personatges seleccionats";
            Debug.Log("NO ESTAN TOTS ELS ROLS SELECCIONATS");
        }
        
    }

    public void OnComencarPartida()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        photonPlayer.SetDificultat((int)dificultat.value);
        switch (nivellSeleccionat)
        {
            case 0:
                PV.RPC("RPC_OnNivell0", RpcTarget.All);
                break;
            case 1:
                OnNivell1();
                break;
            case 2:
                OnNivell2();
                break;
            case 3:
                OnNivell3();
                break;
        }
    }

    public void OnCancellarPartida()
    {
        PanellDificultat.SetActive(false);
        PanelMapa.SetActive(true);
    }

    /// <summary>
    /// Activació del nivell 0
    /// </summary>
    [PunRPC]
    private void RPC_OnNivell0()
    {
        //Activació de la cinemàtica inicial
        PanelMapa.SetActive(false);
        PanelConfiguracio.SetActive(false);
        PanelCinematica.SetActive(true);
        AudioSource explicacio = PanelCinematica.GetComponent<AudioSource>();
        if (!explicacio.isPlaying)
        {
            explicacio.Play();
            PanelCinematica.GetComponentInChildren<Animation>().Play("ExplicacioHistoriaInicial");

            StartCoroutine(EsperarCinematica(PanelCinematica.GetComponent<AudioSource>()));

        }
        else if(PhotonNetwork.IsMasterClient)
        {
            StopAllCoroutines();
            PhotonNetwork.LoadLevel(ScenesManager.GetScene(ScenesManager.Scene.Nivell0));
        }
    }

    /// <summary>
    /// Funció per mostrar la cinemàtica i canviar automàticament d'escena quan acabi l'àudio
    /// </summary>
    /// <param name="audio">Fitxar d'àudio de la cinemàtica</param>
    /// <returns>Temps d'espera</returns>
    IEnumerator EsperarCinematica(AudioSource audio)
    {
        yield return new WaitWhile(() => audio.isPlaying == true);
        if(PhotonNetwork.IsMasterClient) PhotonNetwork.LoadLevel(ScenesManager.GetScene(ScenesManager.Scene.Nivell0));
    }

    /// <summary>
    /// Activació del nivell 1
    /// </summary>
    private void OnNivell1()
    {
        PhotonNetwork.LoadLevel(ScenesManager.GetScene(ScenesManager.Scene.Nivell1));
    }

    /// <summary>
    /// Activació del nivell 1
    /// </summary>
    private void OnNivell2()
    {
        PhotonNetwork.LoadLevel(ScenesManager.GetScene(ScenesManager.Scene.Nivell2));
    }

    /// <summary>
    /// Activació del nivell 1
    /// </summary>
    private void OnNivell3()
    {
        PhotonNetwork.LoadLevel(ScenesManager.GetScene(ScenesManager.Scene.Nivell3));
    }
    #endregion

    /// <summary>
    /// Funció per activar els nous elements de l'escena un cop s'ha carregat la nova escena
    /// </summary>
    /// <param name="scene">Nova escena</param>
    /// <param name="mode"></param>
    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.name;
        Debug.Log("current scene = " + currentScene);
        if (currentScene.Equals(ScenesManager.GetScene(ScenesManager.Scene.Nivell0))
            || currentScene.Equals(ScenesManager.GetScene(ScenesManager.Scene.Nivell1))
            || currentScene.Equals(ScenesManager.GetScene(ScenesManager.Scene.Nivell2))
            || currentScene.Equals(ScenesManager.GetScene(ScenesManager.Scene.Nivell3)))
        {
            photonPlayer.Instantiate();
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Gestió de l'abandonament de la sala per part d'un jugador
    /// </summary>
    /// <param name="otherPlayer"></param>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LoadLevel(ScenesManager.GetScene(ScenesManager.Scene.MenuMultijugador));
    }

    #region Rols

    /// <summary>
    /// Quan un jugador selecciona un boto dels rols, s'ha de guardar el rol seleccionat i fer que la resta de jugadors no
    /// puguin seleccionar el seu rol. Si torna a clicar en el rol que ja ha seleccionat, es des-seleccionarà
    /// </summary>
    /// <param name="rol"></param>
    public void OnSelectRolButton(string rol)
    {
        ButtonRolController botoVell = modificarBoto(photonPlayer.Rol); //fer interactable per la resta
        if (botoVell != null)
        {
            botoVell.boto.interactable = true;
            PV.RPC("RPC_setInteractableButton", RpcTarget.All, photonPlayer.Rol, true);
        }

        //Des-seleccionar
        if (rol.Equals(photonPlayer.Rol))
        {
            PV.RPC("RPC_setInteractableButton", RpcTarget.All, rol, true);
            photonPlayer.Rol = null;
            botoVell.boto.interactable = false;
            botoVell.boto.interactable = true;
            text.text = "Selecciona personatge";
        }
        //Seleccionar
        //no interactable per la resta de jugador i en el jugador propi sí.
        else
        {
            ButtonRolController botoNou = modificarBoto(rol);
            PV.RPC("RPC_setInteractableButton", RpcTarget.Others, rol, false);
            botoNou.seleccionarBoto(true);
            botoNou.isSelected = true;
            text.text = botoNou.getNom();
            photonPlayer.Rol = rol;
        }        
    }

    /// <summary>
    /// Funcio que setteja els botons del rol interactables segons lestat
    /// i indica si estan seleccionats
    /// </summary>
    /// <param name="rol"></param>
    /// <param name="estat"></param>
    [PunRPC]
    private void RPC_setInteractableButton(string rol, bool estat)
    {
        modificarBoto(rol).seleccionarBoto(estat);
    }

    /// <summary>
    /// Funcio que retorna el boto en funcio del string rol
    /// </summary>
    /// <param name="rol">nom del rol</param>
    /// <returns></returns>
    private ButtonRolController modificarBoto(string rol)
    {
        switch (rol)
        {
            case PhotonPlayer.CONSTRUCTOR:
                return constructor;
            case PhotonPlayer.DEFENSOR:
                return defensor;
            case PhotonPlayer.RECOLLECTOR:
                return recollector;
        }
        return null;
    }

    #endregion

    /// <summary>
    /// Es crida quan un jugador clica el botó de sortir de la sala
    /// </summary>
    public void OnTancar()
    {
        PhotonNetwork.LeaveRoom();
        Destroy(RoomController.room.gameObject);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LoadLevel(ScenesManager.GetScene(ScenesManager.Scene.MenuMultijugador));
    }

    public void OnConfig()
    {
        esDinsConfig = !esDinsConfig;
        PanelMapa.SetActive(!esDinsConfig);
        PanelConfiguracio.SetActive(esDinsConfig);

    }

    public void onVolumUpdated(Slider slider)
    {
        mixer.SetFloat(slider.gameObject.name, slider.value);
    }
}
