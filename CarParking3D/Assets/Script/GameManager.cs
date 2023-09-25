using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TEXT MESH PROYU DAHIL EDIYORUZ
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("-------- ARABA AYARLARI")]
    public GameObject[] Arabalar;
    public int KacArabaOlsun; // bu lvda kac tane araba olsun gibi dusun
    int KalanAracSayisiDegeri;
    int AktifAracIndex=0;
    


    [Header("-------- CANVAS AYARLAR")]
    public Sprite AracGeldiGorseli;
    public TextMeshProUGUI KalanAracSayisi;
    public GameObject[] ArabaCanvasGorselleri;
    public TextMeshProUGUI[] Textler;
    public GameObject[] Panellerim;
    public GameObject[] TapToButonlar;

    [Header("-------- PLATFORM AYARLAR")]
    public GameObject Platform_1;
    public GameObject Platform_2;
    public float[] DonusHizlari;
    bool DonusVarmi;

    [Header("-------- LEVEL AYARLAR")]
    public int ElmasSayisi;
    public ParticleSystem CarpmaEfekti;
    public AudioSource[] Sesler;



    private void Start()
    {
        DonusVarmi = true;

        VarsayilanDegerleriKontrolEt();

        KalanAracSayisiDegeri = KacArabaOlsun;

        /*
        KalanAracSayisi.text = KalanAracSayisiDegeri.ToString();

          for(int i=0; i< KacArabaOlsun; i++)
          {
            ArabaCanvasGorselleri[i].SetActive(true); // kac araba ile oynaniyosa o kadar beyaz renkteki araba img goster ac diyoruz.
          }  */

       

    }

    public void YeniArabaGetir() // YENI ARABAYI OYUNDA AKTIF ETMEK ICIN CAGIRIYORUZ.
    {
         
         KalanAracSayisiDegeri--;

        if (AktifAracIndex < KacArabaOlsun) // hala oyunu bitirmemis demektir 5 araba var fakat biz daha 2cisindeyiz gibi dusun
        {
            Arabalar[AktifAracIndex].SetActive(true);  //Index kactaysa o arabayi aktiflestir diyoruz.
        }
        else {
            Kazandin();
        }

        
        /*  ArabaCanvasGorselleri[AktifAracIndex-1].GetComponent<Image>().sprite = AracGeldiGorseli; // araba park edince canvastan yesil yap
         
         KalanAracSayisi.text = KalanAracSayisiDegeri.ToString();   */
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.G))
        {
             Arabalar[AktifAracIndex].GetComponent<Araba>().ilerle = true; //Araba objesinin Araba scriptine ulas ordaki ilerleriyi true yap
             AktifAracIndex++;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Panellerim[0].SetActive(false);
        }

        if(DonusVarmi)
        Platform_1.transform.Rotate(new Vector3(0, 0, -DonusHizlari[0]), Space.Self); // X Y ayni Z de benim veridigim degerde kendine surekli bir ivme katip don diyoruz. Space.Self kendi ekseninde daha soft donus icin.

    }

    public void Kaybettin()
    {
        DonusVarmi = false;
        // ------------------------------   BU KOD SATIRI SAYESINDE KAYBETSE BILE ALDIGI ELMASLARI KAYDEDIYORUZ SISTEME !!! ------------------------------
       // PlayerPrefs.SetInt("Elmas", PlayerPrefs.GetInt("Elmas") + ElmasSayisi); // Kaybettigimde elmas sayim kacsa onla bu bolumde topladiklarimi toplayip ata diyorum

        Textler[6].text = PlayerPrefs.GetInt("Elmas").ToString();   // ToString burda bir nevi casting yapiyor gibi dusunebiliriz aldigi degeri stirng yapip texte atiyor.
        Textler[7].text = SceneManager.GetActiveScene().name;
        Textler[8].text = (KacArabaOlsun - KalanAracSayisiDegeri).ToString();
        Textler[9].text = ElmasSayisi.ToString();

        Sesler[1].Play();
        Sesler[3].Play();
        Panellerim[1].SetActive(true);
        Invoke("KaybettinButonuOrtayaCikart", 2f);   // GECIKMELI METOD  CAGIRMA KOMUTU ( INVOKE ) Invoke Metodu belli bir sure sonra bir metodu cagirmak istersek kullaniyoruz
    }

    void KaybettinButonuOrtayaCikart()
    {
        TapToButonlar[0].SetActive(true);
    }

    void KazandinButonuOrtayaCikart()
    {
        TapToButonlar[1].SetActive(true);
    }

    public void Kazandin()
    {
        PlayerPrefs.SetInt("Elmas", PlayerPrefs.GetInt("Elmas") + ElmasSayisi); // Kaybettigimde elmas sayim kacsa onla bu bolumde topladiklarimi toplayip ata diyorum

        Textler[2].text = PlayerPrefs.GetInt("Elmas").ToString();   // ToString burda bir nevi casting yapiyor gibi dusunebiliriz aldigi degeri stirng yapip texte atiyor.
        Textler[3].text = SceneManager.GetActiveScene().name;
        Textler[4].text = (KacArabaOlsun - KalanAracSayisiDegeri).ToString();
        Textler[5].text = ElmasSayisi.ToString();

        Sesler[2].Play();
        Panellerim[2].SetActive(true);
        Invoke("KazandinButonuOrtayaCikart", 2f);   // GECIKMELI METOD  CAGIRMA KOMUTU ( INVOKE ) Invoke Metodu belli bir sure sonra bir metodu cagirmak istersek kullaniyoruz
    }


    //BELLEK YONETIMI

    void VarsayilanDegerleriKontrolEt()
    {
        if (!PlayerPrefs.HasKey("Elmas")) //Elmas diye bir string kaydedilmismi bak eger false gelirse bir alta in ve elmas degerini kaydederek basla
        {
            PlayerPrefs.SetInt("Elmas", 0);
            PlayerPrefs.SetInt("Level", 1);
        }

        Textler[0].text = PlayerPrefs.GetInt("Elmas").ToString();   // ToString burda bir nevi casting yapiyor gibi dusunebiliriz aldigi degeri stirng yapip texte atiyor.
        Textler[1].text = SceneManager.GetActiveScene().name;

    }


    /* void IzleVeDevamEt()
     {

     }   */

    /* void IzleVeDahaFazlaKazan()
   {

   }   */

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // aktif olan sceneindeximi al ve bir daha onu yukle 
    }

    public void SonrakiLevel()
    {
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1); // aktif lvye +1 ekle dedik.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); // aktif olan sceneindeximi al ve bir daha onu yukle 
    }

}

