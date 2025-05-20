using UnityEngine;
using UnityEngine.Events;

namespace Proc
{
    public class Timer
    {
        public UnityEvent OnTimesUp = new UnityEvent();

        private bool m_IsPause;

        private float m_PassedTime;
        private float m_Time;

        public bool IsPause => m_IsPause;
        public float PassedTime => m_PassedTime;
        public float CurrentTime => m_Time - m_PassedTime;

        public Timer(float time, float passedTime = 0)
        {
            m_PassedTime = passedTime;
            m_Time = time;
        }

        public void SetTime(float time)
        {
            m_PassedTime -= time;
            
            if (m_PassedTime < 0)
                m_PassedTime = 0;
        }

        public void Reset()
        {
            m_PassedTime = 0;
            m_IsPause = false;
        }
        public void Pause()
        {
            m_IsPause = true;
        }
        public void Play()
        {
            m_IsPause = false;
        }

        public void Update(bool isScaled = true)
        {
            if (!m_IsPause)
            {
                m_PassedTime += isScaled ? Time.deltaTime : Time.unscaledDeltaTime;

                if (m_PassedTime >= m_Time)
                {
                    m_IsPause = true;
                    OnTimesUp?.Invoke();
                }
            }
        }
    }
}
