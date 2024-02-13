using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CarMover : MonoBehaviour
{
    public Transform boxBorder;
    public TextMeshProUGUI infoText;

    public float startingSpeed;

    public float speed;
    public float acceleration;
    public float minSpeed;
    public float maxSpeed;

    public bool run;

    private void Start()
    {
        speed = startingSpeed;
    }

    private void Update()
    {

        if (run)
        {
            speed += acceleration * Time.deltaTime;
            
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }

            if (speed < minSpeed)
            {
                speed = minSpeed;
            }

            GetComponent<Rigidbody2D> ().velocity = new Vector3(
                speed,
                GetComponent<Rigidbody2D> ().velocity.y
            );
        }

        infoText.text = "Begin snelheid: " + startingSpeed * 3.6f + "km/uur\nAfstand tot doos: " +
                        "78 meter\nVul de versnelling in" +
                        " zodat de auto net op tijd stopt voor de doos\n";

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "box")
        {
            LevelController.Reset = true;
        }
    }

    /*
     * Bij een eenparig vertraagde beweging vertraagd een voorwerp met een constante vertraging, waardoor de snelheid
     * per seconde steeds met dezelfde hoeveelheid afneemt. De eindsnelheid kan je altijd met de
     * volgende formule berekenen:
       ve = vb + a x t
       ve is de eindsnelheid in meter per seconde (m/s).
       vb is de beginsnelheid in meter per seconde (m/s).
       a is de versnelling in meter per seconde kwadraat (m/s2)
       t is de tijdsduur in seconde (s)
       
     */
}
