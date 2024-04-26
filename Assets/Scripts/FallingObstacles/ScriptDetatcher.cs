using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;


namespace FallingObstacles
{
    public sealed class ScriptDetatcher : MonoBehaviour
    {
    
        private MonoBehaviour _scriptToAttach;
    
        [SerializeField, Min(0f)] private int _affectedGroupObjectsAmount;
        [SerializeField] private List<GameObject> _objectGroup;

        private void Awake()
        {
            _scriptToAttach = GetComponents<MonoBehaviour>().Where(x => x.GetType() != this.GetType()).FirstOrDefault();
            foreach (GameObject parent in _objectGroup)
            {
                List<GameObject> children = new(
                    GetComponentsInChildren(_scriptToAttach.GetType(), false)
                        .Select(x => x.gameObject)
                );

                int childrenToDetachScriptCount = Math.Min(
                    _affectedGroupObjectsAmount,
                    children.Count
                );

                for (int i = 0; i < childrenToDetachScriptCount; i++)
                {
                    int childIndex = new Random().Next(0, children.Count);
                    GameObject child = children[childIndex];

                    Destroy(child.GetComponent(_scriptToAttach.GetType()));

                    children.Remove(children[childIndex]);
                }
            }
        }
    }
}