using UnityEngine;
using UnityEngine.UI;


public class ButtonRolController : MonoBehaviour
{
    public string rol;
    public Button boto;
    public bool isSelected;
    public string nom;

    // Start is called before the first frame update
    void Start()
    {
        boto = GetComponent<Button>();
        nom = GetComponentInChildren<Text>().text;
    }

    public void seleccionarBoto(bool estat)
    {
        boto.interactable = estat;
        isSelected = !estat;
    }
}
