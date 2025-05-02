using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKGUI.Animation;
class Escena
{
    public List<Animacion> Animaciones;
    public Escena() { 
        Animaciones = []; 
    }

    public Escena(Animacion animacion) {
        Animaciones = [];
        Animaciones.Add(animacion);
    }

    public void Add(Animacion animacion)
    {
        Animaciones.Add(animacion);
    }

    public void Play(long tiempo)
    {
        foreach(var animacion in Animaciones)
        {
            animacion.Play(tiempo);
        }
    }
}
