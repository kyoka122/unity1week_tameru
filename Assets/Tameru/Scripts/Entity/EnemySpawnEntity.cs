namespace Tameru.Entity
{
    public class EnemySpawnEntity
    {
        public float currentPauseTime { get; private set; }
        public float currentInterval { get; private set; }

        public void SetInterval(float newInterval)
        {
            currentInterval = newInterval;
        }
        
        public void AddPassedTime(float time)
        {
            currentPauseTime += time;
        }

        public void ResetTime()
        {
            currentPauseTime = 0;
        }
        
    }
}