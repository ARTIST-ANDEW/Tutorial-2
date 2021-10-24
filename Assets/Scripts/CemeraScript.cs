using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CemeraScript : MonoBehaviour
{
    public GameObject target;
    
    void LateUpdate()
    {
        this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, this.transform.position.z);   
    }
}
