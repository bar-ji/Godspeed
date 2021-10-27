using UnityEngine;

namespace Scriptable_Objs
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/World Data", order = 2)]
    public class WorldData : ScriptableObject
    {
        public new string name;
        public int worldNumber;
        public int levelAmount;
        public GameObject door;
    }
}
