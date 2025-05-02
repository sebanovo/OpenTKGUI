using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKGUI.Animation;
class Libreto
{
    public List<Escena> Escenas = [];
    public void Add(Escena escena)
    {
        Escenas.Add(escena);
    }
    public void play(long tiempo)
    {
        foreach (var escena in Escenas)
        {
            escena.play(tiempo);
        }
    }
}
