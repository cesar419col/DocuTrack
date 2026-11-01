using System;
using System.Collections.Generic;

namespace DocuTrack.Models
{
    public class ArbolBinario
    {
        public Nodo? Raiz { get; private set; }

        public ArbolBinario()
        {
            Raiz = null;
        }

        public (bool Exito, int Comparaciones, string Mensaje) Insertar(string nombre, bool esCarpeta)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return (false, 0, "El nombre no puede ser vacío o nulo.");

            int comparaciones = 0;
            var (nuevaRaiz, insertado) = InsertarRecursivo(Raiz, nombre.Trim(), esCarpeta, ref comparaciones);
            Raiz = nuevaRaiz;

            if (insertado)
                return (true, comparaciones, $"Nodo '{nombre}' insertado exitosamente.");
            else
                return (false, comparaciones, $"Inserción rechazada: '{nombre}' ya existe.");
        }

        private (Nodo?, bool) InsertarRecursivo(Nodo? actual, string nombre, bool esCarpeta, ref int comparaciones)
        {
            if (actual == null)
                return (new Nodo(nombre, esCarpeta), true);

            comparaciones++;
            int cmp = string.Compare(nombre, actual.Nombre, StringComparison.OrdinalIgnoreCase);

            if (cmp < 0)
            {
                var (izq, insertado) = InsertarRecursivo(actual.Izquierdo, nombre, esCarpeta, ref comparaciones);
                actual.Izquierdo = izq;
                return (actual, insertado);
            }
            else if (cmp > 0)
            {
                var (der, insertado) = InsertarRecursivo(actual.Derecho, nombre, esCarpeta, ref comparaciones);
                actual.Derecho = der;
                return (actual, insertado);
            }

            return (actual, false);
        }

        public (Nodo? NodoHallado, int Comparaciones) Buscar(string nombre)
        {
            int comparaciones = 0;
            Nodo? resultado = BuscarRecursivo(Raiz, nombre, ref comparaciones);
            return (resultado, comparaciones);
        }

        private Nodo? BuscarRecursivo(Nodo? actual, string nombre, ref int comparaciones)
        {
            if (actual == null)
                return null;

            comparaciones++;
            int cmp = string.Compare(nombre, actual.Nombre, StringComparison.OrdinalIgnoreCase);

            if (cmp == 0)
                return actual;
            if (cmp < 0)
                return BuscarRecursivo(actual.Izquierdo, nombre, ref comparaciones);

            return BuscarRecursivo(actual.Derecho, nombre, ref comparaciones);
        }

        public (bool Exito, string DetalleCaso) Eliminar(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return (false, "Nombre inválido.");

            string casoEjecutado = string.Empty;
            bool eliminado = false;

            Raiz = EliminarRecursivo(Raiz, nombre.Trim(), ref casoEjecutado, ref eliminado);
            return (eliminado, casoEjecutado);
        }

        private Nodo? EliminarRecursivo(Nodo? actual, string nombre, ref string casoEjecutado, ref bool eliminado)
        {
            if (actual == null)
                return null;

            int cmp = string.Compare(nombre, actual.Nombre, StringComparison.OrdinalIgnoreCase);

            if (cmp < 0)
            {
                actual.Izquierdo = EliminarRecursivo(actual.Izquierdo, nombre, ref casoEjecutado, ref eliminado);
            }
            else if (cmp > 0)
            {
                actual.Derecho = EliminarRecursivo(actual.Derecho, nombre, ref casoEjecutado, ref eliminado);
            }
            else
            {
                eliminado = true;

                if (actual.EsHoja)
                {
                    casoEjecutado = "Caso 1 (Nodo Hoja): Desenlazado correctamente.";
                    return null;
                }

                if (actual.TieneUnSoloHijo)
                {
                    casoEjecutado = "Caso 2 (Un solo hijo): Reconectado subárbol con el padre.";
                    return actual.Izquierdo ?? actual.Derecho;
                }

                casoEjecutado = "Caso 3 (Dos hijos): Reemplazado por sucesor inorden (mínimo del derecho).";
                Nodo sucesor = ObtenerMinimo(actual.Derecho!);
                actual.Nombre = sucesor.Nombre;
                actual.EsCarpeta = sucesor.EsCarpeta;
                
                string dummyCaso = string.Empty;
                bool dummyElim = false;
                actual.Derecho = EliminarRecursivo(actual.Derecho, sucesor.Nombre, ref dummyCaso, ref dummyElim);
            }

            return actual;
        }

        private Nodo ObtenerMinimo(Nodo nodo)
        {
            Nodo actual = nodo;
            while (actual.Izquierdo != null)
                actual = actual.Izquierdo;
            return actual;
        }

        public (bool Exito, string Mensaje) Actualizar(string nombreAntiguo, string nombreNuevo)
        {
            var (existente, _) = Buscar(nombreAntiguo);
            if (existente == null)
                return (false, $"El nodo '{nombreAntiguo}' no existe.");

            var (conflicto, _) = Buscar(nombreNuevo);
            if (conflicto != null && !nombreAntiguo.Equals(nombreNuevo, StringComparison.OrdinalIgnoreCase))
                return (false, $"El nuevo nombre '{nombreNuevo}' ya se encuentra en uso.");

            bool tipo = existente.EsCarpeta;

            var (exitoElim, _) = Eliminar(nombreAntiguo);
            if (!exitoElim)
                return (false, $"Fallo al remover '{nombreAntiguo}'.");

            var (exitoIns, _, msgIns) = Insertar(nombreNuevo, tipo);
            if (!exitoIns)
                return (false, $"Error al reinsertar '{nombreNuevo}': {msgIns}");

            return (true, $"Nodo '{nombreAntiguo}' actualizado a '{nombreNuevo}' exitosamente.");
        }

        public List<string> Preorden()
        {
            var lista = new List<string>();
            PreordenRecursivo(Raiz, lista);
            return lista;
        }

        private void PreordenRecursivo(Nodo? nodo, List<string> lista)
        {
            if (nodo == null) return;
            lista.Add($"{nodo.Nombre} ({(nodo.EsCarpeta ? "Carpeta" : "Archivo")})");
            PreordenRecursivo(nodo.Izquierdo, lista);
            PreordenRecursivo(nodo.Derecho, lista);
        }

        public List<string> Inorden()
        {
            var lista = new List<string>();
            InordenRecursivo(Raiz, lista);
            return lista;
        }

        private void InordenRecursivo(Nodo? nodo, List<string> lista)
        {
            if (nodo == null) return;
            InordenRecursivo(nodo.Izquierdo, lista);
            lista.Add($"{nodo.Nombre} ({(nodo.EsCarpeta ? "Carpeta" : "Archivo")})");
            InordenRecursivo(nodo.Derecho, lista);
        }

        public List<string> Postorden()
        {
            var lista = new List<string>();
            PostordenRecursivo(Raiz, lista);
            return lista;
        }

        private void PostordenRecursivo(Nodo? nodo, List<string> lista)
        {
            if (nodo == null) return;
            PostordenRecursivo(nodo.Izquierdo, lista);
            PostordenRecursivo(nodo.Derecho, lista);
            lista.Add($"{nodo.Nombre} ({(nodo.EsCarpeta ? "Carpeta" : "Archivo")})");
        }

        public List<string> PorNiveles()
        {
            var lista = new List<string>();
            if (Raiz == null) return lista;

            var cola = new Queue<Nodo>();
            cola.Enqueue(Raiz);

            while (cola.Count > 0)
            {
                Nodo actual = cola.Dequeue();
                lista.Add($"{actual.Nombre} ({(actual.EsCarpeta ? "Carpeta" : "Archivo")})");

                if (actual.Izquierdo != null) cola.Enqueue(actual.Izquierdo);
                if (actual.Derecho != null) cola.Enqueue(actual.Derecho);
            }

            return lista;
        }

        public int ObtenerAltura() => CalcularAltura(Raiz);

        private int CalcularAltura(Nodo? nodo)
        {
            if (nodo == null) return 0;
            int alturaIzq = CalcularAltura(nodo.Izquierdo);
            int alturaDer = CalcularAltura(nodo.Derecho);
            return 1 + Math.Max(alturaIzq, alturaDer);
        }
    }
}