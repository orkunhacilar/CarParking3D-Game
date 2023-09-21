using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Header("-------- ARABA AYARLARI")]
    public GameObject[] Arabalar;
    public GameObject DurusNoktasi;
    public int KacArabaOlsun; // bu lvda kac tane araba olsun gibi dusun 
    int AktifAracIndex=0;

    [Header("-------- PLATFORM AYARLAR")]
    public GameObject Platform_1;
    public GameObject Platform_2;
    public float[] DonusHizlari;

    private void Start()
    {
        /*  for(int i=0; i< KacArabaOlsun; i++)
          {

          } */

       

    }

    public void YeniArabaGetir() // YENI ARABAYI OYUNDA AKTIF ETMEK ICIN CAGIRIYORUZ.
    {
         DurusNoktasi.SetActive(true); // kapali olan durus noktasini tekrar acmam gerek 1 araba park edildigi zaman ac diyorum tekrar

        if (AktifAracIndex < KacArabaOlsun) // hala oyunu bitirmemis demektir 5 araba var fakat biz daha 2cisindeyiz gibi dusun
        {
            Arabalar[AktifAracIndex].SetActive(true);  //Index kactaysa o arabayi aktiflestir diyoruz.
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.G))
        {
             Arabalar[AktifAracIndex].GetComponent<Araba>().ilerle = true; //Araba objesinin Araba scriptine ulas ordaki ilerleriyi true yap
             AktifAracIndex++;
        }

        Platform_1.transform.Rotate(new Vector3(0, 0, -DonusHizlari[0]), Space.Self); // X Y ayni Z de benim veridigim degerde kendine surekli bir ivme katip don diyoruz. Space.Self kendi ekseninde daha soft donus icin.

    }

}

