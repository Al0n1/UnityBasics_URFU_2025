using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Нужно для перезапуска игры

public class GameManager : MonoBehaviour
{

    public GameObject[] cubes;

    public Button[] cubeButtons;

    public GameObject WinMessage;

    public GameObject LoseMessage;

    private GameObject winCube;

    private bool isGameFinished = false;


    void Start()
    {
        if (WinMessage != null) WinMessage.SetActive(false);
        if (LoseMessage != null) LoseMessage.SetActive(false);

        winCube = cubes[Random.Range(0, cubes.Length)];

        Debug.Log("Победный куб: " + winCube.name);
    }


    public void OnCubeSelected(int playerChoiceIndex)
    {
        // Если игра уже закончилась, ничего не делаем
        if (isGameFinished)
        {
            return;
        }
        isGameFinished = true;

        // 1. Отключаем все кнопки, чтобы нельзя было нажать еще раз
        foreach (Button button in cubeButtons)
        {
            button.interactable = false;
        }

        foreach (GameObject cube in cubes)
        {
            // Если текущий куб в цикле - это победный куб
            if (cube == winCube)
            {
                // Находим его компонент Rigidbody и включаем гравитацию
                Rigidbody rb = cube.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = true;
                }
            }
        }

        // 3. Проверяем, угадал ли игрок
        // Сравниваем GameObject, на который нажал игрок (cubes[playerChoiceIndex]),
        // с заранее выбранным победным кубом (winCube)
        if (cubes[playerChoiceIndex] == winCube)
        {
            // Игрок угадал!
            Debug.Log("ПОБЕДА!");
            WinMessage.SetActive(true);
        }
        else
        {
            // Игрок не угадал
            Debug.Log("ПОРАЖЕНИЕ!");
            LoseMessage.SetActive(true);
        }
    }
}