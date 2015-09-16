using System;
using System.Threading;
#if DEBUG
using Raytracer.Debugging;
#endif

namespace Raytracer
{
    internal class RenderWorker
    {
        private const float RenderedThreshold = 0.00001f;
        private readonly Random _rng = new Random();
        private RenderJob _currentJob;
        private bool _keepRunning = true;
        private bool _pause;
        private bool[] _renderedMask = new bool[0];

        public RenderJob CurrentJob
        {
            get { return _currentJob; }
            set
            {
                _currentJob = value;
                if (_renderedMask.Length != _currentJob.Output.Length)
                {
                    _renderedMask = new bool[_currentJob.Output.Length];
                }
                else
                {
                    for (var i = 0; i < _renderedMask.Length; i++)
                    {
                        _renderedMask[i] = false;
                    }
                }
            }
        }

        public void Start()
        {
            var t = new Thread(Render)
            {
                IsBackground = true
            };
            t.Start();
        }

        public void Pause(bool pause)
        {
            _pause = pause;
        }

        private void Render()
        {
            while (_keepRunning)
            {
                if (_pause)
                {
                    Thread.Sleep(0);
                }
                var pass = 0;
                while (_currentJob.PassesLeft > 0)
                {
                    var i = 0;
                    for (var y = _currentJob.StartY; y < _currentJob.Height + _currentJob.StartY; y++)
                    {
                        for (var x = _currentJob.StartX; x < _currentJob.Width + _currentJob.StartX; x++)
                        {
                            if (_renderedMask[i])
                            {
#if DEBUG
                                Interlocked.Increment(ref Counters.RaycastsSkipped);
#endif
                                continue;
                            }

                            var pixelColor = RenderPixel(_currentJob.Scene, x, y, (float) _rng.NextDouble(),
                                (float) _rng.NextDouble(), _currentJob.PictureHeight, _currentJob.PictureWidth);
                            if (pass > 0)
                            {
                                var lastColor = _currentJob.Output[i];
                                pixelColor = (lastColor*pass + pixelColor)/(pass + 1);
                                _renderedMask[i] = (lastColor - pixelColor).LengthSquared() < RenderedThreshold;
                            }
                            _currentJob.Output[i] = pixelColor;
                            i++;
                        }
                        if (!_keepRunning)
                        {
                            return;
                        }
                    }
                    pass++;
                    _currentJob.PassesLeft--;
                }
            }
        }

        private Vector3 RenderPixel(Scene scene, int screenX, int screenY, float dX, float dY, int pictureHeight,
            int pictureWidth)
        {
            var viewportX = (screenX + dX)/pictureWidth*2 - 1;
            // Screen Y is pointed down
            var viewportY = 1 - (screenY + dY)/pictureHeight*2;
            return scene.SampleColor(viewportX, viewportY, 4);
        }

        public void Stop()
        {
            _keepRunning = false;
        }
    }
}