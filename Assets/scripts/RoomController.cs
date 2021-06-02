using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class RoomController : MonoBehaviourPunCallbacks, IInRoomCallbacks {

    public static RoomController room;
    private PhotonView PV;
    
    public string currentScene;
    public string multiplayerScene;

    public PhotonPlayer photonPlayer;

    public Text text;
    public ButtonRolController constructor;
    public ButtonRolController defensor;
    public ButtonRolController recollector;



    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        PhotonNetwork.AutomaticallySyncScene = false;
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

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("We r now in a room");
        /*photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        myNumberInRoom = playersInRoom;
        PhotonNetwork.Nickname = myNumberInRoom.ToString();*/
    }

    public void OnTutorialButtonClicked()
    {
        if(constructor.isSelected && defensor.isSelected && recollector.isSelected)
        {
            if (PhotonNetwork.IsMasterClient && PV.IsMine)
            {
                PV.RPC("RPC_LoadGameScene", RpcTarget.All);
                RPC_LoadGameScene();
            }
        } else
        {
            Debug.Log("NO ESTAN TOTS ELS ROLS SELECCIONATS");
        }
        
    }
    
    [PunRPC]
    private void RPC_LoadGameScene()
    {
        Debug.Log("Nou nivell pel rol" + photonPlayer.Rol);
        if (photonPlayer.Rol.Equals(PhotonPlayer.CONSTRUCTOR))
        {
            PhotonNetwork.LoadLevel(ScenesManager.GetScene(ScenesManager.Scene.Constructor));
        }
        else
        {
            PhotonNetwork.LoadLevel(ScenesManager.GetScene(ScenesManager.Scene.Prototip));
        }
    }

    /*void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.name;
        Debug.Log("current scene = " + currentScene);
        if (currentScene.Equals(ScenesManager.GetScene(ScenesManager.Scene.MapaNivells)))
        {
            CreatePlayer();
        }
    }*/

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.name;
        Debug.Log("current scene = " + currentScene);
        if (currentScene.Equals(ScenesManager.GetScene(ScenesManager.Scene.Prototip)))
        {
            photonPlayer.Instantiate();
        }
    }

    void CreatePlayer()
    {
        //GameObject pp = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), transform.position,
            //Quaternion.identity, 0);
        photonPlayer = this.GetComponent<PhotonPlayer>();
        
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LoadLevel(ScenesManager.GetScene(ScenesManager.Scene.MenuMultijugador));
    }

    /// <summary>
    /// Quan un jugador selecciona un boto dels rols, s'ha de guardar el rol seleccionat i fer que la resta de jugadors no
    /// puguin seleccionar el seu rol. Si torna a clica en el rol que ja ha seleccionat, es des-seleccionarà
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
            text.text = "";
        }
        //Seleccionar
        //no interactable per la resta de jugador i en el jugador propi sí.
        else
        {
            ButtonRolController botoNou = modificarBoto(rol);
            PV.RPC("RPC_setInteractableButton", RpcTarget.All, rol, false);
            botoNou.boto.interactable = true;
            botoNou.isSelected = true;
            text.text = rol;
            photonPlayer.Rol = rol;
        }


        //activarBoto(rol, false);
        
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
        ButtonRolController brc =  modificarBoto(rol);
        brc.boto.interactable = estat;
        brc.isSelected = !estat;
    }


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
}
