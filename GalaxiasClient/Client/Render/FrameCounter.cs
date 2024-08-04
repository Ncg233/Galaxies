using System.Collections.Generic;
using System.Linq;

namespace Galaxias.Client.Render
{
    public class FrameCounter
    {
        public long TotalFrames { get; private set; }
        public float TotalSeconds { get; private set; }
        public float AverageFramesPerSecond { get; private set; }
        public float CurrentFramesPerSecond { get; private set; }
        public void Update(float deltaTime)
        {
            CurrentFramesPerSecond = 1f / deltaTime;
            _sampleBuffer.Enqueue(CurrentFramesPerSecond);
            if (_sampleBuffer.Count > 100)
            {
                _sampleBuffer.Dequeue();
                AverageFramesPerSecond = _sampleBuffer.Average((i) => i);
            }
            else
            {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }
            long totalFrames = TotalFrames;
            TotalFrames = totalFrames + 1L;
            TotalSeconds += deltaTime;
        }
        public const int MaximumSamples = 100;
        private Queue<float> _sampleBuffer = new Queue<float>();
    }
}
