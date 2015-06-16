using System;
using System.Collections.Generic;
using System.Drawing;

namespace Raytracer
{
    internal class Renderer
    {
        private const int ChunkSize = 64;
        private int _pictureHeight;
        private int _pictureWidth;

        private List<RenderJob> PrepareJobs(Scene scene, int passes)
        {
            var list = new List<RenderJob>();
            var pixelsYleft = _pictureHeight;
            for (var chunkY = 0; chunkY < _pictureHeight/ChunkSize + 1; chunkY++)
            {
                var pixelsXleft = _pictureWidth;
                var chunkHeight = pixelsYleft > ChunkSize ? ChunkSize : pixelsYleft;
                for (var chunkX = 0; chunkX < _pictureWidth/ChunkSize + 1; chunkX++)
                {
                    list.Add(new RenderJob(
                        x: chunkX*ChunkSize,
                        y: chunkY*ChunkSize,
                        width: pixelsXleft > ChunkSize ? ChunkSize : pixelsXleft,
                        height: chunkHeight,
                        passes: passes,
                        scene: scene,
                        pictureWidth: _pictureWidth,
                        pictureHeight: _pictureHeight));

                    pixelsXleft -= ChunkSize;
                }
                pixelsYleft -= ChunkSize;
            }

            return list;
        }

        public void Render(Bitmap bmp, Scene scene, Action refreshAction, int passes, ref bool stopFlag)
        {
            _pictureHeight = bmp.Height;
            _pictureWidth = bmp.Width;
            var renderedMask = new bool[_pictureWidth][];
            for (var i = 0; i < _pictureWidth; i++)
            {
                renderedMask[i] = new bool[_pictureHeight];
            }

            var waitingJobs = PrepareJobs(scene, passes);
            var jobsDone = 0;
            var jobsAssigned = 0;
#if DEBUG

            var processorCount = 1;
#else
            var processorCount =  Environment.ProcessorCount;
#endif
            var workers = new List<RenderWorker>(processorCount);
            var lastPassCount = new int[processorCount];
            for (var i = 0; i < processorCount; i++)
            {
                var renderWorker = new RenderWorker();
                workers.Add(renderWorker);
                renderWorker.CurrentJob = waitingJobs[i];
                renderWorker.Start();
                jobsAssigned++;
            }

            while (jobsDone < waitingJobs.Count)
            {
                for (int workerID = 0; workerID < workers.Count; workerID++)
                {
                    var renderWorker = workers[workerID];
                    var passesLeft = renderWorker.CurrentJob.PassesLeft;
                    if (passesLeft != 0)
                    {
                        if( lastPassCount[workerID] != passesLeft)
                        {
                            BlipToScreen(renderWorker.CurrentJob, bmp);
                        }
                        refreshAction();
                        continue;
                    }

                    var doneJob = renderWorker.CurrentJob;
                    jobsDone++;
                    if (jobsAssigned >= waitingJobs.Count)
                    {
                        renderWorker.Stop();
                    }
                    else
                    {
                        renderWorker.Pause(true);
                        renderWorker.CurrentJob = waitingJobs[jobsAssigned];
                        jobsAssigned++;
                        renderWorker.Pause(false);
                    }
                    BlipToScreen(doneJob, bmp);
                }
                if (stopFlag)
                {
                    foreach (var renderWorker in workers)
                    {
                        renderWorker.Stop();
                        return;
                    }
                }
            }
        }

        private void BlipToScreen(RenderJob job, Bitmap bmp)
        {
            var i = 0;
            for (var y = job.StartY; y < job.StartY + job.Height; y++)
            {
                for (var x = job.StartX; x < job.StartX + job.Width; x++)
                {
                    var output = job.Output[i];
                    if (output == null)
                    {
                        return;
                    }
                    bmp.SetPixel(x, y, output.ToColor());
                    i++;
                }
            }
        }
    }
}