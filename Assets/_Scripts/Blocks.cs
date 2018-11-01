using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour {
    public bool readyToBuild = true;

    protected void OnTriggerStay2D(Collider2D collision)
    {
        readyToBuild = false;
    }
    protected void OnTriggerExit2D(Collider2D collision)
    {
        readyToBuild = true;
    }
}
