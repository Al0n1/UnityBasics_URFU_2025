using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [Header("Левая пушка")]
    public GameObject leftBulletPrefab;      // Префаб пули для левой пушки
    public Transform leftGunSpawnPoint;      // Точка, откуда вылетает пуля

    [Header("Правая пушка")]
    public GameObject rightBulletPrefab;     // Префаб пули для правой пушки
    public Transform rightGunSpawnPoint;     // Точка, откуда вылетает пуля

    [Header("Настройки стрельбы")]
    public float fireRate = 0.25f; // Скорострельность (выстрелов в секунду = 1 / fireRate)

    private float nextLeftFireTime = 0f;
    private float nextRightFireTime = 0f;

    void Update()
    {
        // Стрельба из левой пушки по нажатию Левой Кнопки Мыши
        if (Input.GetMouseButton(0) && Time.time >= nextLeftFireTime)
        {
            // Устанавливаем время следующего возможного выстрела
            nextLeftFireTime = Time.time + fireRate;
            Fire(leftBulletPrefab, leftGunSpawnPoint);
        }

        // Стрельба из правой пушки по нажатию Правой Кнопки Мыши
        if (Input.GetMouseButton(1) && Time.time >= nextRightFireTime)
        {
            // Устанавливаем время следующего возможного выстрела
            nextRightFireTime = Time.time + fireRate;
            Fire(rightBulletPrefab, rightGunSpawnPoint);
        }
    }

    void Fire(GameObject bulletPrefab, Transform spawnPoint)
    {
        if (bulletPrefab == null || spawnPoint == null)
        {
            Debug.LogError("Не назначен префаб пули или точка спавна!");
            return;
        }

        // Создаем пулю в позиции и с поворотом точки спавна
        Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}