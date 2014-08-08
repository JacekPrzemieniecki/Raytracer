using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Raytracer.Parsers
{
    class MtlParser
    {
        private readonly Dictionary<string, Action<string[]>> _actionMap;
        private Dictionary<string, Bitmap> _materials;
        private string _currentMatName;
        private string _path;

        public MtlParser()
        {
            _actionMap = new Dictionary<string, Action<string[]>>
            {
                {"newmtl", line => { _currentMatName = line[1]; }},
                {"map_Kd", ParseFile}
            };
        }

        public Dictionary<string, Bitmap> Parse(string filePath)
        {
            _path = Path.GetDirectoryName(filePath);
            _materials = new Dictionary<string, Bitmap>()
            {
                {"Material.001", new Bitmap("F:\\Projects\\Raytracer\\Raytracer\\SampleData\\TutoScene.001_Cube.png")}
            };
            using (var file = new StreamReader(filePath))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    ParseLine(line.Split(' '));
                }
            }
            return _materials;
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
            }
        }

        private void ParseFile(string[] line)
        {
            Bitmap bmp = new Bitmap(Path.Combine(_path, line[1]));
            _materials[_currentMatName] = bmp;
        }
    }
}
