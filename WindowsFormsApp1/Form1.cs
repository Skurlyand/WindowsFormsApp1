using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;



namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {
        double acc;
        double v;
        double xinit;
        double tinit;
        int choice;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void glControl1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            double A(double x, double y1, double y2, double y3, double y4)
            {
                return y3;
            }
            double B(double x, double y1, double y2, double y3, double y4)
            {
                return y4;
            }
            double C(double x, double y1, double y2, double y3, double y4)
            {
                return acc * y4;
            }
            double D(double x, double y1, double y2, double y3, double y4)
            {
                return acc * y3;    
            }
            /* GL.Viewport(0, 0, Width, Height);

             Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)(10*Math.PI / 180), 1f, 1f, 64f);
             GL.MatrixMode(MatrixMode.Projection);
             GL.LoadMatrix(ref projection);
             GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

             Matrix4 modelview = Matrix4.LookAt(0, 0, -15, 0, 0, 0, 0, 1, 0);
             GL.MatrixMode(MatrixMode.Modelview);
             GL.LoadMatrix(ref modelview);*/
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.ClearColor(0, 0, 0, 0);
            GL.Color3(0f, 1f, 0f);
            GL.Rotate(45f, 0.0f, 0.0f, 45.0f);
            GL.LineWidth(1);
            double a = 0.7;
            double z = 0;
       
            GL.Begin(BeginMode.Lines);

            GL.Vertex3(a, -a, z);
            GL.Vertex3(-a, a, z);

            GL.Vertex3(-a, a, z);
            GL.Vertex3(-a, -a, z);

            GL.Vertex3(-a, -a, z);
            GL.Vertex3(a, -a, z);

            GL.Vertex3(a, -a, z);
            GL.Vertex3(a, a, z);

            double p0, q0;
            p0 = 2 * a * Math.Atan(-50) / 3.14;
            q0 = 2 * a * Math.Atan(-50) / 3.14;
            for (double j = -5; j <= 5; j += 0.25)
            {
                for (double i = -50; i < 50; i += 0.01)
                {
                    GL.Vertex3(p0, q0, z);
                    p0 = 2 * a * Math.Atan(i + j) / 3.14;
                    q0 = 2 * a * Math.Atan(i - j) / 3.14;
                    if ((p0 > a) || (q0 > a) || (q0 < -a) || (p0 < -a)) break;
                    GL.Vertex3(p0, q0, z);
                }
            }
            for (double i = -5; i <= 5; i += 0.25)
            {
                for (double j = -50; j < 50; j += 0.01)
                {
                    GL.Vertex3(p0, q0, z);
                    p0 = 2 * a * Math.Atan(i + j) / 3.14;
                    q0 = 2 * a * Math.Atan(i - j) / 3.14;
                    if ((p0 > a) || (q0 > a) || (q0 < -a) || (p0 < -a)) break;
                    GL.Vertex3(p0, q0, z);
                }
            }

            GL.End();

            if (choice == 1)
            {
                GL.LineWidth(3);
                GL.Begin(BeginMode.Lines);
                GL.Color3(0.6, 0.3, 0.8);
                double p, q;
                p = 2 * a * Math.Atan(-(1 + v) * 50) / 3.14;
                q = 2 * a * Math.Atan(-(1 - v) * 50) / 3.14;
                for (double i = -50; i < 50; i += 0.01)
                {
                    GL.Vertex3(p, q, z);
                    p = 2 * a * Math.Atan((1 + v) * i) / 3.14;
                    q = 2 * a * Math.Atan((1 - v) * i) / 3.14;
                    if ((p > a) || (q > a) || (q < -a) || (p < -a)) break;
                    GL.Vertex3(p, q, z);
                }
                GL.End();
            }


            if (choice == 2)
            {
                GL.LineWidth(3);
                GL.Begin(BeginMode.Lines);
                GL.Color3(0.6, 0.3, 0.8);

                double init = 0; double b = 100; double h = 0.001;
                int n = (int)((b - init) / h);
                int c = 4;
                double[] X = new double[n];

                double[,] F1 = new double[c, n];
                double[,] F2 = new double[c, n];
                double[,] F3 = new double[c, n];
                double[,] F4 = new double[c, n];

              
                double[] Y1 = new double[n];
                double[] Y2 = new double[n];
                double[] Y3 = new double[n];
                double[] Y4 = new double[n];


     
                int i = 0;
                X[0] = init; Y1[0] = tinit; Y2[0] = xinit; Y3[0] = Math.Pow((1 - v * v), (-0.5)); Y4[0] = v * v / Math.Pow((1 - v * v), (-0.5));
                for (int j = 1; j < n; j++)
                {
                    X[j] = init + j * h;

                    F1[i,j] = h * A(X[j - 1], Y1[j - 1], Y2[j - 1], Y3[j - 1], Y4[j - 1]);
                    F1[i + 1,j] = h * B(X[j - 1], Y1[j - 1], Y2[j - 1], Y3[j - 1], Y4[j - 1]);
                    F1[i + 2,j] = h * C(X[j - 1], Y1[j - 1],- Y2[j - 1], Y3[j - 1], Y4[j - 1]);
                    F1[i + 3,j] = h * D(X[j - 1], Y1[j - 1], Y2[j - 1], Y3[j - 1], Y4[j - 1]);


                    F2[i,j] = h * A(X[j - 1] + h / 2.0, Y1[j - 1] + F1[i,j] / 2.0, Y2[j - 1] + F1[i +1 ,j] / 2.0, Y3[j - 1] + F1[i + 2,j] / 2.0, Y4[j - 1] + F1[i + 3,j] / 2.0);
                    F2[i + 1,j] = h * B(X[j - 1] + h / 2.0, Y1[j - 1] + F1[i,j] / 2.0, Y2[j - 1] + F1[i + 1,j] / 2.0, Y3[j - 1] + F1[i + 2,j] / 2.0, Y4[j - 1] + F1[i + 3,j] / 2.0);
                    F2[i + 2,j] = h * C(X[j - 1] + h / 2.0, Y1[j - 1] + F1[i,j] / 2.0, Y2[j - 1] + F1[i + 1,j] / 2.0, Y3[j - 1] + F1[i + 2,j] / 2.0, Y4[j - 1] + F1[i + 3,j] / 2.0);
                    F2[i + 3,j] = h * D(X[j - 1] + h / 2.0, Y1[j - 1] + F1[i,j] / 2.0, Y2[j - 1] + F1[i + 1,j] / 2.0, Y3[j - 1] + F1[i + 2,j] / 2.0, Y4[j - 1] + F1[i + 3,j] / 2.0);

                    F3[i,j] = h * A(X[j - 1] + h / 2.0, Y1[j - 1] + F2[i,j] / 2.0, Y2[j - 1] + F2[i + 1,j] / 2.0, Y3[j - 1] + F2[i + 2,j] / 2.0, Y4[j - 1] + F2[i + 3,j] / 2.0);
                    F3[i + 1,j] = h * B(X[j - 1] + h / 2.0, Y1[j - 1] + F2[i,j] / 2.0, Y2[j - 1] + F2[i + 1,j] / 2.0, Y3[j - 1] + F2[i + 2,j] / 2.0, Y4[j - 1] + F2[i + 3,j] / 2.0);
                    F3[i + 2,j] = h * C(X[j - 1] + h / 2.0, Y1[j - 1] + F2[i,j] / 2.0, Y2[j - 1] + F2[i + 1,j] / 2.0, Y3[j - 1] + F2[i + 2,j] / 2.0, Y4[j - 1] + F2[i + 3,j] / 2.0);
                    F3[i + 3,j] = h * D(X[j - 1] + h / 2.0, Y1[j - 1] + F2[i,j] / 2.0, Y2[j - 1] + F2[i + 1,j] / 2.0, Y3[j - 1] + F2[i + 2,j] / 2.0, Y4[j - 1] + F2[i + 3,j] / 2.0);

                    F4[i,j] = h * A(X[j - 1] + h, Y1[j - 1] + F3[i,j], Y2[j - 1] + F3[i + 1,j], Y3[j - 1] + F3[i + 2,j], Y4[j - 1] + F3[i + 3,j]);
                    F4[i + 1,j] = h * B(X[j - 1] + h, Y1[j - 1] + F3[i,j], Y2[j - 1] + F3[i + 1,j], Y3[j - 1] + F3[i + 2,j], Y4[j - 1] + F3[i + 3,j]);
                    F4[i + 2,j] = h * C(X[j - 1] + h, Y1[j - 1] + F3[i,j], Y2[j - 1] + F3[i + 1,j], Y3[j - 1] + F3[i + 2,j], Y4[j - 1] + F3[i + 3,j]);
                    F4[i + 3,j] = h * D(X[j - 1] + h, Y1[j - 1] + F3[i,j], Y2[j - 1] + F3[i + 1,j], Y3[j - 1] + F3[i + 2,j], Y4[j - 1] + F3[i + 3,j]);

                    Y1[j] = Y1[j - 1] + (F1[i,j] + 2 * F2[i,j] + 2 * F3[i,j] + F4[i,j]) / 6;
                    Y2[j] = Y2[j - 1] + (F1[i + 1,j] + 2 * F2[i + 1,j] + 2 * F3[i + 1,j] + F4[i + 1,j]) / 6;
                    Y3[j] = Y3[j - 1] + (F1[i + 2,j] + 2 * F2[i + 1,j] + 2 * F3[i + 2,j] + F4[i + 2,j]) / 6;
                    Y4[j] = Y4[j - 1] + (F1[i + 3,j] + 2 * F2[i + 3,j] + 2 * F3[i + 3,j] + F4[i + 3,j]) / 6;
                }
                double p, q;
                p = 2 * a *Math.Atan(Y1[0] + Y2[0]) / 3.14;
                q = 2 * a * Math.Atan(Y1[0] - Y2[0]) / 3.14;
                for (int k = 0; k < n; k++)
                {
                    GL.Vertex3(p, q, z);
                    p = 2 * a * Math.Atan(Y1[k] + Y2[k]) / 3.14;
                    q = 2 * a * Math.Atan(Y1[k] - Y2[k]) / 3.14;
                    if ((p > a) || (q > a) || (q < -a) || (p < -a)) break;
                    GL.Vertex3(p, q, z);
                }
                GL.End();
            }

                glControl1.SwapBuffers();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            xinit = Convert.ToDouble(textBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            tinit = Convert.ToDouble(textBox2.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            v = Convert.ToDouble(textBox3.Text);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            acc = Convert.ToDouble(textBox4.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                choice = 1;
            }
            if (comboBox1.SelectedIndex == 1)
            {
                choice = 2;
            }
        }
    }
}
