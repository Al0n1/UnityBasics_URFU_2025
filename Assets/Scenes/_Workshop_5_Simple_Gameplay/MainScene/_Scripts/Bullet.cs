using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;      // Скорость полета пули
    public float lifetime = 3.0f;  // Время жизни пули в секундах

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Задаем пуле начальную скорость в направлении "вперед"
        rb.velocity = transform.forward * speed * -1;

        // Уничтожаем объект пули через 'lifetime' секунд, чтобы не засорять сцену
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Здесь можно добавить логику нанесения урона
        // Debug.Log("Пуля попала в: " + collision.gameObject.name);

        // Уничтожаем пулю при попадании во что-либо
        Destroy(gameObject);
    }
}