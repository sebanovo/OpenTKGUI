using OpenTK.Mathematics;
using OpenTKGUI.Estructura;
using OpenTKGUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenTKGUI.Animation;
class Rotar : ITransformation
{
    public IModelo objeto { get; set; }
    public Vector3 rotacion { get; set; }
    public float velocity;
    public bool llegoX { get; set; }
    public bool llegoY { get; set; }
    public bool llegoZ { get; set; }
    public long t0 { get; set; }
    public long tf { get; set; }
    public Rotar(IModelo objeto, Vector3 rotacion, float velocity, long t0, long tf)
    {
        this.objeto = objeto;
        this.rotacion = rotacion;
        this.velocity = velocity;
        this.t0 = t0;
        this.tf = tf;
        this.llegoX = false;
        this.llegoY = false;
        this.llegoZ = false;
    }

    public void Run(long tiempo)
    {
        if (tiempo >= t0 && tiempo <= tf)
        {
            float X = objeto.Transformation.Rotation.X;
            float Y = objeto.Transformation.Rotation.Y;
            float Z = objeto.Transformation.Rotation.Z;
            if (X > rotacion.X)
            {
                if (!llegoX)
                {
                    objeto.Rotar(new Vector3(-velocity, 0, 0));
                    if (objeto.Transformation.Rotation.X <= rotacion.X)
                    {
                        llegoX = true;
                    }
                }
            }
            if (X < rotacion.X)
            {
                if (!llegoX)
                {
                    objeto.Rotar(new Vector3(velocity, 0, 0));
                    if (objeto.Transformation.Rotation.X >= rotacion.X)
                    {
                        llegoX = true;
                    }
                }
            }
            if (Y > rotacion.Y)
            {
                if (!llegoY)
                {
                    objeto.Rotar(new Vector3(0, -velocity, 0));
                    if (objeto.Transformation.Rotation.Y <= rotacion.Y)
                    {
                        llegoY = true;
                    }
                }
            }
            if (Y < rotacion.Y)
            {
                if (!llegoY)
                {
                    objeto.Rotar(new Vector3(0, velocity, 0));
                    if (objeto.Transformation.Rotation.Y >= rotacion.Y)
                    {
                        llegoY = true;
                    }
                }
            }
            if (Z > rotacion.Z)
            {
                if (!llegoZ)
                {
                    objeto.Rotar(new Vector3(0, 0, -velocity));
                    if (objeto.Transformation.Rotation.Z <= rotacion.Z)
                    {
                        llegoZ = true;
                    }
                }
            }
            if (Z < rotacion.Z)
            {
                if (!llegoZ)
                {
                    objeto.Rotar(new Vector3(0, 0, velocity));
                    if (objeto.Transformation.Rotation.Z >= rotacion.Z)
                    {
                        llegoZ = true;
                    }
                }
            }
        }
    }
}
