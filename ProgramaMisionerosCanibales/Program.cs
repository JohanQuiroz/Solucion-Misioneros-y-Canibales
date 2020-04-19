using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaMisionerosCanibales
{
    class Program
    {
        static void regresarBote(Sitio bote, Sitio orilla)
        {
            // DEVUELVE A LA ORILLA A LOS PASAJEROS QUE SE ENCUENTRAN EN EL BOTE DE REGRESO
            orilla.CantidadCanibales += bote.CantidadCanibales;           
            orilla.CantidadMisioneros += bote.CantidadMisioneros;
            bote.Limpiar();                                    
        }

        static void escogerPasajeroQueBaja(Sitio bote, Sitio orillaIzq, Sitio orillaDer)
        {
            // SE ESCOGERA AL PASAJERO O PASAJEROS QUE BAJAN EN LA ORILLA IZQUIERDA
            bool seleCanibal = true, seleMisionero = true;
            if (orillaDer.ElSitioEstaVacio())
            {
                //SIGNIFICA QUE SON LOS ULTIMOS PASAJEROS
                orillaIzq.CantidadCanibales += bote.CantidadCanibales;
                orillaIzq.CantidadMisioneros += bote.CantidadMisioneros;
            }
            else
            {
                if (bote.CantidadCanibales > 0)
                {
                    //ELIGE UN CANIBAL
                    orillaIzq.CantidadCanibales++;
                    bote.CantidadCanibales--;
                    //VERIFICA QUE NO CAUSE PROBLEMAS 
                    if (!orillaIzq.EstanLosMisionerosFueraDePeligro())
                    {
                        //SI CAUSA PROBLEMAS, LO REGRESA AL BOTE
                        seleCanibal = false;
                        orillaIzq.CantidadCanibales--;
                        bote.CantidadCanibales++;
                    }
                    else
                    {
                        // VERIFICA SI QUEDO SOLO 1 CANIBAL O 2 EN EL BOTE
                        if(bote.CantidadCanibales == 1)
                            Console.WriteLine("             ===>   Se queda 1 CANIBAL y regresa 1 CANIBAL");
                        else
                            Console.WriteLine("             ===>   Se queda 1 CANIBAL y regresa 1 MISIONERO");
                    }                        
                }
                else                
                    seleCanibal = false;                

                if (bote.CantidadMisioneros > 0 && !seleCanibal)
                {
                    //ELIGE UN MISIONERO
                    orillaIzq.CantidadMisioneros++;
                    bote.CantidadMisioneros--;
                    //VERIFICA QUE NO CAUSE PROBLEMAS 
                    if (!orillaIzq.EstanLosMisionerosFueraDePeligro())
                    {
                        //SI CAUSA PROBLEMAS, LO REGRESA AL BOTE
                        seleMisionero = false;
                        orillaIzq.CantidadMisioneros--;
                        bote.CantidadMisioneros++;
                    }
                    else
                    {
                        // VERIFICA SI QUEDO SOLO 1 MISIONERO O 2 EN EL BOTE
                        if (bote.CantidadMisioneros == 1)
                            Console.WriteLine("             ===>   Se queda 1 MISIONERO y regresa 1 MISIONERO");
                        else
                            Console.WriteLine("             ===>   Se queda 1 MISIONERO y regresa 1 CANIBAL");
                    }               
                }
                else                
                    seleMisionero = false;                

                //SE CUMPLE CUANDO AL SELECCIONAR 1 CANIBAL COMO 1 MISIONERO CAUSA ERROR, SE DEVUELVE A LOS 2
                if(!seleCanibal && !seleMisionero)
                    Console.WriteLine("             ===>   Se REGRESA EL BOTE SIN DEJAR A NADIE ");
            }            

        }
        
        static void seleccionarPasajeros(Sitio bote, Sitio orilla)
        {
            Random pasajero = new Random();                     
            do
            {
                // VUELVE A COLOCAR A LA ORILLA LOS MISIONEROS O CANIBALES DEL BOTE EN CASO NO SE PUEDAN TRASLADAR PORQUE CAUSAN ERROR
                // EN LA PRIMERA ITERACION BOTE NO CONTIENE ALGUN PASAJERO
                orilla.CantidadMisioneros += bote.CantidadMisioneros;
                orilla.CantidadCanibales += bote.CantidadCanibales;
                bote.Limpiar();
                for (int i = 0; i < 2; i++)
                {
                    // EL NUMERO RANDOM  0 => MISIONERO
                    //                   1 => CANIBAL
                    // VERIFICA QUE EXISTA MISIONEROS
                    if (pasajero.Next(0, 2) == 0 && orilla.CantidadMisioneros > 0)
                    {
                        //SELECCIONAR UN MISIONERO Y LO COLOCA AL BOTE Y QUITA 1 DE LA ORILLA
                        bote.CantidadMisioneros++;
                        orilla.CantidadMisioneros--;
                    }
                    // VERIFICA QUE EXISTA CANIBALES
                    else if (orilla.CantidadCanibales > 0)
                    {
                        //SELECCIONAR UN CANIBAL Y LO COLOCA AL BOTE Y QUITA 1 DE LA ORILLA
                        bote.CantidadCanibales++;
                        orilla.CantidadCanibales--;
                    }                        
                }                                
                // VERIFICA QUE NO CAUSE PROBLEMAS EN LA ORILLA DERECHA
            } while (!orilla.EstanLosMisionerosFueraDePeligro());
            Console.WriteLine("                  <<>> Pasajeros Bote : Misioneros => " + bote.CantidadMisioneros +
                              " Canibales => " + bote.CantidadCanibales);
            

        }

        static void imprimirResultado(Sitio orillaIz, Sitio orillaDe)
        {
            Console.WriteLine("            IZQUIERDA : Misioneros => " + orillaIz.CantidadMisioneros +
                              " -- Canibales => " + orillaIz.CantidadCanibales +
                              " ------  DERECHA : Misioneros => " + orillaDe.CantidadMisioneros +
                                  " -- Canibales => " + orillaDe.CantidadCanibales);
        }
        
        static void Main(string[] args)
        {
            Sitio orillaIzquierda = new Sitio(), orillaDerecha = new Sitio(), bote = new Sitio();
            bool ida = true;
            Console.Write("Ingrese la cantidad de Misioneros : ");
            orillaDerecha.CantidadMisioneros = Int16.Parse(Console.ReadLine());
            Console.Write("Ingrese la cantidad de Canibales : ");
            orillaDerecha.CantidadCanibales = Int16.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("             =>   Bote en la Orilla Derecha");
            while (!orillaDerecha.ElSitioEstaVacio())
            {
                if (ida)
                {                    
                    seleccionarPasajeros(bote, orillaDerecha);
                    Console.WriteLine("             <=   Bote en la Orilla Izquierda");
                    escogerPasajeroQueBaja(bote, orillaIzquierda, orillaDerecha);
                }
                else
                {                    
                    regresarBote(bote, orillaDerecha);
                    Console.WriteLine("             =>   Bote en la Orilla Derecha");
                    imprimirResultado(orillaIzquierda, orillaDerecha);
                }                                                                        
                ida = !ida;                
            }
            Console.WriteLine("\n       RESULTADO FINAL  -------- \n ");
            imprimirResultado(orillaIzquierda, orillaDerecha);
            Console.ReadKey();
        }
    }
}
