using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StudyController : MonoBehaviour
{
    [Header("공부 게임 설정")]
    [SerializeField] private List<Button> answerButtons;
    [SerializeField] private Image timerImage;
    [SerializeField] private TMP_Text text;
    [Space]
    [SerializeField] private List<GameObject> lifeImage;
    [Space]
    [SerializeField] private List<GameObject> clearTextAndOverText;
    [Space]
    [SerializeField] private TMP_Text roundText;

    private int life = 3;

    private float timer;

    private int answerInt;

    private int round;

    private bool check = false;

    private void Awake()
    {
        timer = 80f;

        buttonCheck();
    }

    private void Update()
    {
        if (round == 3)
        {
            clearTextAndOverText[0].SetActive(true);
            return;
        }
        else if (life == 0)
        {
            clearTextAndOverText[1].SetActive(true);
            return;
        }

        if (round == 0)
        {
            roundText.text = $"1 라운드";
        }
        else if (round == 1)
        {
            roundText.text = $"2 라운드";
        }
        else
        {
            roundText.text = $"마지막 라운드";
        }

        gameStart();
    }

    private void gameStart()
    {
        timer -= Time.deltaTime;
        timerImage.fillAmount = timer / 80f;

        if (check == false)
        {
            int randomA = Random.Range(0, 11);
            int randomB = Random.Range(0, 11);
            int randomC = Random.Range(0, 4);
            int buttonRandom = Random.Range(0, 3);

            if (randomC == 0)
            {
                buttonCheck(buttonRandom, randomA + randomB);
                text.text = $"{randomA} + {randomB} = ?";

                answerInt = randomA + randomB;
            }
            else if (randomC == 1)
            {
                buttonCheck(buttonRandom, randomA - randomB);
                text.text = $"{randomA} - {randomB} = ?";

                answerInt = randomA - randomB;
            }
            else if (randomC == 2)
            {
                buttonCheck(buttonRandom, randomA * randomB);
                text.text = $"{randomA} × {randomB} = ?";

                answerInt = randomA * randomB;
            }
            else if (randomC == 3) 
            {
                buttonCheck(buttonRandom, randomA / randomB);
                text.text = $"{randomA} ÷ {randomB} = ?";

                answerInt = randomA / randomB;
            }

            check = true;
        }
    }

    private void buttonCheck(int _check, int _number)
    {
        if (_check == 0)
        {
            int ranA = Random.Range(0, 11);
            int ranB = Random.Range(0, 11);

            int ranC = Random.Range(0, 11);
            int ranD = Random.Range(0, 11);

            Answer scA = answerButtons[0].GetComponent<Answer>();
            scA.SetCheck(_number);

            Answer scB = answerButtons[1].GetComponent<Answer>();
            scB.SetCheck(ranA + ranB);

            Answer scC = answerButtons[2].GetComponent<Answer>();
            scC.SetCheck(ranC + ranD);
        }
        else if (_check == 1)
        {
            int ranA = Random.Range(0, 11);
            int ranB = Random.Range(0, 11);

            int ranC = Random.Range(0, 11);
            int ranD = Random.Range(0, 11);

            Answer scA = answerButtons[0].GetComponent<Answer>();
            scA.SetCheck(ranA + ranB);

            Answer scB = answerButtons[1].GetComponent<Answer>();
            scB.SetCheck(_number);

            Answer scC = answerButtons[2].GetComponent<Answer>();
            scC.SetCheck(ranC + ranD);
        }
        else
        {
            int ranA = Random.Range(0, 11);
            int ranB = Random.Range(0, 11);

            int ranC = Random.Range(0, 11);
            int ranD = Random.Range(0, 11);

            Answer scA = answerButtons[0].GetComponent<Answer>();
            scA.SetCheck(ranC + ranD);

            Answer scB = answerButtons[1].GetComponent<Answer>();
            scB.SetCheck(ranA + ranB);

            Answer scC = answerButtons[2].GetComponent<Answer>();
            scC.SetCheck(_number);
        }
    }

    private void buttonCheck()
    {
        answerButtons[0].onClick.AddListener(() => 
        {
            Answer sc = answerButtons[0].GetComponent<Answer>();
            if (answerInt == sc.Check())
            {
                round++;
                check = false;
                timer = 80f;
            }
            else
            {
                life--;
                lifeImage[life].SetActive(false);
            }
        });

        answerButtons[1].onClick.AddListener(() =>
        {
            Answer sc = answerButtons[1].GetComponent<Answer>();
            if (answerInt == sc.Check())
            {
                round++;
                check = false;
                timer = 80f;
            }
            else
            {
                life--;
                lifeImage[life].SetActive(false);
            }
        });

        answerButtons[2].onClick.AddListener(() =>
        {
            Answer sc = answerButtons[2].GetComponent<Answer>();
            if (answerInt == sc.Check())
            {
                round++;
                check = false;
                timer = 80f;
            }
            else
            {
                life--;
                lifeImage[life].SetActive(false);
            }
        });
    }
}
