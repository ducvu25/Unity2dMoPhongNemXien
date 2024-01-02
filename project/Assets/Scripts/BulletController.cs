using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("-------Thong tin co ban------")]
    public float anpha;
    public float vo;
    public float x0;

    [Header("\n-------Thong tin khac------")]
    public Vector3 pDat;
    public bool check = false;
    public Vector3 p;

    float t;

    [Header("\n-------Duong di------")]
    public int maxPoints = 100; // Số lượng điểm trên quỹ đạo
    public GameObject prePointSpawn;
    List<GameObject> points;
    public float time_spawn = 0.5f;
    float _time;
    [Header("\n-------Thong tin diem ban------")]
    public GameObject goCanva;
    bool showH;

    public GameObject goParent;
    public GameController controller;

    public Tunnel tunnel;
    // Start is called before the first frame update
    void Start()
    {
        t = 0;
        _time = time_spawn;
        p = transform.position;

        showH = false;
        points = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!check) return;
        t += Time.deltaTime;
        _time -= Time.deltaTime;
        if(_time < 0)
        {
            maxPoints--;
            if(maxPoints > 0)
            {
                points.Add(Instantiate(prePointSpawn, transform.position, Quaternion.identity));
                points[points.Count - 1].transform.SetParent(goParent.transform);

            }
            _time = time_spawn;
        }
        if(vo * Mathf.Sin(anpha) - Setting.g * t <= 0.05f && !showH)
        {
            showH = true;
            points.Add(Instantiate(prePointSpawn, transform.position, Quaternion.identity));
            points[points.Count - 1].transform.SetParent(goParent.transform);
            points.Add(Instantiate(goCanva, transform.position, Quaternion.identity));
            //Debug.Log(y0);
            float h = (transform.localPosition.y - pDat.y) * Setting.tiLe;
            string s = "Tầm cao: " + h.ToString("F2") + " m, tai t = " + t.ToString("F2") + " s";
            points[points.Count - 1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "h = " + h.ToString("F2") + " m";
            controller.UpdateTxt(s, 0);
            points[points.Count - 1].transform.SetParent(goParent.transform);
        }
        if(transform.position.y <= pDat.y && check)
        {
            check = false;
            points.Add(Instantiate(goCanva, transform.position, Quaternion.identity));
            float l = (transform.localPosition.x - x0) * Setting.tiLe;
            string s = "Tầm xa: " + l.ToString("F2") + " m, tai t = " + t.ToString("F2") + " s";
            points[points.Count - 1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "l = " + l.ToString("F2") + " m";
            controller.UpdateTxt(s, 1);
            points[points.Count - 1].transform.SetParent(goParent.transform);
            tunnel.Show(transform.localPosition);
            AudioController.instance.PlaySound(1);
        }

        //Debug.Log((vo * Mathf.Cos(anpha) * t).ToString() + " " + (Mathf.Cos(anpha)).ToString() + " " + (anpha).ToString());
        transform.position = p + new Vector3(vo * Mathf.Cos(anpha) * t, vo * Mathf.Sin(anpha) * t - Setting.g * t * t / 2);
    }
/*    public void Destroy()
    {
        // Hủy các đối tượng
        foreach (var point in points)
        {
            Destroy(point);
        }
        Destroy(gameObject);
    }*/
}
