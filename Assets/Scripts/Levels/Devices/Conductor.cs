using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : Device
{    
    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Photon")
        {
            Photon photon = other.GetComponent<Photon>();
            photon.MoveToward(this.front);
            photon.UpdateClock(clk);

            photon.transform.position = new Vector3(
                transform.position.x,
                1f,
                transform.position.z
            );
        }  
    }
}