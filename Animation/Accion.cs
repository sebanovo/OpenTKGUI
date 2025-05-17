using OpenTKGUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKGUI.Animation;
class Accion
{
    public List<ITransformation> Transformaciones = [];
    public Accion()
    {
        Transformaciones = [];
    }

    public  Accion (ITransformation transformacion)
    {
        Transformaciones = [];
        Transformaciones.Add(transformacion);
    }

    public void Add(ITransformation transformacion)
    {
        Transformaciones.Add(transformacion);
    }

    public void Play(long tiempo)
    {
        foreach(var transformacion in Transformaciones)
        {
            transformacion.Run(tiempo);
        }
    }
}
