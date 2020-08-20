using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeController : VZPlayer
{
    // Start is called before the first frame update
    protected override void Update()
    {
        
        base.Update();
        var angles = Controller.Head.eulerAngles;
        angles.x = angles.z = 0;


        transform.eulerAngles = angles;


        mRigidbody.velocity = Quaternion.Euler(0, angles.y, 0) * mRigidbody.velocity;
    }
}
