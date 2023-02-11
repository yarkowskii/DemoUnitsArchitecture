using ScriptableObjects.Balance;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public bool IsGame { get; private set; }

        public UnitsBalanceSO unitsBalance;
        public FriendshipPropsSO friendshipProps;

        public AlliesCollection alliesCollection;
        public GuardiansCollections guardiansCollections;
        
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

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