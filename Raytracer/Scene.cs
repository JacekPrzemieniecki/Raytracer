using System;
using System.Collections.Generic;

namespace Raytracer
{
    internal class Scene
    {
        public List<Mesh> Meshes;

        public Scene()
        {
            Meshes = new List<Mesh>
            { 
            /*new Mesh(new Vector3[]
                {
                    new Vector3(0, 3, -7),
                    new Vector3(0, 1, -5),    //1
                    new Vector3(1, 1, -7),
                    new Vector3(0, 1, -9),    //3  
                    new Vector3(-1, 1, -7),   
                    new Vector3(0, 0.5f, -4),//5   
                    new Vector3(1.5f, 0.5f, -7),   
                    new Vector3(0, 0.5f, -10), //7  
                    new Vector3(-1.5f, 0.5f, -7)                       
                }, 
            new[] {
            0, 1, 2, 
            0, 2, 3, 
            0, 3, 4, 
            0, 4, 1, 
            1, 2, 5, 
            2, 6, 5, 
            2, 3, 6, 
            3, 7, 6, 
            3, 4, 7, 
            4, 8, 7, 
            4, 1, 8, 
            1, 5, 8
            })  
            };*/
            new TriangleSphere(new Vector3(0, 0, -5), 1.0, 10, 10)};
            Camera = new Camera(8.0f / 6.0f, (float)Math.PI * 60f / 180f);
        }

        public Camera Camera { get; set; }
    }
}