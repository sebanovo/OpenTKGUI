using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKGUI.Estructura;
public interface IModelo
{
    public string Name { get; set; }
    public void Draw(Matrix4? modelMatrix = null);
    public Transformation Transformation { get; }
    public void Add(IModelo modelo);
    public IModelo Get(string name); 
    public void Rotar(Vector3 rotation);
    public void Escalar(Vector3 scalation);
    public void Trasladar(Vector3 translation);
    public void Dispose();
}
