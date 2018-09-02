using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Core
{
    public class ToggleTransform : MonoBehaviour
    {
        public void ToggleActive()
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
        }
    }
}