using UnityEngine;
using System.Collections;
using Photon.Pun;

public class Tutorial : MonoBehaviour
{
    public AtacController defensor;
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
        defensor = FindObjectOfType<AtacController>();
        recolector = FindObjectOfType<RecollectorController>();
        if (constructor.photonView.IsMine)
        {
            HudTutorial = Instantiate(tutorialConstructor);
            player = constructor.gameObject.AddComponent<PlayerTutorial>();
            player.setVariables(this);

        }
        else if (defensor.photonView.IsMine)
        {
            HudTutorial = Instantiate(tutorialDefensor);
            player = defensor.gameObject.AddComponent<PlayerTutorial>();
            player.setVariables(this);
        }
        else if (recolector.photonView.IsMine)
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
