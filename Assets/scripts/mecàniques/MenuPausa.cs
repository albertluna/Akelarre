using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MenuPausa : MonoBehaviour
{
    public GameObject boto;
    public GameObject menu;
    public GameObject comprovar;

    private PhotonView PV;
    //boolea per detectar qui ha obert el menu
    private bool jo;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        jo = false;
    }

    public void OnBotoPressed()
    {
        PV.RPC("RPC_ObrirMenu", RpcTarget.All);
        jo = true;
    }

    public void OnReanudarPressed()
    {
        if (jo)
            PV.RPC("RPC_Reanudar", RpcTarget.All);
        jo = false;
    }

    public void OnComprovar(bool estat)
    {
        menu.SetActive(!estat);
        comprovar.SetActive(estat);
    }

    public void OnSortirPartida()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.LoadLevel(ScenesManager.GetScene(ScenesManager.Scene.MapaNivells));
    }

    [PunRPC]
    private void RPC_ObrirMenu()
    {
        Time.timeScale = 0f;
        boto.SetActive(false);
        menu.SetActive(true);
    }

    [PunRPC]
    private void RPC_Reanudar()
    {
        boto.SetActive(true);
        menu.SetActive(false);
        Time.timeScale = 1f;
    }

}
