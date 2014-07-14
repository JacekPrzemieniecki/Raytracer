using System.Drawing;

namespace Raytracer
{
    internal class Renderer
    {
        private Scene _scene;

        public void Render(Bitmap bmp, Scene scene)
        {
            _scene = scene;
            for (int y = 0; y < bmp.Size.Height; y++)
            {
                for (int x = 0; x < bmp.Size.Width; x++)
                {
                    bmp.SetPixel(x, y, RenderPixel(x, y));
                }
            }
        }
        
        private Color RenderPixel(int x, int y)
        {
            var ray = new Ray(Vector3.Zero, new Vector3(x, y, 1.0f));
            RaycastHit hitInfo;
            bool hit = Raycast(_scene.Meshes[0], ray, out hitInfo);
            return hit ? Color.Red : Color.White;
        }

        private bool Raycast(Mesh mesh, Ray ray, out RaycastHit hitInfo)
        {
            hitInfo = new RaycastHit();
            return RaycastTriangle(mesh.Vertices, mesh.Triangles, 0, ray, hitInfo);
        }

        private bool RaycastTriangle(Vector3[] vertices, int[] triangles, int triangleId, Ray ray, RaycastHit hitInfo)
        {
            int triangleOffset = triangleId*3;
            Vector3 vert0 = vertices[triangles[triangleOffset]];
            Vector3 vert1 = vertices[triangles[triangleOffset + 1]];
            Vector3 vert2 = vertices[triangles[triangleOffset + 2]];
            Vector3 edge1 = vert1 - vert0;
            Vector3 edge2 = vert2 - vert0;
            Vector3 pVec = Vector3.Cross(ray.Direction, edge2);
            float determinant = Vector3.Dot(edge1, pVec);
            if (determinant == 0) return false;
            float invDeterminant = 1/determinant;
            Vector3 tVec = ray.Origin - vert0;
            hitInfo.u = Vector3.Dot(tVec, pVec)*invDeterminant;
            if (hitInfo.u < 0 || hitInfo.u > 1) return false;
            Vector3 qVec = Vector3.Cross(tVec, edge1);
            hitInfo.v = Vector3.Dot(ray.Direction, qVec)*invDeterminant;
            if (hitInfo.v < 0 || hitInfo.v + hitInfo.u > 1) return false;
            hitInfo.t = Vector3.Dot(edge2, qVec)*invDeterminant;
            return true;
        }
    }
}