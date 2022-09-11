using UnityEngine;

namespace LiftGame
{
    public class FrameRateLimiter : MonoBehaviour
    {
        public static FrameRateLimiter Instance;
    
        void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        
        }

        // Update is called once per frame
        void Update()
        {
            if (!Instance)
            { 
                Instance = this;
            }
        }
    }
}
