using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public interface IEntityController
    {
        Transform GetTransform();
    }
}