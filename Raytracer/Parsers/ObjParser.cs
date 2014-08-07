using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using Raytracer.Samplers;
using Raytracer.Shaders;

namespace Raytracer.Parsers
{
    class ObjParser
    {
        public List<Mesh> Meshes { get; set; }
        private List<Vector3> _vertices;
        private List<Triangle> _triangles;
        private int _vertexOffset = 1;
        private Dictionary<string, Action<string[]>> _actionMap;
        private string _currentMeshName;

        public ObjParser()
        {
            Meshes = new List<Mesh>();
            _vertices = new List<Vector3>();

            _actionMap = new Dictionary<string, Action<string[]>>
            {
                {"o", ParseObject},
                {"v", ParseVertex},
                {"vt", NotImplemented},
                {"vn", Ignore},
                {"vp", Ignore},
                {"f", ParseFace},
                {"mtlib", NotImplemented},
                {"usemtl", NotImplemented},
                {"g", NotImplemented},
                {"s", NotImplemented}
            }; 
        }

        private void InitBuffers()
        {
            _vertexOffset += _vertices.Count;
            _vertices = new List<Vector3>();
            _triangles = new List<Triangle>();

        }

        public void Parse(StreamReader file)
        {
            InitBuffers();
            string line;
            while ((line = file.ReadLine()) != null)
            {
                ParseLine(line.Split(' '));
            }
            WrapMesh();
        }


        private void ParseLine(string[] line)
        {
            if (line.Length == 0) { } // empty line
            else if (line[0].StartsWith("#")) { } // comment
            else if (_actionMap.ContainsKey(line[0])) // command
            {
                _actionMap[line[0]](line);
            }
            else
            {
                throw new Exception("OBJ parsing error, unrecognized command: " + string.Join(" ", line));
            }
        }

        private void ParseFace(string[] line)
        {
            if (line.Length == 4)
            {
                ParseTriangle(line);
            }
            else if (line.Length == 5)
            {
                ParseQuad(line);
            }
            else
            {
                throw new Exception("OBJ parsing error, unrecognized command: " + string.Join(" ", line));
            }
        }

        private void ParseTriangle(string[] line)
        {
            int v3 = int.Parse(line[1]) - _vertexOffset;
            int v2 = int.Parse(line[2]) - _vertexOffset;
            int v1 = int.Parse(line[3]) - _vertexOffset;
            AddTriangle(v1, v2, v3);
        }

        private void ParseQuad(string[] line)
        {
            int v4 = int.Parse(line[1]) - _vertexOffset;
            int v3 = int.Parse(line[2]) - _vertexOffset;
            int v2 = int.Parse(line[3]) - _vertexOffset;
            int v1 = int.Parse(line[4]) - _vertexOffset;
            AddTriangle(v1, v2, v3);
            AddTriangle(v3, v4, v1);
        }

        private void AddTriangle(int v1, int v2, int v3)
        {
            _triangles.Add(new Triangle(v1, v2, v3));
        }

        private void ParseVertex(string[] line)
        {
            float x = float.Parse(line[1], NumberStyles.Any, CultureInfo.InvariantCulture);
            float y = float.Parse(line[2], NumberStyles.Any, CultureInfo.InvariantCulture);
            float z = float.Parse(line[3], NumberStyles.Any, CultureInfo.InvariantCulture);
            _vertices.Add(new Vector3(x, y, z));
        }

        private void ParseObject(string[] line)
        {
            if (_currentMeshName != null)
            {
                WrapMesh();
            }
            _currentMeshName = line[1];
        }

        private void WrapMesh()
        {
            var newMesh = new Mesh(_vertices.ToArray(), _triangles.ToArray(), Vector3.Zero,
                new DiffuseShader(new SolidColorSampler(Color.Red), new FlatNormalSampler()));
            Meshes.Add(newMesh);
            InitBuffers();
        }

        private void NotImplemented(string[] s)
        {
            
        }

        private void Ignore(string[] s)
        {
            
        }
    }
}
