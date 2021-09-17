using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuPausa : MonoBehaviour
{
    private PhotonView photonView;
    private RolController controller;

    #region variables
    [Header("Menú Pausa")]
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject comprovar;
    [SerializeField]
    private GameObject configuracio;
    [SerializeField]
    private GameObject[] HudPartida;
    [SerializeField]
    private AudioMixer mixer;
    
    #endregion

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        controller = GetComponent<RolController>();
    }

    /// <summary>
    /// Funció que es crida quan es clica el botó de la pausa i es para el temps
    /// i s'obre el menú de pausa
    /// </summary>
    public void OnBotoPressed()
    {
        photonView.RPC("RPC_PararPartida", RpcTarget.All);
        menu.SetActive(true);
        AmagarHUD(false);
    }

    /// <summary>
    /// Funció que es crida quan es clica el botó de reprendre la partida
    /// </summary>
    public void OnReanudarPressed()
    {
        menu.SetActive(false);
        AmagarHUD(true);
        photonView.RPC("RPC_ReprendrePartida", RpcTarget.All);
    }

    public void AmagarHUD(bool estat) { foreach (GameObject go in HudPartida) go.SetActive(estat); }

    /// <summary>
    /// Funció per obrir o tancar el menú de comprovació de tancar la partida
    /// </summary>
    /// <param name="estat">true si s'obre el menú comprovació, false si es tanca</param>
    public void OnComprovar(bool estat)
    {
        menu.SetActive(!estat);
        comprovar.SetActive(estat);
    }

    public void onObrirConfig(bool estat)
    {
        menu.SetActive(!estat);
        configuracio.SetActive(estat);
    }

    /// <summary>
    /// Funció per sortir del joc i tornar al mapa
    /// </summary>
    public void OnSortirPartida()
    {
        if (PhotonNetwork.MasterClient.IsLocal)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.LoadLevel(ScenesManager.GetScene(ScenesManager.Scene.MapaNivells));
            Time.timeScale = 1f;
        }
    }

    /// <summary>
    /// Funció per posar la pausa
    /// </summary>
    [PunRPC]
    private void RPC_PararPartida()
    {
        Time.timeScale = 0f;
        
    }

    /// <summary>
    /// Funció per retornar a la parita
    /// </summary>
    [PunRPC]
    private void RPC_ReprendrePartida()
    {
        
        Time.timeScale = 1f;
    }

    public void onVolumUpdated(Slider slider)
    {
        mixer.SetFloat(slider.gameObject.name, slider.value);
    }
}
