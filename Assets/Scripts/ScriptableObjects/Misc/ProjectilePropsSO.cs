using UnityEngine;

namespace ScriptableObjects.Misc
{
    [CreateAssetMenu(fileName = "New Projectile Props", menuName = "ProjectileProps", order = 0)]
    public class ProjectilePropsSO : ScriptableObject
    {
        public float speed;
        public float reachedTargetDistanceThreshold;
    }
}