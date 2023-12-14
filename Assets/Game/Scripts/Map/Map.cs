using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Transform[] listTransMove;
    public MoveHome[] listMoveHome;
    public NguaTrongChuong[] listNguaPool;
    public XuatChuong[] listXuatChuong;
    public List<GameObject> listNguaInMap;
    public List<NguaHientai> listNguaCurrent;

    private void Start()
    {
        foreach (NguaTrongChuong ngua in listNguaPool)
        {
            NguaHientai nguaHT = new NguaHientai();
            List<GameObject> nguas = new List<GameObject>();
            nguaHT.colorNgua = ngua.color;
            foreach (Transform trans in ngua.trans)
            {
                GameObject nguapool = ObjectPool.Ins.SpawnFromPool(ngua.tagNgua, trans.position, trans.rotation);
                nguas.Add(nguapool);
                listNguaInMap.Add(nguapool);
            }
            nguaHT.nguas = nguas;
            listNguaCurrent.Add(nguaHT);
        }
    }
}
