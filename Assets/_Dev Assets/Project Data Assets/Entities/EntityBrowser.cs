using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EntityData
{
    /// <summary>
    /// Used to store prefabs of each entity and allow them to be built.
    /// </summary>
    [CreateAssetMenu(fileName = "EntityBrowser", menuName = "Scriptable Objects/EntityBrowser")]
    public class EntityBrowser : SerializedScriptableObject
    {
        [SerializeField]
        private Dictionary<string, GameObject> m_entityBrowser = new();

        public GameObject BrowseForEntity(string key)
        {
            if (!m_entityBrowser.TryGetValue(key, out GameObject prefab))
                return null;
            
            return prefab;
        }
}
}