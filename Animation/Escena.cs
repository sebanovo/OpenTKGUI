using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKGUI.Animation;
class Escena
{
    public List<Animacion> Animaciones = [];
    public void Add(Animacion animacion)
    {
        Animaciones.Add(animacion);
    }

    public void play(long tiempo)
    {
        foreach(var animacion in Animaciones)
        {
            animacion.play(tiempo);
        }
    }
}
