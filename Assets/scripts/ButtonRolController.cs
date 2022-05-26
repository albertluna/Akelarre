using UnityEngine;
using UnityEngine.UI;


public class ButtonRolController : MonoBehaviour
{
    public Button boto;
    public bool isSelected;
    private string nom;
    [SerializeField]
    private Color seleccionat;
    [SerializeField]
    private Color noSeleccionat;
    
    private ColorBlock original;

    // Start is called before the first frame update
    void Start()
    {
        boto = GetComponent<Button>();
        nom = GetComponentInChildren<Text>().text;
        original = boto.colors;
    }

    /// <summary>
    /// Botó per modificar la interactibilitat del botó
    /// </summary>
    /// <param name="estat">true es selecciona, false es des-selecciona</param>
    public void seleccionarBoto(bool estat)
    {
        boto.interactable = estat;
        isSelected = !estat;
    }

    public void mantenirBotoSeleccionat()
    {
        ColorBlock cb = boto.colors;
        cb.normalColor = seleccionat;
        cb.highlightedColor = seleccionat;
        cb.pressedColor = seleccionat;
        cb.selectedColor = seleccionat;
        //colros.highlightedColor = new Color32(255, 100, 100, 255);
        boto.colors = cb;
    }

    public void desSeleccionarBoto()
    {
        boto.colors = original;
    }

    public string getNom() { return nom; }
}
