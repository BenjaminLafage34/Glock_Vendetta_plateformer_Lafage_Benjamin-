using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class TurretContainerScript : MonoBehaviour
{
    public Transform turretObjectTransform;

    private void LateUpdate()
    {
        transform.position = turretObjectTransform.position;
    }
}