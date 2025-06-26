using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject piecePrefab;

    public Vector3Int piecesCount = new Vector3Int(4, 8, 4); // Количество кусочков по осям X, Y, Z

    public float explosionForce = 500f;

    public float explosionRadius = 5f;

    public float upwardModifier = 1.0f; // Сила, толкающая кусочки вверх

    [SerializeField] TextMeshProUGUI textScore;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject); // Уничтожаем пулю

            DestructionHandler handler = GetComponentInParent<DestructionHandler>();

            if (handler != null)
            {
                handler.InitiateDestruction();
            }
            else
            {
                Debug.LogWarning("На родительском объекте не найден DestructionHandler. Взрыв без звука.", this);
                Explode();
            }
        }
    }


    public void Explode()
    {
        // Получаем размер оригинального объекта из его коллайдера
        Vector3 originalSize = GetComponent<Collider>().bounds.size;

        // Вычисляем размер одного кусочка на основе общего размера и количества
        Vector3 pieceSize = new Vector3(
            originalSize.x / piecesCount.x,
            originalSize.y / piecesCount.y,
            originalSize.z / piecesCount.z
        );

        // Вычисляем стартовую точку, чтобы начать создавать кусочки из нижнего-левого-дальнего угла
        Vector3 startPoint = transform.position - originalSize / 2 + pieceSize / 2;

        for (int x = 0; x < piecesCount.x; x++)
        {
            for (int y = 0; y < piecesCount.y; y++)
            {
                for (int z = 0; z < piecesCount.z; z++)
                {
                    // Вычисляем позицию для каждого кусочка
                    Vector3 spawnPosition = startPoint + new Vector3(
                        x * pieceSize.x,
                        y * pieceSize.y,
                        z * pieceSize.z
                    );

                    // Создаем кусочек
                    GameObject piece = Instantiate(piecePrefab, spawnPosition, transform.rotation);

                    // Масштабируем кусочек, чтобы он точно соответствовал ячейке
                    piece.transform.localScale = pieceSize;

                    Rigidbody pieceRb = piece.GetComponent<Rigidbody>();
                    if (pieceRb != null)
                    {
                        // Применяем силу взрыва
                        pieceRb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardModifier);
                    }
                }
            }
        }

        AddScore();

        // Уничтожаем оригинальный объект
        Destroy(gameObject);
    }
    
    private void AddScore()
    {
        Progress.Instance.PlayerInfo.Score += 10;
        textScore.text = "Score: " + Progress.Instance.PlayerInfo.Score.ToString();
    }
}