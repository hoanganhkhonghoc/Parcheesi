using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NguaHientai
{
    public List<GameObject> nguas;
    public ColorEnum colorNgua;
}
[System.Serializable]
public class XuatChuong
{
    public GameObject xuatTras;
    public GameObject nextTras;
    public ColorEnum color;
}