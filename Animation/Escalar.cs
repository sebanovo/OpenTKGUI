using OpenTK.Mathematics;
using OpenTKGUI.Estructura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKGUI.Animation;
class Escalar
{
    public IModelo objeto { get; set; }
    public Vector3 escalacion { get; set; }
    public float velocity;
    public bool llegoX { get; set; }
    public bool llegoY { get; set; }
    public bool llegoZ { get; set; }
    public long t0 { get; set; }
    public long tf { get; set; }
    public Escalar(IModelo objeto, Vector3 escalacion,float velocity, long t0, long tf)
    {
        this.objeto = objeto;
        this.escalacion = escalacion;
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
            float X = objeto.Transformation.Scale.X;
            float Y = objeto.Transformation.Scale.Y;
            float Z = objeto.Transformation.Scale.Z;
            if (X > escalacion.X)
            {
                if (!llegoX)
                {
                    objeto.Escalar(new Vector3(-velocity, 0, 0));
                    if (objeto.Transformation.Scale.X <= escalacion.X)
                    {
                        llegoX = true;
                    }
                }
            }
            if (X < escalacion.X)
            {
                if (!llegoX)
                {
                    objeto.Escalar(new Vector3(velocity, 0, 0));
                    if (objeto.Transformation.Scale.X >= escalacion.X)
                    {
                        llegoX = true;
                    }
                }
            }
            if (Y > escalacion.Y)
            {
                if (!llegoY)
                {
                    objeto.Escalar(new Vector3(0, -velocity, 0));
                    if (objeto.Transformation.Scale.Y <= escalacion.Y)
                    {
                        llegoY = true;
                    }
                }
            }
            if (Y < escalacion.Y)
            {
                if (!llegoY)
                {
                    objeto.Escalar(new Vector3(0, velocity, 0));
                    if (objeto.Transformation.Scale.Y >= escalacion.Y)
                    {
                        llegoY = true;
                    }
                }
            }
            if (Z > escalacion.Z)
            {
                if(!llegoZ)
                {
                    objeto.Escalar(new Vector3(0, 0, -velocity));
                    if(objeto.Transformation.Scale.Z <= escalacion.Z)
                    {
                        llegoZ = true;
                    }
                }
            }
            if(Z < escalacion.Z)
            {
                if(!llegoZ)
                {
                    objeto.Escalar(new Vector3(0, 0, velocity));
                    if(objeto.Transformation.Scale.Z >= escalacion.Z)
                    {
                        llegoZ = true;
                    }
                }
            }
        }
    }
}
