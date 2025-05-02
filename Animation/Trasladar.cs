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
    public long t0 { get; set; }
    public long tf { get; set; }
    public Trasladar(IModelo objeto, Vector3 traslacion, long t0, long tf)
    {
        this.objeto = objeto;
        this.traslacion = traslacion;
        this.t0 = t0;
        this.tf = tf;
    }

    public void Run(long tiempo)
    {
        if (tiempo >= t0 && tiempo <= tf)
        {
            objeto.Trasladar(traslacion);
        }
    }
}
