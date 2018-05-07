﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Npc.Ally;
using UnityEngine.UI;
namespace Npc
{
    namespace Enemy
    {
        public class Zombie : Npcs
        {
            public GameObject go;
            public ZombieInfo gz;
            public Gusto gusto;
            public GameObject playerObject;
            public bool convert = false;
            GameManager gm;
            public  GameObject IntansCanvas;


            private void Start()
            {
                //se obtiene el gamemanager 
                gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
                // se intancia el canvas para manejar el UI
                IntansCanvas = Instantiate(gm.canvasZombie, gameObject.transform.position, Quaternion.identity);
                IntansCanvas.transform.SetParent(gameObject.transform);
                IntansCanvas.transform.position = transform.position;
                //se obtiene un go y se le agregan las propiedades de zombie
                go = gameObject;
                go.name = "Zombie";
                int numColor = Random.Range(0, 3);
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;

                switch (numColor)
                {
                    case 0:
                        go.GetComponent<Renderer>().material.color = Color.cyan;
                        break;
                    case 1:
                        go.GetComponent<Renderer>().material.color = Color.magenta;
                        break;
                    case 2:
                        go.GetComponent<Renderer>().material.color = Color.green;
                        break;
                }

                gusto = (Gusto)Random.Range(0, 5);
                gz.gustoZombie = gusto;
            }
            Text zombieMenssage;
            // se modifica el texto  del zombie y se verifica si hay que desactivarlo o activarlo
            public void DisplayWrite(bool id)
            {
                if (id == true)
                {
                    Text[] tx;
                    tx = IntansCanvas.transform.Find("ZombieText").GetComponents<Text>();
                    IntansCanvas.SetActive(true);
                    zombieMenssage = tx[0];
                    zombieMenssage.text = "Arrrrrr quiero comer: " + gz.gustoZombie.ToString();
                }

                if (id == false)
                {
                    IntansCanvas.SetActive(false);
                }

            }
            //si un zombie collisiona con un ciudadano 
            private void OnCollisionEnter(Collision collision)
            {
                // verificamos que sea el citizen Citizen se convierte le agrega el componente zombie  y se le dice que es convertido 
                if (collision.gameObject.GetComponent<Citizen>())
                {
                    Citizen cit = collision.gameObject.GetComponent<Citizen>();
                    Zombie z = (Zombie)cit;
                    z.convert = true;
                   
                }
            }
        }
    }
}
