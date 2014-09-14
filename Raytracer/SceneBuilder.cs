using System;
using System.Collections.Generic;
using System.Drawing;
using Raytracer.LightSources;
using Raytracer.Samplers;
using Raytracer.SampleShapes;
using Raytracer.Shaders;

namespace Raytracer
{
    static class SceneBuilder
    {
        public static Scene GeometricFiguresBox()
        {
            var cameraPosition = new Vector3(0, 8, 5);
            var camera = new Camera(cameraPosition, Quaternion.LookRotation(new Vector3(0, -1, -1), Vector3.Up),
                8.0f / 6.0f, (float)Math.PI * 60f / 180f);

            ISampler smooth = new InterpolatedNormalSampler();
            ISampler flat = new FlatNormalSampler();
            Shader diffuseRedSmooth = new DiffuseShader(new SolidColorSampler(Color.Red));
            Shader diffuseSilver = new DiffuseShader(new SolidColorSampler(Color.Silver));
            Shader diffuseWhite = new DiffuseShader(new SolidColorSampler(Color.White));
            Shader diffuseGreenSmooth = new DiffuseShader(new SolidColorSampler(Color.Green));
            Shader glossy = new GlossyShader();
            Shader mirror = new MixShader(diffuseSilver, glossy, 0.6f);
            Shader cubeShader = new MixShader(diffuseWhite, glossy, 0.6f);
            Shader smallSphereShader = new MixShader(diffuseGreenSmooth, glossy, 0.4f);

            Mesh sphere = TriangleSphere.Create(
                new Vector3(3, 2, -6), Quaternion.Identity, 2, 32, 40, diffuseRedSmooth, smooth);
            Mesh smallSphere = TriangleSphere.Create(
                new Vector3(-2, 1, -4), Quaternion.Identity, 1, 26, 20, smallSphereShader, smooth);
            Mesh cube = Cube.Create(
                new Vector3(-2.9f, 1.5f, -8), new Quaternion(0, 1, 0, 0.33f).Normalized(), 3, cubeShader, flat);
            Mesh backWall = Plane.Create(
                new Vector3(0, 0, -10), Quaternion.Identity, Vector3.Down, Vector3.Right, 100, diffuseSilver, flat);
            Mesh frontWall = Plane.Create(
                new Vector3(0, 0, 6), Quaternion.Identity, Vector3.Up, Vector3.Right, 100, diffuseSilver, flat);
            Mesh floor = Plane.Create(
                new Vector3(0, 0, 0), Quaternion.Identity, Vector3.Forward, Vector3.Right, 100, diffuseSilver, flat);
            Mesh ceiling = Plane.Create(
                new Vector3(0, 10, 0), Quaternion.Identity, Vector3.Forward, Vector3.Left, 100, diffuseSilver, flat);
            Mesh leftWall = Plane.Create(
                new Vector3(-5, 0, 0), Quaternion.Identity, Vector3.Forward, Vector3.Down, 100, mirror, flat);
            Mesh rightWall = Plane.Create(
                new Vector3(5, 0, 0), Quaternion.Identity, Vector3.Forward, Vector3.Up, 100, mirror, flat);

            var meshes = new List<Mesh>
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
            var lightSources = new List<LightSource>
            {
                new PointLight(new Vector3(-4.5f, 7.5f, -9.5f), 2.0f, 5, Color.White),
                new PointLight(new Vector3(4.5f, 7.5f, -9.5f), 2.0f, 5, Color.White),
                cameraLightSource
            };
            return new Scene(meshes, lightSources, camera, Color.White);
        }

        public static Scene Spheres()
        {
            var cameraPosition = new Vector3(0, 1, 0);
            var camera = new Camera(cameraPosition, Quaternion.Identity, 8.0f / 6.0f, (float)Math.PI * 80f / 180f);

            ISampler smoothSampler = new InterpolatedNormalSampler();
            ISampler flatSampler = new FlatNormalSampler();

            Shader grayDiffuse = new DiffuseShader(new SolidColorSampler(Color.DarkSlateGray));
            Shader glossyShader = new GlossyShader();
            Shader floorShader = new MixShader(grayDiffuse, glossyShader, 0.8f);
            Mesh floor = Plane.Create(Vector3.Zero, Quaternion.Identity, Vector3.Forward, Vector3.Right, 100,
                floorShader, flatSampler);
            var meshes = new List<Mesh> { floor };
            var radiuses = new List<double> {1, 0.2, 1, 4, 1.1, 0.6, 0.9};
            var positions = new List<Vector2>
            {
                new Vector2(-6, 5),
                new Vector2(-0.2f, 2),
                new Vector2(-2f, 3.6f),
                new Vector2(-1f, 14),
                new Vector2(2, 3),
                new Vector2(0.7f, 2.5f),
                new Vector2(6, 13)
            };
            var colors = new List<Color>
            {
                Color.Green,
                Color.LightSeaGreen,
                Color.DarkOrange,
                Color.Orchid,
                Color.Blue,
                Color.White,
                Color.DarkViolet
            };

            for (int i = 0; i < 7; i++)
            {
                Shader shader = new MixShader(new DiffuseShader(new SolidColorSampler(colors[i])), new GlossyShader(), 0.7f);
                meshes.Add(
                    TriangleSphere.Create(
                        new Vector3(positions[i].x, (float) radiuses[i], positions[i].y), Quaternion.Identity, radiuses[i],
                        16, 16,  shader, smoothSampler));
            }

            var lights = new List<LightSource>
            {
                new DirectionalLight(2, Color.White, new Vector3(0.3f, -1f, 0.3f)),
                new PointLight(cameraPosition, 1, 10, Color.White)
            };
            return new Scene(meshes, lights, camera, Color.Black);
        }
    }
}
