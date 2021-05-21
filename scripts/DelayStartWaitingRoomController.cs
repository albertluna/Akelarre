using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DelayStartWaitingRoomController : MonoBehaviourPunCallbacks
{

    private PhotonView myPhotonView;

    [SerializeField]
    private int multiplayerSceneIndex;

    [SerializeField]
    private int menuSceneIndex;

    private int playerCount;
    private int roomSize;

    [SerializeField]
    private int minPlayersToStart;

    [SerializeField]
    private TextMeshProUGUI roomCountDisplay;

    [SerializeField]
    private TextMeshProUGUI timerToStartDisplay;

    private bool readyToCountDown;
    private bool readyToStart;
    private bool startingGame;

    private float timerToStartGame;
    private float notFullGameTimer;
    private float fullGameTimer;

    [SerializeField]
    private float maxWaitTime;

    [SerializeField]
    private float maxFullGameWaitTime;


    // Start is called before the first frame update
    void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        fullGameTimer = maxFullGameWaitTime;
        notFullGameTimer = maxWaitTime;
        timerToStartGame = maxWaitTime;

        playerCountUpdate();

    }

    private void playerCountUpdate()
    {
        playerCount = PhotonNetwork.PlayerList.Length;
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        roomCountDisplay.text = playerCount + "/" + roomSize;
        if(playerCount == roomSize)
        {
            readyToStart = true;
        }
        else if(playerCount >= minPlayersToStart)
        {
            readyToCountDown = true;
        } else
        {
            readyToCountDown = false;
            readyToStart = false;
        }

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        playerCountUpdate();

        if(PhotonNetwork.IsMasterClient)
        {
            myPhotonView.RPC("RPC_SendTimer", RpcTarget.Others, timerToStartGame);
        }
        base.OnPlayerEnteredRoom(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        playerCountUpdate();
        base.OnPlayerLeftRoom(otherPlayer);
    }

    [PunRPC]
    private void RPC_SendTimer(float timeIn)
    {
        timerToStartGame = timeIn;
        notFullGameTimer = timeIn;
        if(timeIn < fullGameTimer)
        {
            fullGameTimer = timeIn;
        }
    }

    // Update is called once per frame
    void Update()
    {
        waitingForMorePlayers();
    }

    private void waitingForMorePlayers()
    {
        if(playerCount < minPlayersToStart)
        {
            ResetTimer();
        }
        if(readyToStart)
        {
            fullGameTimer -= Time.deltaTime;
            timerToStartGame = fullGameTimer;
        }
        else if(readyToCountDown)
        {
            fullGameTimer -= Time.deltaTime;
            timerToStartGame = notFullGameTimer;
        }
        string tempTimer = string.Format("{0:00}", timerToStartGame);
        timerToStartDisplay.text = tempTimer;

        if(timerToStartGame <= 0f)
        {
            if (startingGame) return;
            StartGame();
        }
    }

    private void ResetTimer()
    {
        timerToStartGame = maxWaitTime;
        notFullGameTimer = maxWaitTime;
        fullGameTimer = maxFullGameWaitTime;
    }

    public void StartGame()
    {
        startingGame = true;
        if (!PhotonNetwork.IsMasterClient) return;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(multiplayerSceneIndex);
    }

    public void DelayCancel()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(menuSceneIndex);
    }
}
