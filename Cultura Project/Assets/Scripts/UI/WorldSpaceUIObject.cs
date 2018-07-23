using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cultura.UI
{
    public class WorldSpaceUIObject : MonoBehaviour
    {
        [SerializeField]
        private Transform linkedUIelement;

        private Transform worldSpaceCanvas;

        private void Start()
        {
            worldSpaceCanvas = GameObject.FindGameObjectWithTag("WorldCanvas").transform;
        }

        private void OnDestroy()
        {
            if (linkedUIelement != null)
                Destroy(linkedUIelement.gameObject);
        }
    }
}