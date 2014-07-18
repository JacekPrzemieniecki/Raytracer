using System.Drawing;

namespace Raytracer
{
    internal class Renderer
    {
        private Scene _scene;
        private int _pictureWidth;
        private int _pictureHeight;

        public void Render(Bitmap bmp, Scene scene)
        {
            _scene = scene;
            _pictureHeight = bmp.Height;
            _pictureWidth = bmp.Width;
            for (int y = 0; y < bmp.Size.Height; y++)
            {
                for (int x = 0; x < bmp.Size.Width; x++)
                {
                    bmp.SetPixel(x, y, RenderPixel(x, y));
                }
            }
        }

        private Color RenderPixel(int screenX, int screenY)
        {
            float viewportX = (screenX + 0.5f) / _pictureWidth * 2 - 1;
            // Screen Y is pointed down
            float viewportY = 1 - (screenY + 0.5f) / _pictureHeight * 2;
            var ray = _scene.Camera.ViewportPointToRay(viewportX, viewportY);
            RaycastHit hitInfo;
            bool hit = Raycast(_scene.Meshes[0], ray, out hitInfo);
            if (hit)
            {
                return Color.FromArgb((int)(0xFF * (hitInfo.t / 7)), 0, 0);
            }
            return Color.White;
        }

        private bool Raycast(Mesh mesh, Ray ray, out RaycastHit closestHit)
        {
            closestHit = new RaycastHit();
            closestHit.t = float.MaxValue;
            for (int i = 0; i < mesh.TriangleCount; i++)
            {
                RaycastHit hitInfo;
                if (!RaycastTriangle(mesh, i, ray, out hitInfo) ||
                    (closestHit.t < hitInfo.t)) continue;
                closestHit = hitInfo;
            }
            return closestHit.t != float.MaxValue;
        }

        private bool RaycastTriangle(Mesh mesh, int triangleId, Ray ray, out RaycastHit hitInfo)
        {
            Vector3[] vertices = mesh.Vertices;
            int[] triangles = mesh.Triangles;
            hitInfo = new RaycastHit();
            int triangleOffset = triangleId * 3;
            Vector3 vert0 = vertices[triangles[triangleOffset]];
            Vector3 vert1 = vertices[triangles[triangleOffset + 1]];
            Vector3 vert2 = vertices[triangles[triangleOffset + 2]];
            Vector3 edge1 = vert1 - vert0;
            Vector3 edge2 = vert2 - vert0;
            Vector3 pVec = Vector3.Cross(ray.Direction, edge2);
            float determinant = Vector3.Dot(edge1, pVec);
            if (determinant == 0) return false;
            float invDeterminant = 1 / determinant;
            Vector3 tVec = ray.Origin - vert0;
            hitInfo.u = Vector3.Dot(tVec, pVec) * invDeterminant;
            if (hitInfo.u < 0 || hitInfo.u > 1) return false;
            Vector3 qVec = Vector3.Cross(tVec, edge1);
            hitInfo.v = Vector3.Dot(ray.Direction, qVec) * invDeterminant;
            if (hitInfo.v < 0 || hitInfo.v + hitInfo.u > 1) return false;
            hitInfo.t = Vector3.Dot(edge2, qVec) * invDeterminant;
            return hitInfo.t > 0;
        }
    }
}