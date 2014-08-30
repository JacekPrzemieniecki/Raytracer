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
        public List<Mesh> Meshes { get; private set; }
        private List<Vector3> _vertices;
        private List<Triangle> _triangles;
        private List<Vector2> _uvs;
        private bool _uvMapped = true;
        private bool _useSmoothShading;
        private int _vertexOffset = 1;
        private int _uvOffset = 1;
        private readonly Dictionary<string, Action<string[]>> _actionMap;
        private Dictionary<string, Bitmap> _materials; 
        private string _currentMeshName;
        private string _currentMaterialName;
        private string _path;

        public ObjParser()
        {
            Meshes = new List<Mesh>();
            _vertices = new List<Vector3>();
            _uvs = new List<Vector2>();
            _materials = new Dictionary<string, Bitmap>();

            _actionMap = new Dictionary<string, Action<string[]>>
            {
                {"o", ParseObject},
                {"v", ParseVertex},
                {"vt", ParseTextureVertex},
                {"vn", Ignore},
                {"vp", Ignore},
                {"f", ParseFace},
                {"mtllib", ParseMtLib},
                {"usemtl", line => { _currentMaterialName = line[1]; }},
                {"g", NotImplemented},
                {"s", ParseSmoothShading}
            }; 
        }

        private void InitBuffers()
        {
            _vertexOffset += _vertices.Count;
            _uvOffset += _uvs.Count;
            _vertices = new List<Vector3>();
            _triangles = new List<Triangle>();
            _uvs = new List<Vector2>();
            _uvMapped = true;
            _useSmoothShading = false;
        }

        public void Parse(string filePath)
        {
            _path = Path.GetDirectoryName(filePath);
            using (var file = new StreamReader(filePath))
            {
                InitBuffers();
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    ParseLine(line.Split(' '));
                }
            }
            WrapMesh();
        }

        private void ParseTextureVertex(string[] line)
        {
            float x = float.Parse(line[1], NumberStyles.Any, CultureInfo.InvariantCulture);
            float y = 1 - float.Parse(line[2], NumberStyles.Any, CultureInfo.InvariantCulture);
            _uvs.Add(new Vector2(x, y));
            
        }

        private void ParseLine(string[] line)
        {
            if (line[0] == "") { } // empty line
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
            int uv3;
            int v3 = ParseIndexVertUV(line[1], out uv3);
            int uv2;
            int v2 = ParseIndexVertUV(line[2], out uv2);
            int uv1;
            int v1 = ParseIndexVertUV(line[3], out uv1);
            AddTriangle(v1, v2, v3, uv1, uv2, uv3);
        }

        private void ParseQuad(string[] line)
        {
            ParseTriangle(new[] {"f", line[1], line[2], line[3]});
            ParseTriangle(new [] {"f", line[3], line[4], line[1]});
        }

// ReSharper disable once InconsistentNaming
        private int ParseIndexVertUV(string data, out int uv)
        {
            string[] indices = data.Split('/');
            if (indices.Length == 2)
            {
                uv = int.Parse(indices[1]) - _uvOffset;
            }
            else
            {
                uv = -1;
            }
            return int.Parse(indices[0]) - _vertexOffset;
        }

        private void AddTriangle(int v1, int v2, int v3, int uv1, int uv2, int uv3)
        {
            if (uv1 < 0 || uv2 < 0 || uv3 < 0)
            {
                _uvMapped = false;
            }
            _triangles.Add(_uvMapped ? new Triangle(v1, v2, v3, uv1, uv2, uv3) : new Triangle(v1, v2, v3));
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
            Mesh newMesh;
            ISampler normalSampler;
            if (_useSmoothShading)
            {
                normalSampler = new InterpolatedNormalSampler();
            }
            else
            {
                normalSampler = new FlatNormalSampler();
            }

            if (_uvMapped)
            {
                newMesh = new Mesh(_vertices.ToArray(), _triangles.ToArray(), _uvs.ToArray(), Vector3.Zero,
                        new DiffuseShader(new BitmapSampler(_materials[_currentMaterialName])), normalSampler);
            }
            else
            {
                newMesh = new Mesh(_vertices.ToArray(), _triangles.ToArray(), Vector3.Zero,
                        new DiffuseShader(new SolidColorSampler(Color.Red)), normalSampler);
            }
            Meshes.Add(newMesh);
            InitBuffers();
        }

        private void ParseSmoothShading(string[] line)
        {
            if (line[1] == "off")
            {
                _useSmoothShading = false;
            }
            else if (line[1] == "1")
            {
                _useSmoothShading = true;
            }
            else
            {
                throw new Exception("OBJ parsing error, unrecognized command: " + string.Join(" ", line));
            }
        }

        private void ParseMtLib(string[] line)
        {
            string file = Path.Combine(_path, line[1]);
            MtlParser parser = new MtlParser();
            Dictionary<string, Bitmap> materials = parser.Parse(file);
            foreach (KeyValuePair<string, Bitmap> pair in materials)
            {
                _materials[pair.Key] = pair.Value;
            }
        }

        private void NotImplemented(string[] s)
        {
            
        }

        private void Ignore(string[] s)
        {
            
        }
    }
}
