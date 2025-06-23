using UnityEngine;

public class Brake : MonoBehaviour
{
    private Rigidbody _rb;
    private float _initialDrag;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _initialDrag = _rb.drag;
    }

    void Update()
    {
        // При зажатии 'B', резко увеличиваем сопротивление
        if(Input.GetKeyDown(KeyCode.B)) 
        {
            _rb.drag = 10f;
        }
        // Когда отпускаем, возвращаем как было
        if(Input.GetKeyUp(KeyCode.B))
        {
            _rb.drag = _initialDrag;
        }
    }
}
