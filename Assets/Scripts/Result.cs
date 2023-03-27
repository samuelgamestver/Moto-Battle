using UnityEngine;
using TMPro;

public class Result : MonoBehaviour
{
    [SerializeField] GameObject window;

    [SerializeField] TextMeshProUGUI ResultTxt;
    

    void Start()
    { 
        Player.StopGame += OpenWindow;
    }

    void OnDisable()
    {
        Player.StopGame -= OpenWindow;
    }

    // Update is called once per frame
    void OpenWindow(bool wins)
    {
        window.SetActive(true);

        Time.timeScale = 0;

        if (wins)
        {
            ResultTxt.color = Color.green;
            ResultTxt.text = "победа";
        }
        else
        {
            ResultTxt.color = Color.red;
            ResultTxt.text = "поражение";
        }
        
    }
}
