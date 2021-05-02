using Manager;
using UnityEngine;
using Zenject;

namespace Base
{
    public abstract class Acceptable : MonoBehaviour
    {
        protected GameManager _gameManager;
        protected BreadFactoryManager _breadFactoryManager;

        [Inject]
        private void Construct(
            GameManager gameManager,
            BreadFactoryManager breadFactoryManager
        )
        {
            _gameManager = gameManager;
            _breadFactoryManager = breadFactoryManager;
        }
    
        public virtual void OnDropped(Draggable draggable)
        {
            _breadFactoryManager.RemoveGameObject(draggable.gameObject);
        }
    }
}