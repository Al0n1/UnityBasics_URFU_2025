using System.Collections;
using UnityEngine;

public class WorldCollapseTarget : MonoBehaviour
{
    // Тэги объектов, которые нужно будет деактивировать.
    // Это более гибкий подход, чем искать объекты по имени.
    public string[] tagsToDeactivate = { "Player", "Environment", "Target", "Bullet", "GG" };

    // Задержка перед тем, как мир "схлопнется" после попадания.
    public float delayBeforeCollapse = 0.2f;

    public float delayBeforeEndText = 1.0f;

    public GameObject endCamera;
    public GameObject endText;

    private bool isHit = false; // Флаг, чтобы избежать повторных срабатываний

    // Этот метод вызывается, когда в коллайдер этого объекта что-то входит.
    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, не был ли уже произведен выстрел и попал ли в нас объект с тегом "Bullet"
        if (!isHit && collision.gameObject.CompareTag("Bullet"))
        {
            isHit = true; // Устанавливаем флаг, чтобы предотвратить повторный вызов

            // Запускаем корутину, чтобы "схлопывание" произошло с небольшой задержкой
            StartCoroutine(CollapseWorld());
        }
    }

    private IEnumerator CollapseWorld()
    {
        // Ждем небольшую паузу для эффекта
        yield return new WaitForSeconds(delayBeforeCollapse);

        Debug.Log("Мир схлопывается!");

        // Проходим по всем тегам, которые мы указали в инспекторе
        foreach (string tag in tagsToDeactivate)
        {
            // Находим ВСЕ игровые объекты с данным тегом
            GameObject[] objectsToDeactivate = GameObject.FindGameObjectsWithTag(tag);

            if (objectsToDeactivate.Length == 0)
            {
                Debug.LogWarning("Не найдено объектов с тегом: " + tag);
                continue;
            }

            // Деактивируем каждый найденный объект
            foreach (GameObject obj in objectsToDeactivate)
            {
                if (obj != this.gameObject)
                {
                    obj.SetActive(false);
                }
            }
        }


        endCamera.SetActive(true);
        
        yield return new WaitForSeconds(delayBeforeEndText);

        endText.SetActive(true);
        

        this.gameObject.SetActive(false);
    }
}