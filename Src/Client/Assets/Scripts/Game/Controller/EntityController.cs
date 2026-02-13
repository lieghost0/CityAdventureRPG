using System.Collections;
using System.Collections.Generic;
using Entity;
using UnityEngine;

public class EntityController : MonoBehaviour, IEntityController
{
    public Transform GetTransform()
    {
        return this.transform;
    }
}
