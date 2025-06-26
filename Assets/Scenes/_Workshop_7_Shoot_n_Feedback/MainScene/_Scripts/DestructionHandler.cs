using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Этот скрипт должен быть на родительском объекте, который содержит Enemy и AudioSource.
public class DestructionHandler : MonoBehaviour
{
    // Ссылки на компоненты, которые мы настроим в инспекторе
    public Enemy enemyToDestroy;
    public AudioSource destructionAudio;

    // Публичный метод, который будет вызывать наш Enemy при столкновении
    public void InitiateDestruction()
    {
        // Запускаем корутину (coroutine), которая будет управлять последовательностью действий
        StartCoroutine(DestructionSequence());
    }

    private IEnumerator DestructionSequence()
    {
        // 1. Проверяем, что все ссылки на месте, чтобы избежать ошибок
        if (enemyToDestroy == null || destructionAudio == null || destructionAudio.clip == null)
        {
            Debug.LogError("DestructionHandler не настроен! Проверьте ссылки на Enemy и AudioSource/AudioClip.", this);
            // Если что-то не так, просто уничтожаем все сразу
            if (enemyToDestroy != null) enemyToDestroy.Explode();
            Destroy(gameObject);
            yield break; // Прерываем выполнение корутины
        }

        // 2. Даем команду врагу взорваться. Враг сам себя уничтожит.
        enemyToDestroy.Explode();

        // 3. Включаем звук
        destructionAudio.Play();

        // 4. Ждем, пока звук доиграет до конца.
        // gameObject этого скрипта (родительский) будет "жить" все это время.
        yield return new WaitForSeconds(destructionAudio.clip.length);

        if (Progress.Instance.PlayerInfo.Score >= 100)
        {
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            Destroy(gameObject);
        }
    }
}