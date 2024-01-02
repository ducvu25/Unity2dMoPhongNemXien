using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("----------Muc ban--------")]
    [SerializeField] GameObject goMuc;
    [SerializeField] float maxHeight = 5f;
    [SerializeField] Slider sldMuc;
    Vector3 pMuc;


    [Header("\n\n----------Nong sung--------")]
    [SerializeField] GameObject goNongSung;
    [SerializeField] Slider sldNongSung;
    [SerializeField] Transform pSpawnBullet;
    [SerializeField] GameObject preBullet;

    [Header("\n\n----------Muc ban--------")]
    [SerializeField] float maxVeclocity = 20f;
    [SerializeField] Slider sldLuc;
    float veclocity;

    [Header("\n\n----------Txt--------")]
    [SerializeField] TextMeshProUGUI txtH;
    [SerializeField] TextMeshProUGUI txtL;
    [SerializeField] TextMeshProUGUI txtD;

    [SerializeField] GameObject goSpawn;
    [SerializeField] int number = 3;
    int _number;

    [Header("\n\n----------Dich--------")]
    [SerializeField] GameObject preTunnel;
    GameObject goTunnel;
    [SerializeField] float maxX;
    // Start is called before the first frame update
    void Start()
    {
        pMuc = goMuc.transform.position;
        veclocity = 0;
        _number = number;
        UpdateGoc();
        UpdateLuc();
        UpdateMuc();
        NewEnemy();
    }
    public void UpdateMuc()
    {
        goMuc.transform.position = pMuc + new Vector3(0f, sldMuc.value*maxHeight, 0f);
        sldMuc.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Chiều cao: " + (pSpawnBullet.position.y - pMuc.y).ToString("F2") + " m";
    }
    public void UpdateGoc()
    {
        goNongSung.transform.rotation = Quaternion.Euler(0f, 0f, sldNongSung.value);
        sldNongSung.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Góc bắn: " + (sldNongSung.value).ToString("F2") + " độ";
        sldMuc.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Chiều cao: " + (pSpawnBullet.position.y - pMuc.y).ToString("F2") + " m";
    }
    public void UpdateLuc()
    {
        veclocity = sldLuc.value * maxVeclocity;
        sldLuc.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Tốc độ: " + (veclocity).ToString("F2") + " m/s";
    }
    public void Spawn()
    {
        _number--;
        if(_number == 0)
        {
            _number = number;
            for (int i = goSpawn.transform.childCount - 1; i >= 0; i--)
            {
                // Lấy đối tượng con thứ i
                GameObject childObject = goSpawn.transform.GetChild(i).gameObject;

                // Hủy bỏ đối tượng con
                Destroy(childObject);
            }
        }
        GameObject gameObject = Instantiate(preBullet, pSpawnBullet.position, Quaternion.identity);
        gameObject.transform.SetParent(goSpawn.transform);
        BulletController bulletController = gameObject.GetComponent<BulletController>();
        bulletController.anpha = sldNongSung.value * (Mathf.PI / 180f);
        bulletController.vo = veclocity;
        bulletController.goParent = goSpawn;
        bulletController.pDat = pMuc;
        bulletController.x0 = pSpawnBullet.position.x;
        bulletController.check = true;
        bulletController.controller = this;
        AudioController.instance.PlaySound(0);
        bulletController.tunnel = goTunnel.transform.GetComponent<Tunnel>();
        txtH.text = "Tầm cao tối đa: ... m" ;
        txtL.text = "Tầm dài tối đa: ... m" ; 
    }
    public void UpdateTxt(string s, int type)
    {
        if(type == 0)
            txtH.text = s;
        else
            txtL.text = s;
    }
    public void NewEnemy()
    {
        if (goTunnel != null)
            Destroy(goTunnel);
        goTunnel = Instantiate(preTunnel, new Vector3(Random.Range(pMuc.x + 2, maxX), pMuc.y + 0.3f, 0), Quaternion.identity);
        txtD.text = "Khoảng cách: " + (goTunnel.transform.position.x - pMuc.x).ToString("F2") + " m";
    }
    public void Quit()
    {
        Application.Quit();
    }
}