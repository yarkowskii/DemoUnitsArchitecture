using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UnitsSystem.Base
{
    public enum AnimationType
    {
        Attack,
        Idle,
        Die,
        AttackIdle,
        Run,
    }

    [Serializable]
    public class AnimationData
    {
        public AnimationType animationType;
        public string animationTrigger;
        public int animationVariants;

        public float animationLength;
    }

    public class CharacterAnimationController : MonoBehaviour
    {
        [SerializeField] private List<AnimationData> movingAnimationData;
        [SerializeField] private Animator animator;

        public void ChangeAnimation(AnimationType animationType)
        {
            var animationData = GetAnimationData(animationType);

            if (animationData == null)
            {
                Debug.LogWarning($"There is no animation data with [{animationType}] type");
                return;
            }
        
            if(animationData.animationVariants > 0)
                animator.SetInteger(animationData.animationTrigger + "Type", Random.Range(0, animationData.animationVariants));
        
            animator.SetTrigger(animationData.animationTrigger);
        }
        public float GetAnimationLength(AnimationType animationType)
        {
            var animationData = GetAnimationData(animationType);

            if (animationData != null) return animationData.animationLength;
            
            Debug.LogWarning($"There is no animation data with [{animationType}] type");
            return 0;
        }
    
        private AnimationData GetAnimationData(AnimationType animationType)
        {
            var animationData = movingAnimationData.Find(a => a.animationType == animationType);

            if (animator == null)
            {
                Debug.LogWarning("No animator asigned");
                return null;
            }
            
            return animationData;
        }
    
    
    }
}