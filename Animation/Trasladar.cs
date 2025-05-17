using OpenTK.Mathematics;
using OpenTKGUI.Estructura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKGUI.Animation;
class Trasladar : ITransformation
{
    public IModelo objeto { get; set; }
    public Vector3 traslacion { get; set; }
    public float velocity;
    public bool llegoX { get; set; }
    public bool llegoY { get; set; }
    public bool llegoZ { get; set; }
    public long t0 { get; set; }
    public long tf { get; set; }
    public Trasladar(IModelo objeto, Vector3 traslacion, float velocity, long t0, long tf)
    {
        this.objeto = objeto;
        this.traslacion = traslacion;
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
            float X = objeto.Transformation.Position.X;
            float Y = objeto.Transformation.Position.Y;
            float Z = objeto.Transformation.Position.Z;
            if (X > traslacion.X)
            {
                if (!llegoX)
                {
                    objeto.Trasladar(new Vector3(-velocity, 0, 0));
                    if (objeto.Transformation.Position.X <= traslacion.X)
                    {
                        llegoX = true;
                    }
                }
            }
            if (X < traslacion.X)
            {
                if (!llegoX)
                {
                    objeto.Trasladar(new Vector3(velocity, 0, 0));
                    if (objeto.Transformation.Position.X >= traslacion.X)
                    {
                        llegoX = true;
                    }
                }
            }
            if (Y > traslacion.Y)
            {
                if (!llegoY)
                {
                    objeto.Trasladar(new Vector3(0, -velocity, 0));
                    if (objeto.Transformation.Position.Y <= traslacion.Y)
                    {
                        llegoY = true;
                    }
                }
            }
            if (Y < traslacion.Y)
            {
                if (!llegoY)
                {
                    objeto.Trasladar(new Vector3(0, velocity, 0));
                    if (objeto.Transformation.Position.Y >= traslacion.Y)
                    {
                        llegoY = true;
                    }
                }
            }
            if (Z > traslacion.Z)
            {
                if(!llegoZ)
                {
                    objeto.Trasladar(new Vector3(0, 0, -velocity));
                    if(objeto.Transformation.Position.Z <= traslacion.Z)
                    {
                        llegoZ = true;
                    }
                }
            }
            if(Z < traslacion.Z)
            {
                if(!llegoZ)
                {
                    objeto.Trasladar(new Vector3(0, 0, velocity));
                    if(objeto.Transformation.Position.Z >= traslacion.Z)
                    {
                        llegoZ = true;
                    }
                }
            }
        }
    }
}
