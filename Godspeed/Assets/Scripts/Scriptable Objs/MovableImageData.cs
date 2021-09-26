using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scriptable_Objs
{
    [CreateAssetMenu(fileName = "New MovableImageData", menuName = "Movable Image Data", order = 51)]
    public class MovableImageData : ScriptableObject
    {
        public Vector2 startPos;
        public Vector2 endPos;
        public float time;
        public Ease easeType;
    }
}
