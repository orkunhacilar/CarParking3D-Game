using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Araba : MonoBehaviour
{
    //Trail Renderer kullanarak arabama teker izi kattim.

    public bool ilerle;
    bool DurusNoktasiDurumu = false;
    
    //
    //
    public GameObject[] Tekerizleri;
    public Transform parent; // Bir ust cismin icine arabayi atalim ki arabada platform ile beraber donsun
    public GameManager _GameManager;

    

    // Update is called once per frame
    void Update()
    {

        if (!DurusNoktasiDurumu)
            transform.Translate(8f * Time.deltaTime * transform.forward); // arabaya ileri dogru bu hizda ilerlet.
        if (ilerle)
            transform.Translate(15f * Time.deltaTime * transform.forward); // arabaya ileri dogru bu hizda ilerlet.
    }


    private void OnCollisionEnter(Collision collision) // box coliderlar yardimi ile carpismalar olunca tagler uzerinde islem yapmamizi saglayan fonksyon
    {
        if (collision.gameObject.CompareTag("DurusNoktasi")) //eger tagi Parking olan bir cisme carparsa
        {
            DurusNoktasiDurumu = true;
            _GameManager.DurusNoktasi.SetActive(false); // ilk araba carpinca durusnoktasini kapat ki ileri gitmeye calistigi zaman kuvvetle karsilasmasin
        }
        else if (collision.gameObject.CompareTag("Parking")) //eger tagi Parking olan bir cisme carparsa
        {
            ilerle = false;
            Tekerizleri[0].SetActive(false); //Teker izlerini parking box coliderina carpinca kapat.
            Tekerizleri[1].SetActive(false); // Teker izlerini parking box coliderina carpinca kapat.

            transform.SetParent(parent); // disardan verdigim objeyi ana cismimin yani arabamin parenti yap diyoruz.

            //ARABA CARPTIGI ZAMAN PARK ALANINA BUTUN KONUM VE ROTASYONLARINI KITLE KI DEGISIK GORUNTULER OLUSMASIN
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ |
                RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ; 

             _GameManager.YeniArabaGetir();
        }

        if (collision.gameObject.CompareTag("OrtaGobek")) 
        {
            Destroy(gameObject); //Canvas cikicak // obje havuzunda arabayi false yapacagim.
        }
        else if (collision.gameObject.CompareTag("Araba"))
        {
            Destroy(gameObject); // obje havuzunda arabayi false yapacagim.
        }
    }
}
