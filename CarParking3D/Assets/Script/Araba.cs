using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Araba : MonoBehaviour
{
    //Trail Renderer kullanarak arabama teker izi kattim.

    public bool ilerle;
    //
    //
    public GameObject[] Tekerizleri;
    public Transform parent; // Bir ust cismin icine arabayi atalim ki arabada platform ile beraber donsun

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ilerle)
            transform.Translate(15f * Time.deltaTime * transform.forward); // arabaya ileri dogru bu hizda ilerlet.
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Parking")) //eger tagi Parking olan bir cisme carparsa
        {
            ilerle = false;
            Tekerizleri[0].SetActive(false); //Teker izlerini parking box coliderina carpinca kapat.
            Tekerizleri[1].SetActive(false); // Teker izlerini parking box coliderina carpinca kapat.

            transform.SetParent(parent); // disardan verdigim objeyi ana cismimin yani arabamin parenti yap diyoruz.
        }

        if (collision.gameObject.CompareTag("OrtaGobek")) 
        {
            Destroy(gameObject); // obje havuzunda arabayi false yapacagim.
        }
    }
}
