using UnityEngine;

[RequireComponent(typeof(Rigidbody))] // Убедимся, что на объекте всегда есть Rigidbody
public class AircraftController : MonoBehaviour
{
    [Header("Настройки движения")]
    public float forwardSpeed = 25f;      // Скорость движения вперед/назад
    public float strafeSpeed = 15f;       // Скорость движения вбок (стрейф)
    public float verticalSpeed = 10f;     // Скорость подъема/спуска
    public float maxSpeed = 30f;          // Максимальная общая скорость

    [Header("Настройки вращения")]
    public float rotationSpeed = 100f;    // Скорость вращения мышью

    private Rigidbody rb;
    private Vector3 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Важные настройки для управления без гравитации
        rb.useGravity = false;      // Отключаем встроенную гравитацию
        rb.drag = 1f;               // Добавляем сопротивление, чтобы корабль останавливался
        rb.angularDrag = 2f;        // Сопротивление вращению для более плавного контроля

        // Замораживаем вращение по осям X и Z, чтобы корабль не кувыркался
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Прячем курсор и блокируем его в центре экрана
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // --- Сбор ввода от игрока ---

        // Вращение мышью по горизонтали (вокруг оси Y)
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, mouseX, 0);

        // Движение вперед/назад (W/S) и вбок (A/D)
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D
        float moveVertical = Input.GetAxis("Vertical");     // W/S

        // Вертикальное движение (Пробел/Ctrl)
        float moveUp = 0f;
        if (Input.GetKey(KeyCode.Space))
        {
            moveUp = 1f; // Движемся вверх
        }
        else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            moveUp = -1f; // Движемся вниз
        }

        // Собираем все векторы ввода в один
        moveInput = new Vector3(-moveHorizontal * strafeSpeed, moveUp * verticalSpeed, -moveVertical * forwardSpeed);
    }

    void FixedUpdate()
    {
        // --- Применение физики ---

        // Преобразуем локальный вектор ввода в глобальный и применяем силу
        // Это заставит корабль двигаться в ту сторону, куда он повернут
        Vector3 localMove = transform.TransformDirection(moveInput);
        rb.AddForce(localMove, ForceMode.Force);

        // Ограничение максимальной скорости
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}