using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]//이 스크립트가 동작하는 환경에 해당 컴포넌트가 있으면 통과, 없으면 생성 ,어트리뷰트
public class FunctionFade : MonoBehaviour
{
    public static FunctionFade Instance;

    Image imgFade;
    [SerializeField] float fadeTime = 1.0f;//페이드 되는 시간 및 복구되는 시간

    UnityAction actionFadeOut;//페이드 아웃 되었을때 동작할 기능
    UnityAction actionFadeIn;//페이드 인 되었을때 동작할 기능

    bool fade = true;//true가 In false가 Out;

    private void Awake()
    {
        imgFade = GetComponent<Image>();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (fade && imgFade.color.a > 0)
        {
            Color color = imgFade.color;
            color.a -= Time.deltaTime / fadeTime;
            if (color.a < 0)
            {
                color.a = 0;
                invokeAction(fade);
            }
            imgFade.color = color;
        }
        else if (!fade && imgFade.color.a < 1)
        {
            Color color = imgFade.color;
            color.a += Time.deltaTime / fadeTime;
            if (color.a > 1)
            {
                color.a = 1;
                invokeAction(fade);
            }
            imgFade.color = color;
        }

        imgFade.raycastTarget = imgFade.color.a != 0;
    }

    private void invokeAction(bool _fade)
    {
        switch (_fade)
        {
            case true:
                {
                    if (actionFadeIn != null)
                    {
                        actionFadeIn.Invoke();
                        actionFadeIn = null;
                    }
                }
                break;

            case false:
                {
                    if (actionFadeOut != null)
                    {
                        actionFadeOut.Invoke();
                        actionFadeOut = null;
                    }
                }
                break;
        }
    }

    /// <summary>
    /// 페이드 기능을 수행합니다
    /// </summary>
    /// <param name="_fade">true는 In false 는 Out</param>
    /// <param name="_action">ture일시 In이 될때 기능이, false일때 Out기능을 등록합니다</param>
    public void SetActive(bool _fade, UnityAction _action = null)
    {
        fade = _fade;
        switch (_fade)
        {
            case true: actionFadeIn = _action; break;
            case false: actionFadeOut = _action; break;
        }
    }
}
