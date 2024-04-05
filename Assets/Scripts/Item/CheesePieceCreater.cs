using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheesePieceCreater : MonoBehaviour
{
    private Inventory inventory;

    private TIuBookManager tIuBookManager;

    [SerializeField] private Slider cheesePieceSlider;
    private bool chnageMove = false;
    [SerializeField] private Transform cheesePieceTransform;
    [SerializeField] private GameObject cheesePiecePrefab;

    private void Start()
    {
        inventory = Inventory.Instance;

        tIuBookManager = TIuBookManager.Instance;

        cheesePieceSlider.value = 0;
    }

    private void Update()
    {
        starForceMove();
        starCatchCheck();
    }

    private void starForceMove()
    {
        if (chnageMove == false)
        {
            cheesePieceSlider.value += Time.deltaTime / 2f;

            if (cheesePieceSlider.value >= 1)
            {
                chnageMove = true;
                cheesePieceSlider.value = 1;
            }
        }
        else if (chnageMove == true)
        {
            cheesePieceSlider.value -= Time.deltaTime / 2f;

            if (cheesePieceSlider.value <= 0)
            {
                chnageMove = false;
                cheesePieceSlider.value = 0;
            }
        }
    }

    private void starCatchCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (cheesePieceSlider.value >= 0.4f && cheesePieceSlider.value <= 0.6f)
            {
                inventory.SetItem(100);
                tIuBookManager.SetItemIdCheck(100);
                chnageMove = false;
                cheesePieceSlider.value = 0;
                gameObject.SetActive(false);
            }
            else
            {
                chnageMove = false;
                cheesePieceSlider.value = 0;
            }

            return;
        }
    }
}
