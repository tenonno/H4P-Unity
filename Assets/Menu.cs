using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public InputField inputField;
    public Button runButton;
    public Button closeButton;

    public Button enchantBook;

    public GameObject menuGroup1;
    public GameObject menuGroup2;


    public static void Swap(ref GameObject lhs, ref GameObject rhs)
    {
        var temp = lhs.GetComponent<CanvasGroup>().alpha;
        lhs.GetComponent<CanvasGroup>().alpha = rhs.GetComponent<CanvasGroup>().alpha;
        rhs.GetComponent<CanvasGroup>().alpha = temp;
    }

    void Start()
    {
        closeButton.onClick.AddListener(Toggle);
        enchantBook.onClick.AddListener(Toggle);


    }

    private bool a = true;

    private void OnGUI()
    {




    }


    void Toggle()
    {


        menuGroup1.GetComponent<CanvasGroup>().alpha = a ? 1f : 0f;
        menuGroup2.GetComponent<CanvasGroup>().alpha = a ? 0f : 1f;


        menuGroup1.GetComponent<CanvasGroup>().blocksRaycasts = a;
        menuGroup2.GetComponent<CanvasGroup>().blocksRaycasts = !a;


        a = !a;


    }

    // Update is called once per frame
    void Update()
    {

    }
}
