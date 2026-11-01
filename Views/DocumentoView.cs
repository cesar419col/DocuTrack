using System;
using System.Collections.Generic;
using DocuTrack.Models;

namespace DocuTrack.Views
{
    public class DocumentoView
    {
        public void MostrarEncabezado(string titulo)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('=', 70));
            Console.WriteLine($"  {titulo.ToUpper()}");
            Console.WriteLine(new string('=', 70));
            Console.ResetColor();
        }

        public void MostrarMensaje(string mensaje, bool exito = true)
        {
            Console.ForegroundColor = exito ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"  [{(exito ? "OK" : "ALERTA")}] {mensaje}");
            Console.ResetColor();
        }

        public void MostrarInfo(string etiqueta, string valor)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"  {etiqueta}: ");
            Console.ResetColor();
            Console.WriteLine(valor);
        }

        public void MostrarResultadoBusqueda(string nombreBuscado, Nodo? nodo, int comparaciones)
        {
            if (nodo != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  -> Elemento '{nombreBuscado}': HALLADO");
                Console.ResetColor();
                Console.WriteLine($"     Tipo: {(nodo.EsCarpeta ? "Carpeta" : "Archivo")} | Comparaciones realizadas: {comparaciones}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"  -> Elemento '{nombreBuscado}': NO HALLADO");
                Console.ResetColor();
                Console.WriteLine($"     Comparaciones realizadas: {comparaciones}");
            }
        }

        public void MostrarRecorrido(string nombreRecorrido, List<string> elementos)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n  Recorrido {nombreRecorrido}:");
            Console.ResetColor();
            Console.WriteLine("  [ " + string.Join("  ->  ", elementos) + " ]");
        }

        public void ImprimirArbol(Nodo? raiz)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n  Estructura Jerárquica del Repositorio (BST):");
            Console.ResetColor();

            if (raiz == null)
            {
                Console.WriteLine("  (Árbol vacío)");
                return;
            }

            ImprimirNodoRecursivo(raiz, "", true);
            Console.WriteLine();
        }

        private void ImprimirNodoRecursivo(Nodo nodo, string prefijo, bool esUltimo)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(prefijo);
            Console.Write(esUltimo ? "└── " : "├── ");
            Console.ResetColor();

            if (nodo.EsCarpeta)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"[{nodo.Nombre}]");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(nodo.Nombre);
            }
            Console.ResetColor();

            var hijos = new List<(Nodo Nodo, bool EsUltimo)>();
            if (nodo.Izquierdo != null && nodo.Derecho != null)
            {
                hijos.Add((nodo.Izquierdo, false));
                hijos.Add((nodo.Derecho, true));
            }
            else if (nodo.Izquierdo != null)
            {
                hijos.Add((nodo.Izquierdo, true));
            }
            else if (nodo.Derecho != null)
            {
                hijos.Add((nodo.Derecho, true));
            }

            for (int i = 0; i < hijos.Count; i++)
            {
                string nuevoPrefijo = prefijo + (esUltimo ? "    " : "│   ");
                ImprimirNodoRecursivo(hijos[i].Nodo, nuevoPrefijo, hijos[i].EsUltimo);
            }
        }
    }
}