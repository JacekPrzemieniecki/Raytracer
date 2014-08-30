using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Raytracer.Debugging;
using Raytracer.LightSources;
using Raytracer.Parsers;
using Raytracer.Samplers;
using Raytracer.SampleShapes;
using Raytracer.Shaders;

namespace Raytracer
{
    class Scene
    {
        public readonly List<LightSource> LightSources;
        private readonly Camera _camera;
        private readonly List<Mesh> _meshes;

        public Scene(string file)
        {
            var cameraPosition = new Vector3(3, 3, 8);
            _camera = new Camera(cameraPosition, new Quaternion(0, 0, 0, 1), 8.0f / 6.0f, (float)Math.PI * 80f / 180f);

            var parser = new ObjParser();
            parser.Parse(file);
            _meshes = parser.Meshes;

            LightSource cameraLightSource = new PointLight(cameraPosition, 2, 1000, Color.White);
            LightSources = new List<LightSource>
            {
                cameraLightSource
            };
        }

        public Scene()
        {
            var cameraPosition = new Vector3(0, 8, 5);
            _camera = new Camera(cameraPosition, Quaternion.LookRotation(new Vector3(0, -0.8f, -1), Vector3.Up), 8.0f / 6.0f, (float)Math.PI * 60f / 180f);

            //Shader normal = new NormalShader();
            Shader diffuseRedSmooth = new DiffuseShader(new SolidColorSampler(Color.Red), new InterpolatedNormalSampler());
            Shader diffuseSilver = new DiffuseShader(new SolidColorSampler(Color.Silver), new FlatNormalSampler());
            Shader diffuseWhite = new DiffuseShader(new SolidColorSampler(Color.White), new FlatNormalSampler());
            Shader diffuseGreenSmooth = new DiffuseShader(new SolidColorSampler(Color.Green), new InterpolatedNormalSampler());
            Shader glossySmooth = new GlossyShader(new InterpolatedNormalSampler());
            Shader glossyFlat = new GlossyShader(new FlatNormalSampler());
            Shader mirror = new MixShader(diffuseSilver, glossyFlat, 0.6f);
            Shader cubeShader = new MixShader(diffuseWhite, glossyFlat, 0.6f);
            //Shader sphereShader = new MixShader(diffuseRedSmooth, glossySmooth, 0.9f);
            Shader smallSphereShader = new MixShader(diffuseGreenSmooth, glossySmooth, 0.4f);
            //Shader testShader = new TestShader(new FlatNormalSampler());
            //Shader position = new PositionShader();
            //Shader reflected = new ReflectedVectorShader(new FlatNormalSampler());

            Mesh sphere = TriangleSphere.Create(new Vector3(3, 2, -6), 2.0, 32, 40, diffuseRedSmooth);
            Mesh smallSphere = TriangleSphere.Create(new Vector3(-1, 1, -4), 1, 26, 20, smallSphereShader);
            Mesh cube = Cube.Create(new Vector3(-3, 1.5f, -8), 3, cubeShader);
            Mesh backWall = Plane.Create(new Vector3(0, 0, -10), Vector3.Down, Vector3.Right, 100, diffuseSilver);
            Mesh frontWall = Plane.Create(new Vector3(0, 0, 6), Vector3.Up, Vector3.Right, 100, diffuseSilver);
            Mesh floor = Plane.Create(new Vector3(0, 0, 0), Vector3.Forward, Vector3.Right, 100, diffuseSilver);
            Mesh ceiling = Plane.Create(new Vector3(0, 10, 0), Vector3.Forward, Vector3.Left, 100, diffuseSilver);
            Mesh leftWall = Plane.Create(new Vector3(-5, 0, 0), Vector3.Forward, Vector3.Down, 100, mirror);
            Mesh rightWall = Plane.Create(new Vector3(5, 0, 0), Vector3.Forward, Vector3.Up, 100, mirror);

            _meshes = new List<Mesh>
            {
                cube,
                sphere,
                smallSphere,
                backWall,
                frontWall,
                floor,
                ceiling,
                leftWall,
                rightWall
            };

            LightSource cameraLightSource = new PointLight(cameraPosition, 3, 10, Color.White);
            LightSources = new List<LightSource>
            {
                //new PointLight(new Vector3(-4.5f, 0.5f, -9.5f), 2.0f, 5, Color.White),
                //new PointLight(new Vector3(4.5f, 0.5f, -9.5f), 2.0f, 5, Color.White),
                new PointLight(new Vector3(-4.5f, 7.5f, -9.5f), 2.0f, 5, Color.White),
                new PointLight(new Vector3(4.5f, 7.5f, -9.5f), 2.0f, 5, Color.White),
                cameraLightSource,
                //new PointLight(new Vector3(0, 1, -3.9f), 3, Color.White ),
            };
        }

        public bool IsVisible(Vector3 from, Vector3 to)
        {
            Vector3 direction = to - from;
            float distance = direction.Length();
            var ray = new Ray(from, direction);
            return IsVisible(ray, distance);
        }

        public bool IsVisible(Ray ray, float distance)
        {
            float dist = distance - 0.001f;
            foreach (Mesh mesh in _meshes)
            {
#if DEBUG
                Interlocked.Increment(ref Counters.BoundingBoxHits);
#endif
                RaycastHit meshHit = null;
                bool hitFound = mesh.Raycast(ray, dist, ref meshHit);
                if (hitFound)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Raycast(Ray ray, float maxDistance, ref RaycastHit hitInfo)
        {
            float closestHitDistance = maxDistance;
            bool hitFound = false;
            foreach (Mesh mesh in _meshes)
            {
#if DEBUG
                Interlocked.Increment(ref Counters.BoundingBoxHits);
#endif
                RaycastHit meshHit = null;
                bool hit = mesh.Raycast(ray, closestHitDistance, ref meshHit);
                if (hit)
                {
                    hitInfo = meshHit;
                    closestHitDistance = meshHit.Distance;
                    hitFound = true;
                }
            }
            return hitFound;
        }

        public Vector3 SampleColor(float viewportX, float viewportY, int maxRecursiveRaycasts)
        {
            Ray ray = _camera.ViewportPointToRay(viewportX, viewportY);
            return SampleColor(ray, maxRecursiveRaycasts);
        }

        public Vector3 SampleColor(Ray ray, int maxRecursiveRaycasts)
        {
            RaycastHit raycastHit = null;
            bool hitFound = Raycast(ray, float.MaxValue, ref raycastHit);
            if (!hitFound)
            {
                return new Vector3(1, 1, 1);
            }
            return raycastHit.Mesh.SampleColor(this, raycastHit, maxRecursiveRaycasts);
        }
    }
}