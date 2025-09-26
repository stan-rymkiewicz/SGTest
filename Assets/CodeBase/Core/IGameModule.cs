using UnityEngine;

namespace CodeBase.Core
{
    public interface IGameModule
    {
        public void Exit();
        public void OnClose();
        public void Initialize();
        public void StartGameLoop();

        public void UpdateGameLoop(float deltaTime);
    }

    public abstract class GameModuleController : MonoBehaviour , IGameModule
    {
        
        private void Awake()
        {
            Initialize();
        }
        
        private void Update()
        {
            UpdateGameLoop(Time.deltaTime);
        }
        
        public virtual void Exit()
        {
        }

        public virtual void OnClose()
        {
        }

        public virtual void Initialize()
        {
        }

        public virtual void StartGameLoop()
        {
        }

        public virtual void UpdateGameLoop(float deltaTime)
        {
        }
    }
}