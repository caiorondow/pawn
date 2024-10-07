using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenderDoor : Device
{
    public GameObject prefabPhoton;

    public override void PerformAction()
    { 
        var pos = transform.position;

        Photon photon = Instantiate(
            prefabPhoton, 
            new Vector3(pos.x, 1f, pos.z), 
            transform.rotation,
            transform.parent
        ).GetComponent<Photon>();

        photon.MoveToward(Vector3.forward);
        photon.UpdateClock(clk);
    }
}