using _3DTools;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace GraphicsBasics3D
{
    public partial class Sphere : Window
    {

        public Sphere()
        {
            InitializeComponent();
            DrawSphere(new Point3D(1, 1, 1), 3, 20, 15, Colors.DeepPink);
            Trackball trb = new Trackball();
            MyCamera.Transform = trb.Transform;
            trb.EventSource = this;
        }
        public Point3D GetPosition(double radius, double theta, double phi)
        {
            Point3D pt = new Point3D();
            double snt = Math.Sin(theta * Math.PI / 180);
            double cnt = Math.Cos(theta * Math.PI / 180);
            double snp = Math.Sin(phi * Math.PI / 180);
            double cnp = Math.Cos(phi * Math.PI / 180);
            pt.X = radius * snt * cnp;
            pt.Y = radius * cnt;
            pt.Z = -radius * snt * snp;
            return pt;
        }
        public static void drawTriangle(
            Point3D p0, Point3D p1, Point3D p2, Color color, Viewport3D viewport3D)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = color;
            Material material = new DiffuseMaterial(brush);
            GeometryModel3D geometry = new GeometryModel3D(mesh, material);
            ModelUIElement3D model = new ModelUIElement3D();
            model.Model = geometry;
            viewport3D.Children.Add(model);
        }
        public void DrawSphere(Point3D center, double radius, int u, int v, Color color)
        {
            if (u < 2 || v < 2)
                return;
            Model3DGroup tor = new Model3DGroup();
            Point3D[,] pts = new Point3D[u, v];
            for (int i = 0; i < u; i++)
            {
                for (int j = 0; j < v; j++)
                {
                    pts[i, j] =
                             GetPosition(radius, i * 180 / (u - 1), j * 180 / (v - 1));
                    pts[i, j] += (Vector3D)center;
                }
            }
            Point3D[] p = new Point3D[4];
            for (int i = 0; i < u - 1; i++)
            {
                for (int j = 0; j < v - 1; j++)
                {
                    p[0] = pts[i, j];
                    p[1] = pts[i + 1, j];
                    p[2] = pts[i + 1, j + 1];
                    p[3] = pts[i, j + 1];
                    drawTriangle(p[0], p[1], p[2], color, mainViewport);
                    drawTriangle(p[2], p[3], p[0], color, mainViewport);
                    drawTriangle(p[3], p[2], p[1], color, mainViewport);
                    drawTriangle(p[1], p[2], p[3], color, mainViewport);
                    drawTriangle(p[3], p[1], p[0], color, mainViewport);
                    drawTriangle(p[0], p[1], p[3], color, mainViewport);
                }

            }
           
        }

    }
}

