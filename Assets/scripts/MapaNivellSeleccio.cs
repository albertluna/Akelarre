using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.IO;

public class MapaNivellSeleccio : MonoBehaviourPunCallbacks, IInRoomCallbacks {

    public static MapaNivellSeleccio room;
    private PhotonView PV;

    public bool isGameLoaded;
    public int currentScene;
    public int multiplayerScene;

    Player[] photonPlayers;
    public int playersInRoom;
    public int muNumberInGame;

    public int playersInGame;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        PhotonNetwork.AutomaticallySyncScene = false;
    }

    private void Awake()
    {
        if(MapaNivellSeleccio.room == null)
        {
            MapaNivellSeleccio.room = this;
        } else
        {
            if(MapaNivellSeleccio.room != this)
            {
                Destroy(MapaNivellSeleccio.room.gameObject);
                MapaNivellSeleccio.room = this;
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

    void StartGame() {
        
        if (PhotonNetwork.IsMasterClient)
        {

            
        }
        else
        {
            
        }
    }

    public void OnTutorialButtonClicked()
    {
        if(PhotonNetwork.IsMasterClient && PV.IsMine)
        {
            PV.RPC("RPC_LoadGameScene", RpcTarget.All);
            PhotonNetwork.LoadLevel(ScenesManager.GetScene(ScenesManager.Scene.Constructor));
        }
    }
    
    [RunRPC]
    private void RPC_LoadGameScene()
    {
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
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), transform.position,
            Quaternion.identity, 0);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {

    }
}
