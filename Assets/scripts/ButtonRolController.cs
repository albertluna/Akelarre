using UnityEngine;
using UnityEngine.UI;


public class ButtonRolController : MonoBehaviour
{
    public Button boto;
    public bool isSelected;
    private string nom;

    // Start is called before the first frame update
    void Start()
    {
        boto = GetComponent<Button>();
        nom = GetComponentInChildren<Text>().text;
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

    public string getNom() { return nom; }
}
