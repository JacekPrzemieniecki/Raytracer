﻿using System;
using System.Drawing;
using Raytracer.LightSources;
using Raytracer.Samplers;

namespace Raytracer.Shaders
{
    internal class DiffuseShader : Shader
    {
        private ISampler _textureSampler;
        private ISampler _normalSampler;

        public DiffuseShader(ISampler textureSampler, ISampler normalSampler)
        {
            _textureSampler = textureSampler;
            _normalSampler = normalSampler;
        }

        public override Vector3 Shade(Scene scene, RaycastHit hitInfo, int maxRecursiveRaycasts)
        {
            float totalIntensity = 0;
            Vector3 normal = _normalSampler.Sample(hitInfo);
            foreach (LightSource lightSource in scene.LightSources)
            {
                Color c;
                totalIntensity += lightSource.IntensityAt(hitInfo.Position, normal, scene, out c);
            }
            Vector3 diffuse = _textureSampler.Sample(hitInfo);
            return diffuse * totalIntensity;
        }
    }
}