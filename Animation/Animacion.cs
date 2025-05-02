using OpenTKGUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKGUI.Animation;
class Animacion
{
    public List<ITransformation> Transformaciones = [];
    public void Add(ITransformation transformacion)
    {
        Transformaciones.Add(transformacion);
    }
    public void play(long tiempo)
    {
        foreach(var transformacion in Transformaciones)
        {
            transformacion.Execute(tiempo);
        }
    }

}
