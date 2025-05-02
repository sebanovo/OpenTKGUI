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
    public long t0 { get; set; }
    public long tf { get; set; }
    public Escalar(IModelo objeto, Vector3 escalacion, long t0, long tf)
    {
        this.objeto = objeto;
        this.escalacion = escalacion;
        this.t0 = t0;
        this.tf = tf;
    }

    public void Run(long tiempo)
    {
        if (tiempo >= t0 && tiempo <= tf)
        {
            objeto.Escalar(escalacion);
        }
    }
}
