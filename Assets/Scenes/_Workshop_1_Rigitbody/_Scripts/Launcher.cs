using UnityEngine;

public class Launcher : MonoBehaviour
{
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true; 
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            _rb.isKinematic = false;
        }
    }
}
