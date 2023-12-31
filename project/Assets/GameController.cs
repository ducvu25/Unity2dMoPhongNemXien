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

    [Header("\n\n----------Muc ban--------")]
    [SerializeField] float maxVeclocity = 20f;
    [SerializeField] Slider sldLuc;
    float veclocity;

    // Start is called before the first frame update
    void Start()
    {
        pMuc = goMuc.transform.position;
        veclocity = 0;
        UpdateGoc();
        UpdateLuc();
        UpdateMuc();
    }
    public void UpdateMuc()
    {
        goMuc.transform.position = pMuc + new Vector3(0f, sldMuc.value*maxHeight, 0f);
        sldMuc.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Chiều cao: " + (sldMuc.value * maxHeight*2).ToString("F2") + " m";
    }
    public void UpdateGoc()
    {
        goNongSung.transform.rotation = Quaternion.Euler(0f, 0f, sldNongSung.value);
        sldNongSung.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Góc bắn: " + (sldNongSung.value).ToString("F2") + " độ";
    }
    public void UpdateLuc()
    {
        veclocity = sldLuc.value * maxVeclocity;
        sldLuc.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Tốc độ: " + (veclocity).ToString("F2") + " m/s";
    }
    
}