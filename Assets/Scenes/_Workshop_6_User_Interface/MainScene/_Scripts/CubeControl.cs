using UnityEngine;

public class CubeControl : MonoBehaviour
{
    private Rigidbody _rb;

    public GameObject _go;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }


    public void StartButton()
    {
        _rb.useGravity = true;
        _go.SetActive(true);
    }


    public void StopButton()
    {
        _rb.useGravity = false;
    }


    public void ChangeColorButton()
    {
        _rb.gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
}