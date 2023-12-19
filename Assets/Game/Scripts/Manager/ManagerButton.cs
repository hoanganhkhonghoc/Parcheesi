using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerButton : Singleton<ManagerButton>
{
    [SerializeField] private RotateDice dice;
    public GameObject buttonXucXac;
    public GameObject buttonXuatChuong;
    public GameObject buttonDiChuyen;

    private void Update()
    {
        if (dice.checkRollCurrent)
        {
            if(dice.index == 6)
            {
                buttonXuatChuong.SetActive(true);
                buttonDiChuyen.SetActive(true);
            }
            else
            {
                buttonDiChuyen.SetActive(true);
            }
        }
        else
        {
            buttonXuatChuong.SetActive(false);
            buttonDiChuyen.SetActive(false);
        }

        if (dice.checkRoll)
        {
            buttonXucXac.SetActive(true);
        }
        else
        {
            buttonXucXac.SetActive(false);
        }
    }
}
