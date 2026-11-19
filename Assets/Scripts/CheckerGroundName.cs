using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerGroundName : MonoBehaviour
{
    public int GroundInt;
    //1 = Sand, 0 = Grass, 2 = Stone, 3 = Wood

    public LayerMask grass;
    public LayerMask sand;
    public LayerMask asphalt;
    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "GrassGround")
        {
            GroundInt = 0;
        } else if (other.tag == "SandGround")
        {
            GroundInt = 1;
        } else if (other.tag == "AsphaltGround")
        {
            GroundInt = 2;
        } else if (other.tag == "WoodGround")
        {
            GroundInt = 3;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "GrassGround")
        {
            GroundInt = 0;
        }
        else if (other.tag == "SandGround")
        {
            GroundInt = 1;
        }
        else if (other.tag == "AsphaltGround")
        {
            GroundInt = 2;
        }
        else if (other.tag == "WoodGround")
        {
            GroundInt = 3;
        }
    }
    */
    private void Update()
    {
        if(Physics.CheckSphere(transform.position, 0.4f, grass) == true)
        {
            GroundInt = 0;
        } else if (Physics.CheckSphere(transform.position, 0.4f, sand) == true)
        {
            GroundInt = 1;
        }
        else if (Physics.CheckSphere(transform.position, 0.4f, asphalt) == true)
        {
            GroundInt = 2;
        }
    }
    public string GetGround()
    {
        if(GroundInt == 0)
        {
            return "GrassGround";
        }
        else if(GroundInt == 1)
        {
            return "SandGround";
        }
        else if (GroundInt == 2)
        {
            return "AsphaltGround";
        }
        else
        {
            return null;
        }
    }
}
