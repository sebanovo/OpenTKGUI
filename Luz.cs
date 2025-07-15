using OpenTKGUI.Estructura;
using OpenTKGUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using System.Globalization;

namespace OpenTKGUI
{
    public class Luz 
    {
        public Vector3 position { get; set; }
        public Vector3 color { get; set; }
        public Luz(Vector3 position, Vector3 color)
        {
            this.position = position;
            this.color = color;
        }
    }
}