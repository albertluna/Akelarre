using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MenuPausa : MonoBehaviour
{
    private PhotonView PV;

    #region MenuPausa
    [Header("Menú Pausa")]
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject comprovar;
    [SerializeField]
    private GameObject[] HudPartida;
    //boolea per detectar qui ha obert el menu
    private bool jo;
    #endregion

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        jo = false;
    }
    #region lògica Menú Pausa
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
        if (PhotonNetwork.MasterClient.IsLocal)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.LoadLevel(ScenesManager.GetScene(ScenesManager.Scene.MapaNivells));
            Time.timeScale = 1f;
        }
    }

    [PunRPC]
    private void RPC_ObrirMenu()
    {
        Time.timeScale = 0f;
        menu.SetActive(true);
        foreach (GameObject go in HudPartida) go.SetActive(false);
    }

    [PunRPC]
    private void RPC_Reanudar()
    {
        menu.SetActive(false);
        foreach (GameObject go in HudPartida) go.SetActive(true);

        Time.timeScale = 1f;
    }
    #endregion
}
