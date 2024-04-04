using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrologueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text prologueText;
    [SerializeField] private Image mapImage;
    private int prologueIndex = 0;
    [SerializeField] private bool mapOn = false;
    [SerializeField] private bool mapOff = false;
    [SerializeField] private float mapOnTimer;
    [SerializeField] private float mapOffTimer;

    private void Start()
    {
        mapImage.gameObject.SetActive(false);

        prologueText.text = "오늘도 실패했어...";
    }

    private void Update()
    {
        mpaImageTimer();
        prologue();
    }

    private void mpaImageTimer()
    {
        if (mapOn == true)
        {
            Color imageColor = mapImage.color;

            if (imageColor.a != 1)
            {
                mapOnTimer += Time.deltaTime / 2;

                imageColor.a = mapOnTimer;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    imageColor.a = 1;
                }

                if (imageColor.a >= 1)
                {
                    imageColor.a = 1f;
                    mapOnTimer = 0f;
                    mapOffTimer = 1f;
                    mapOff = true;
                    mapOn = false;
                }

                mapImage.color = imageColor;
            }
        }

        if (mapOff == true)
        {
            Color imageColor = mapImage.color;

            if (imageColor.a != 0)
            {
                mapOffTimer -= Time.deltaTime / 2;

                imageColor.a = mapOffTimer;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    imageColor.a = 0;
                }

                if (imageColor.a <= 0)
                {
                    imageColor.a = 0f;
                    mapOffTimer = 1;
                    mapOff = false;
                }

                mapImage.color = imageColor;
            }
        }
    }

    /// <summary>
    /// 프롤로그의 대사창을 담당하는 함수
    /// </summary>
    private void prologue()
    {
        switch (prologueIndex)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "내 마법을 통해 사람들에게 디저트를 만들어 주고 싶은데";
                    prologueIndex++;
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "종류가 너무 많아서";
                    prologueIndex++;
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "뭘 만들어야 할지 모르겠어";
                    prologueIndex++;
                }
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "너무 어렵다...";
                    prologueIndex++;
                }
                break;
            case 4:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "디저트 드림 타운?";
                    prologueIndex++;
                }
                break;
            case 5:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "디저트 전통이 100년이라고? ";
                    prologueIndex++;
                }
                break;
            case 6:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "...!";
                    prologueIndex++;
                }
                break;
            case 7:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "좋은 생각이 났어!";
                    prologueIndex++;
                }
                break;
            case 8:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "여기서 디저트에 대해 배우는 거야";
                    prologueIndex++;
                }
                break;
            case 9:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "그럼 맛있는 디저트를 만들 수 있겠지? ";
                    prologueIndex++;
                }
                break;
            case 10:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "결정! ";
                    prologueIndex++;
                }
                break;
            case 11:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "일단 짐부터 살까?";
                    prologueIndex++;
                }
                break;
            case 12:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "이제 출발해 볼까? ";
                    prologueIndex++;
                }
                break;
            case 13:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    mapImage.gameObject.SetActive(true);
                    mapOn = true;
                    prologueIndex++;
                }
                break;
            case 14:
                if (Input.GetKeyDown(KeyCode.Space) && mapOn == false && mapOff == false)
                {
                    prologueText.text = "우와~!!!";
                    prologueIndex++;
                }
                break;
            case 15:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "여기가 바로 디저트 마을이구나...";
                    prologueIndex++;
                }
                break;
            case 16:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "너무 이쁘다~";
                    prologueIndex++;
                }
                break;
            case 17:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "맞다! 촌장님을 만나야 하는데 어디 계신 거지?";
                    prologueIndex++;
                }
                break;
            case 18:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "위쪽으로 가볼까?";
                    prologueIndex++;
                }
                break;
            case 19:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadSceneAsync("DessertVillage");
                }
                break;
        }
    }
}
