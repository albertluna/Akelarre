using UnityEngine;
using System.Collections;
using Photon.Pun;

public class Tutorial : MonoBehaviour
{
    public atacController defensor;
    public RecollectorController recolector;
    public ConstructorController constructor;

    [SerializeField]
    private GameObject tutorialDefensor;
    [SerializeField]
    private GameObject tutorialRecolector;
    [SerializeField]
    private GameObject tutorialConstructor;

    private PlayerTutorial player;
    GameObject HudTutorial;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 0f;
        constructor = FindObjectOfType<ConstructorController>();
        defensor = FindObjectOfType<atacController>();
        recolector = FindObjectOfType<RecollectorController>();
        if (constructor.PV.IsMine)
        {
            HudTutorial = Instantiate(tutorialConstructor);
            player = constructor.gameObject.AddComponent<PlayerTutorial>();
            player.setVariables(this);

        }
        else if (defensor.PV.IsMine)
        {
            HudTutorial = Instantiate(tutorialDefensor);
            player = defensor.gameObject.AddComponent<PlayerTutorial>();
            player.setVariables(this);
        }
        else if (recolector.PV.IsMine)
        {
            HudTutorial = Instantiate(tutorialRecolector);
            player = recolector.gameObject.AddComponent<PlayerTutorial>();
            player.setVariables(this);
        }
        HudTutorial.GetComponent<HUD_tutorial>().setTutorial(this);
    }
    /// <summary>
    /// Funcio per tancar els tutorials, cridat de playr
    /// </summary>
    public void Comencar()
    {
        player.Comencar();
    }

    public void EsborrarHUDTutorial()
    {
        Destroy(HudTutorial);
        Destroy(this.gameObject);
    }
}
