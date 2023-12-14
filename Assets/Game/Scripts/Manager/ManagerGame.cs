using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ManagerGame : MonoBehaviour
{
    public Map map;
    public RotateDice dice;
    public ColorEnum[] listEnums;
    public ColorEnum colorCurrent;
    public int index = 0;
    public int endLoop = 0;
    public List<GameObject> danhsachnguahientai;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject[] lights;
    void Start()
    {
        dice.ResetDice();
    }

    void Update()
    {
        if (dice.checkRollCurrent && endLoop == 0)
        {
            text.text = "";
            endLoop++;
            KiemTraNguaTrenMap();
        }
        for(int i = 0; i < lights.Length; i++)
        {
            if(i == index)
            {
                lights[i].SetActive(true);
            }
            else
            {
                lights[i].SetActive(false);
            }
        }
    }

    private void KiemTraNguaTrenMap()
    {
        foreach(GameObject ngua in map.listNguaInMap)
        {
            Ngua nguaclass = ngua.GetComponent<Ngua>();
            if(nguaclass != null)
            {
                if(colorCurrent == nguaclass.color)
                {
                    danhsachnguahientai.Add(ngua);
                }
            }
        }
        KiemTraNguaXuatChuong();
    }

    private void KiemTraNguaXuatChuong()
    {
        int dem = 0;
        foreach(GameObject ngua in danhsachnguahientai)
        {
            Ngua nguaClass = ngua.GetComponent<Ngua>();
            if(nguaClass != null)
            {
                if (nguaClass.checkUse)
                {
                    dem++;
                }
            }
        }

        if(dem == 0)
        {
            KhongCoNguaTrenMap();
        }
        else
        {
            text.text = "Hãy chọn con ngựa bạn muốn di chuyển!!";
            Debug.Log("Co ngua");
        }
    }

    private void KhongCoNguaTrenMap()
    {
        if(dice.index == 6)
        {
            text.text = "Bạn không có ngựa trên sân nên sẽ tự động xuất chuồng cho bạn!!";
            XuatChuong();
        }
        else
        {
            ChuyenLuot();
        }
    }

    private void XuatChuong()
    {
        GameObject ngua = null;
        foreach(GameObject nguacheck in danhsachnguahientai)
        {
            Ngua nguaClass = nguacheck.GetComponent<Ngua>();
            if (!nguaClass.checkUse)
            {
                ngua = nguacheck;
            }
        }
        if(ngua != null)
        {
            Ngua nguaclass = ngua.GetComponent<Ngua>();
            nguaclass.checkUse = true;
            GameObject transXuat = null;
            foreach(XuatChuong xuat in map.listXuatChuong)
            {
                if(xuat.color == colorCurrent)
                {
                    transXuat = xuat.xuatTras;
                }
            }
            Vector3 vc = transXuat.transform.position;
            vc.y += 0.5f;
            ngua.transform.DOMove(vc, 5).SetEase(Ease.Linear).OnKill(() =>
            {
                ResetLuot();
                text.text = "6 rồi roll tiếp nhé <3";
            });
            ngua.transform.localEulerAngles = transXuat.transform.localEulerAngles;
        }
    }

    private void ResetLuot()
    {
        dice.ResetDice();
        danhsachnguahientai.Clear();
        endLoop = 0;
    }

    private void ChuyenLuot()
    {
        index++;
        if(index > 3)
        {
            index = 0;
        }
        colorCurrent = listEnums[index];
        ResetLuot();
    }
}
