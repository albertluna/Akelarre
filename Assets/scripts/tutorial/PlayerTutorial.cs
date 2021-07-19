using UnityEngine;
using System.Collections;
using Photon.Pun;

public class PlayerTutorial : MonoBehaviour
{
    PhotonView PV;
    Tutorial tutorial;

    public void setVariables(Tutorial tutorial)
    {
        PV = GetComponent<PhotonView>();
        this.tutorial = tutorial;
    }

    public void Comencar()
    {
        PV.RPC("RPC_Comencar", RpcTarget.All);
    }

    [PunRPC]
    private void RPC_Comencar()
    {
        Time.timeScale = 1f;
        tutorial.EsborrarHUDTutorial();
        Destroy(this);

    }


}
