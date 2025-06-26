using TMPro;
using UnityEngine;

public class TitleRainbow : MonoBehaviour
{
    
    public float speed = 1.0f;
    public TextMeshProUGUI titleText;


    void Start()
    {
    }

    
    void Update()
    {
        titleText.color = Color.HSVToRGB(Mathf.PingPong(Time.time * speed, 1), 1, 1);
    }
}
