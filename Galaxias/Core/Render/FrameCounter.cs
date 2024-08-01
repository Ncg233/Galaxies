using System.Collections.Generic;
using System.Linq;

namespace Galasias.Core.Render
{
    public class FrameCounter
    {
        public long TotalFrames { get; private set; }
        public float TotalSeconds { get; private set; }
        public float AverageFramesPerSecond { get; private set; }
        public float CurrentFramesPerSecond { get; private set; }
        public void Update(float deltaTime)
        {
            this.CurrentFramesPerSecond = 1f / deltaTime;
            this._sampleBuffer.Enqueue(this.CurrentFramesPerSecond);
            if (this._sampleBuffer.Count > 100)
            {
                this._sampleBuffer.Dequeue();
                this.AverageFramesPerSecond = this._sampleBuffer.Average((float i) => i);
            }
            else
            {
                this.AverageFramesPerSecond = this.CurrentFramesPerSecond;
            }
            long totalFrames = this.TotalFrames;
            this.TotalFrames = totalFrames + 1L;
            this.TotalSeconds += deltaTime;
        }
        public const int MaximumSamples = 100;
        private Queue<float> _sampleBuffer = new Queue<float>();
    }
}
