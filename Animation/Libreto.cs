using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKGUI.Animation;
class Libreto
{
    public List<Escena> Escenas;
    public Libreto()
    {
        Escenas = [];
    }

    public Libreto(Escena escena)
    {
        Escenas = [];
        Escenas.Add(escena);
    }
    
    public void Add(Escena escena)
    {
        Escenas.Add(escena);
    }

    public void Play(long tiempo)
    {
        foreach (var escena in Escenas)
        {
            escena.Play(tiempo);
        }
    }
}
