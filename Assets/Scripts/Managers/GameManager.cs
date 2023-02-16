using DesignPatterns;
using ScriptableObjects.Balance;
using UnityEngine;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {

        public bool IsGame { get; private set; }

        public UnitsBalanceSO unitsBalance;
        public FriendshipPropsSO friendshipProps;

        public AlliesCollection alliesCollection;
        public GuardiansCollections guardiansCollections;
        
        protected override void Awake()
        {
            base.Awake();

            alliesCollection = new AlliesCollection();
            guardiansCollections = new GuardiansCollections();
            
            guardiansCollections.noObjectsLeft.AddListener(StopGame);

            IsGame = true;
        }

        public void StopGame()
        {
            IsGame = false;
            Debug.Log($"LEVEL COMPLETED!!!");
        }
    }
}