using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaMisionerosCanibales
{
    public class Sitio
    {
        public int CantidadMisioneros { get; set; }
        public int CantidadCanibales { get; set; }

        // VERIFICA QUE LOS MISIONEROS SEAN MAYOR O IGUAL QUE LOS CANIBALES EN EL SITIO
        public bool EstanLosMisionerosFueraDePeligro()
        {            
            if ((CantidadCanibales == 1 && CantidadMisioneros == 0) || CantidadMisioneros >= CantidadCanibales)
                return true;
            else
                return false;
        }

        // VERIFICA SI EL SITIO ESTA VACIO
        public bool ElSitioEstaVacio()
        {
            if (CantidadMisioneros == 0 && CantidadCanibales == 0)
                return true;
            else
                return false;
        }

        // LIMPIA LOS VALORES
        public void Limpiar()
        {
            CantidadMisioneros = 0;
            CantidadCanibales = 0;
        }
    }
}
