using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Araba;

    [Header("-------- PLATFORM AYARLAR")]
    public GameObject Platform_1;
    public GameObject Platform_2;
    public float[] DonusHizlari;

    private void Start()
    {
        
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.G))
        {
            Araba.GetComponent<Araba>().ilerle = true; //Araba objesinin Araba scriptine ulas ordaki ilerleriyi true yap
        }

        Platform_1.transform.Rotate(new Vector3(0, 0, -DonusHizlari[0]), Space.Self); // X Y ayni Z de benim veridigim degerde kendine surekli bir ivme katip don diyoruz. Space.Self kendi ekseninde daha soft donus icin.

    }

}

