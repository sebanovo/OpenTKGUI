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
    public long t0 { get; set; }
    public long tf { get; set; }
    public Rotar(IModelo objeto, Vector3 rotacion, long t0, long tf)
    {
        this.objeto = objeto;
        this.rotacion = rotacion;
        this.t0 = t0;
        this.tf = tf;
    }
    public void Execute(long tiempo)
    {
        if (tiempo >= t0 && tiempo <= tf)
        {
            objeto.Rotar(rotacion);
        }
    }
}
