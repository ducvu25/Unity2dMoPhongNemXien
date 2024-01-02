using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    [SerializeField] float[] distances = { 0.5f, 1f, 1.5f };
    // Start is called before the first frame update
    [SerializeField] GameObject txtScore;

    public void Show(Vector3 p)
    {
        float distance = Vector2.Distance(transform.position, p);
        if(distance < distances[0])
        {
            SettingTxt("A");
        }else if(distance < distances[1])
        {
            SettingTxt("B");
        }else if (distance < distances[2])
        {
            SettingTxt("C");
        }else SettingTxt("F");
    }
    void SettingTxt(string s)
    {
        GameObject gameObject = Instantiate(txtScore, transform.position, Quaternion.identity);
        gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = s;
        Destroy(gameObject, 1f);
    }

}
