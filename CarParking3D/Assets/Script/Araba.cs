using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Araba : MonoBehaviour
{
    //Trail Renderer kullanarak arabama teker izi kattim.

    public bool ilerle;
    bool DurusNoktasiDurumu = false;
    float YukselmeDeger;
    bool PlatformYukselt;

    //
    //
    public GameObject[] Tekerizleri;
    public Transform parent; // Bir ust cismin icine arabayi atalim ki arabada platform ile beraber donsun     ---------- PARENT
    public GameManager _GameManager;
    public GameObject ParcPoint;
    

    

    // Update is called once per frame
    void Update()
    {

        if (!DurusNoktasiDurumu)
            transform.Translate(8f * Time.deltaTime * transform.forward); // arabaya ileri dogru bu hizda ilerlet.
        if (ilerle)
            transform.Translate(15f * Time.deltaTime * transform.forward); // arabaya ileri dogru bu hizda ilerlet.
        if (PlatformYukselt)
        {

            if(YukselmeDeger > _GameManager.Platform_1.transform.position.y)
            {
                //yavasca mevcut oldugu konumdan soyledigim konuma gec LERP METODU
                _GameManager.Platform_1.transform.position = Vector3.Lerp(_GameManager.Platform_1.transform.position, new Vector3(_GameManager.Platform_1.transform.position.x, _GameManager.Platform_1.transform.position.y + 1.3f, _GameManager.Platform_1.transform.position.z), .010f);

            }else
            {
                PlatformYukselt = false;
            }


        }
    }


    private void OnCollisionEnter(Collision collision) // box coliderlar yardimi ile carpismalar olunca tagler uzerinde islem yapmamizi saglayan fonksyon
    {


        // rigitbody varken Triger ile calismazken boyle yaptik
       /* if (collision.gameObject.CompareTag("DurusNoktasi")) //eger tagi Parking olan bir cisme carparsa
        {
            DurusNoktasiDurumu = true;
            _GameManager.DurusNoktasi.SetActive(false); // ilk araba carpinca durusnoktasini kapat ki ileri gitmeye calistigi zaman kuvvetle karsilasmasin
        } */


        if (collision.gameObject.CompareTag("Parking")) //eger tagi Parking olan bir cisme carparsa
        {
            ArabaTeknikIslemi();

            transform.SetParent(parent); // disardan verdigim objeyi ana cismimin yani arabamin parenti yap diyoruz.

            //ARABA CARPTIGI ZAMAN PARK ALANINA BUTUN KONUM VE ROTASYONLARINI KITLE KI DEGISIK GORUNTULER OLUSMASIN
            //   GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ |
            //     RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;


            if (_GameManager.YukselecekPlatformVarmi)
            {
                YukselmeDeger = _GameManager.Platform_1.transform.position.y + 1.3f;
                PlatformYukselt = true;
            }


            _GameManager.YeniArabaGetir();
        }

        
        else if (collision.gameObject.CompareTag("Araba"))
        {
            _GameManager.CarpmaEfekti.transform.position = ParcPoint.transform.position;
            _GameManager.CarpmaEfekti.Play();
            ArabaTeknikIslemi();
            _GameManager.Kaybettin(); //Panel cikaran metod
        }
        
    }

    void ArabaTeknikIslemi()
    {
        ilerle = false;
        Tekerizleri[0].SetActive(false); //Teker izlerini parking box coliderina carpinca kapat.
        Tekerizleri[1].SetActive(false); // Teker izlerini parking box coliderina carpinca kapat.
    }

    private void OnTriggerEnter(Collider other) // Fiziksel kuvvet gibi seylere ihtiyacimiz yoksa rigitbody kullanmadan box collider ve triger ile kolayca cozuyoruz.
    {
        if (other.CompareTag("DurusNoktasi"))
        {
            DurusNoktasiDurumu = true;

        }
        else if (other.CompareTag("Elmas"))
        {
            other.gameObject.SetActive(false); // Carpmis oldugumuz o objeyide pasiflestirmis oluyoruz.
            _GameManager.ElmasSayisi++;
            _GameManager.Sesler[0].Play();
        }
        else if (other.CompareTag("OrtaGobek"))
        {
            _GameManager.CarpmaEfekti.transform.position = ParcPoint.transform.position;
            _GameManager.CarpmaEfekti.Play();
            ArabaTeknikIslemi();
            _GameManager.Kaybettin();//Panel cikaran metod
        }
        else if (other.CompareTag("On_Parking")) // onde duran kucuk gorunmez collidera carparsan
        {
            //other.gameObject.GetComponent<On_Parking>().ParkingAktiflestir(); //git o objeye eris onun icinde scripte eris ordan o metodu cagir dedik.
            other.gameObject.GetComponent<On_Parking>().Parking.SetActive(true);
        }
    }
}
