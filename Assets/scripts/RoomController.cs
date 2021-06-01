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
    
    public int currentScene;
    public int multiplayerScene;

    public PhotonPlayer photonPlayer;

    //Player[] photonPlayers;
    public Dictionary<string, Player> Rols;

    public Text text;
    public ButtonRolController constructor;
    public ButtonRolController defensor;
    public ButtonRolController recollector;



    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        PhotonNetwork.AutomaticallySyncScene = false;
        AdjudicarRols();
    }

    void AdjudicarRols()
    {
        Rols = new Dictionary<string, Player>();
        foreach (Player p in PhotonNetwork.PlayerList)
        {

        }
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
        if(PhotonNetwork.IsMasterClient && PV.IsMine)
        {
            PV.RPC("RPC_LoadGameScene", RpcTarget.All);
            PhotonNetwork.LoadLevel(ScenesManager.GetScene(ScenesManager.Scene.Constructor));
        }
    }
    
    [PunRPC]
    private void RPC_LoadGameScene()
    {
        Debug.Log("Nou nivell");
        PhotonNetwork.LoadLevel(ScenesManager.GetScene(ScenesManager.Scene.Prototip));
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if (currentScene == multiplayerScene)
        {
            CreatePlayer();
        }
    }

    

    void CreatePlayer()
    {
        GameObject pp = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), transform.position,
            Quaternion.identity, 0);
        photonPlayer = pp.GetComponent<PhotonPlayer>();
        
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LoadLevel(ScenesManager.GetScene(ScenesManager.Scene.MenuMultijugador));
    }

    public void OnSelectRolButton(string rol)
    {
        ButtonRolController botoVell = modificarBoto(photonPlayer.Rol); //fer interactable per la resta
        botoVell.boto.interactable = true;
        PV.RPC("RPC_setInteractableButton", RpcTarget.All, photonPlayer.Rol, true);


        ButtonRolController botoNou = modificarBoto(rol); //no interactable per la resta
        PV.RPC("RPC_setInteractableButton", RpcTarget.All, rol, false);
        text.text = rol;
        photonPlayer.Rol = rol;


        //activarBoto(rol, false);
        
    }
    [PunRPC]
    private void RPC_setInteractableButton(string rol, bool estat)
    {
        modificarBoto(rol).boto.interactable = estat;
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
