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
    public bool chon = false;
    public List<GameObject> danhsachnguahientai;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject[] lights;
    public List<Transform> waypoints;
    public int currentWaypointIndex = 0;
    public GameObject nguaChoose;
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
        if (chon)
        {
            KiemTraDiChuyen();
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
            ChonNguaTrenMap();
        }
    }

    private void KhongCoNguaTrenMap()
    {
        if (dice.index == 6)
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
            foreach(XuatChuong xuat in map.listXuatChuong)
            {
                if(xuat.color == nguaclass.color)
                {
                    ngua.transform.SetParent(xuat.xuatTras.transform);
                }
            }
            ngua.transform.DOMove(vc, 1).SetEase(Ease.InBounce).OnKill(() =>
            {
                ResetLuot();
                text.text = "6 rồi roll tiếp nhé <3";
            });
            ngua.transform.localEulerAngles = transXuat.transform.localEulerAngles;
        }
    }

    private void ResetLuot()
    {
        text.text = "Lượt đi đội " + colorCurrent;
        dice.ResetDice();
        ResetVFX();
        danhsachnguahientai.Clear();
        nguaChoose = null;
        endLoop = 0;
    }

    private void ResetVFX()
    {
        foreach (GameObject game in danhsachnguahientai)
        {
            Ngua ngua = game.GetComponent<Ngua>();
            if (ngua != null)
            {
                ngua.vfx.SetActive(false);
            }
        }
    }

    public void ChuyenLuot()
    {
        index++;
        if (index > 3)
        {
            index = 0;
        }
        colorCurrent = listEnums[index];
        ResetLuot();
        
    }

    private void ChonNguaTrenMap()
    {
        chon = true;
    }

    private void KiemTraDiChuyen()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                if(clickedObject != null)
                {
                    ResetVFX();
                    nguaChoose = null;
                    foreach (GameObject game in danhsachnguahientai)
                    {
                        if(game == clickedObject)
                        {
                            Ngua ngua = game.GetComponent<Ngua>();
                            if (ngua.checkUse)
                            {
                                ngua.vfx.SetActive(true);
                            }
                            nguaChoose = clickedObject;
                            break;
                        }
                    }
                }
            }
        }
    }


    public void DiChuyen()
    {
        if(nguaChoose != null)
        {
            Ngua ngua = nguaChoose.GetComponent<Ngua>();
            bool coDungOChuongKhong = false;
            foreach(XuatChuong xuat in map.listXuatChuong)
            {
                if(xuat.color == ngua.color)
                {
                    if(nguaChoose.transform.parent == xuat.xuatTras.transform)
                    {
                        coDungOChuongKhong = true;
                        break;
                    } 
                }
            }
            if (coDungOChuongKhong)
            {
                MoveNgua(nguaChoose, ngua, dice.index - 1);
            }
            else
            {
                MoveNgua(nguaChoose, ngua, dice.index);
            }
        }
    }

    private void MoveNgua(GameObject nguaObj, Ngua nguaClass ,int number)
    {
        Transform vitriStart = null;
        if(dice.index == number)
        {
            vitriStart = nguaObj.transform.parent;
        }
        else
        {
            foreach(XuatChuong xuat in map.listXuatChuong)
            {
                if(xuat.color == nguaClass.color)
                {
                    vitriStart = xuat.nextTras.transform;
                }
            }
        }
        if(vitriStart.transform.childCount <= 1)
        {
            int index = 0;
            bool checkmove = true;
            for(int i = 0; i < map.listTransMove.Length; i++)
            {
                if(vitriStart == map.listTransMove[i])
                {
                    index = i;
                }
            }
            for(int i = index; i <= number + index; i++)
            {
                if(index > map.listTransMove.Length - 1)
                {
                    index = 0;
                }
                else
                {
                    if (map.listTransMove[index].childCount > 0 && vitriStart != map.listTransMove[index])
                    {
                        Debug.Log(1);
                        checkmove = false;
                    }
                    else
                    {
                        waypoints.Add(map.listTransMove[i]);
                    }
                }
            }
            if (checkmove)
            {
                MoveToNextWaypoint(index + number, nguaObj);
            }
            else
            {
                waypoints.Clear();
            }
        }
    }

    

    void MoveToNextWaypoint(int index, GameObject ngua)
    {
        if (currentWaypointIndex < waypoints.Count)
        {
            Vector3 vc = waypoints[currentWaypointIndex].position;
            vc.y += 0.5f;
            nguaChoose.transform.DOMove(vc, 1).SetEase(Ease.Linear).OnComplete(() =>
            {
                currentWaypointIndex++;
                MoveToNextWaypoint(index, ngua);
            });
        }
        else
        {
            if(index >= map.listTransMove.Length)
            {
                index = index - map.listTransMove.Length;
            }
            ngua.transform.SetParent(map.listTransMove[index]);
            waypoints.Clear();
            currentWaypointIndex = 0;
            chon = false;
            ChuyenLuot();
        }
    }

    public void CheckButtonClickXuatChuong()
    {
        if(dice.index == 6)
        {
            bool check = false;
            foreach(XuatChuong xuat in map.listXuatChuong)
            {
                if(xuat.color == colorCurrent)
                {
                    int i = xuat.xuatTras.transform.childCount;
                    if(i <= 0)
                    {
                        check = true;
                    }
                }
            }
            if (check)
            {
                XuatChuong();
            }
            else
            {
                text.text = "Bạn không thể xuất chuồng nếu có ngựa đang đứng trên ô Start";
            }
        }
    }
}
