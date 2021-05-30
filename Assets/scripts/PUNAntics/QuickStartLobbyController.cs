using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class QuickStartLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject quickStartButton; //Boto per entrar en una partida
    [SerializeField]
    private GameObject quickCancelButton; //Boto per cancelar l'entrada a una partida
    [SerializeField]
    private int RoomSize; //Mida maxima d'una partida

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        quickStartButton.SetActive(true);
        base.OnConnectedToMaster();
    }

    public void QuickStart() //Ligat al boto de quickstart
    {
        quickStartButton.SetActive(false);
        quickCancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Quick start");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a room");
        CreateRoom();
        base.OnJoinRandomFailed(returnCode, message);
    }

    void CreateRoom()
    {
        Debug.Log("creant sala");
        int RandomNumber = Random.Range(0, 1000); //Creant id sala
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)RoomSize };
        PhotonNetwork.CreateRoom("Room" + RandomNumber, roomOptions);
        Debug.Log(RandomNumber);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Ha fallat crear la sala, creant una nova...");
        CreateRoom();
        base.OnCreateRoomFailed(returnCode, message);
    }

    public void QuickCancel() //Lligat al boto de cancelar
    {
        quickCancelButton.SetActive(false);
        quickStartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
