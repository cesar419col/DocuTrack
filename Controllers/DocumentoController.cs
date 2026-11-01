using DocuTrack.Models;
using DocuTrack.Views;

namespace DocuTrack.Controllers
{
    public class DocumentoController
    {
        private readonly ArbolBinario _arbol;
        private readonly DocumentoView _vista;

        public DocumentoController()
        {
            _arbol = new ArbolBinario();
            _vista = new DocumentoView();
        }

        public void EjecutarTaller()
        {
            ConstruirArbolInicial();
            EjecutarBusquedasRapidas();
            EjecutarActualizacionesSelectivas();
            EjecutarEliminacionesSelectivas();
            EjecutarRecorridosYMetricas();
        }

        private void ConstruirArbolInicial()
        {
            _vista.MostrarEncabezado("Caso de Uso 1: Construcción del Árbol (≥ 14 Nodos)");

            var datosPrueba = new (string Nombre, bool EsCarpeta)[]
            {
                ("Manuales", true),
                ("Finanzas", true),
                ("Ventas", true),
                ("Auditoria", false),
                ("Legal", true),
                ("Recursos", true),
                ("Zonas", false),
                ("Actas.pdf", false),
                ("Balances.xlsx", false),
                ("Contratos", true),
                ("Convenios.docx", false),
                ("Proyectos", true),
                ("Sistemas", true),
                ("Operaciones", true),
                ("Políticas.docx", false)
            };

            foreach (var doc in datosPrueba)
            {
                var resultado = _arbol.Insertar(doc.Nombre, doc.EsCarpeta);
                _vista.MostrarMensaje($"{resultado.Mensaje} (Comparaciones: {resultado.Comparaciones})", resultado.Exito);
            }

            _vista.MostrarInfo("Prueba Duplicado", "Intentando reinsertar 'Finanzas'...");
            var testDup = _arbol.Insertar("Finanzas", true);
            _vista.MostrarMensaje(testDup.Mensaje, testDup.Exito);

            _vista.ImprimirArbol(_arbol.Raiz);
        }

        private void EjecutarBusquedasRapidas()
        {
            _vista.MostrarEncabezado("Caso de Uso 2: Búsquedas Rápidas (6 Búsquedas)");

            string[] busquedas = new string[]
            {
                "Actas.pdf",
                "Legal",
                "Ventas",
                "Zonas",
                "Inexistente.doc",
                "Nomina2026.xlsx"
            };

            foreach (var nombre in busquedas)
            {
                var (nodo, comparaciones) = _arbol.Buscar(nombre);
                _vista.MostrarResultadoBusqueda(nombre, nodo, comparaciones);
            }
        }

        private void EjecutarActualizacionesSelectivas()
        {
            _vista.MostrarEncabezado("Caso de Uso 3: Actualizaciones Selectivas (Eliminar + Insertar)");

            _vista.MostrarInfo("1. Actualizar Hoja", "Modificando 'Auditoria' -> 'AuditoriaInterna'");
            var r1 = _arbol.Actualizar("Auditoria", "AuditoriaInterna");
            _vista.MostrarMensaje(r1.Mensaje, r1.Exito);
            _vista.ImprimirArbol(_arbol.Raiz);

            _vista.MostrarInfo("2. Actualizar Hijo Único", "Modificando 'Contratos' -> 'Contratos2026'");
            var r2 = _arbol.Actualizar("Contratos", "Contratos2026");
            _vista.MostrarMensaje(r2.Mensaje, r2.Exito);
            _vista.ImprimirArbol(_arbol.Raiz);

            _vista.MostrarInfo("3. Actualizar Raíz", "Modificando 'Manuales' -> 'DirectorioGeneral'");
            var r3 = _arbol.Actualizar("Manuales", "DirectorioGeneral");
            _vista.MostrarMensaje(r3.Mensaje, r3.Exito);
            _vista.ImprimirArbol(_arbol.Raiz);
        }

        private void EjecutarEliminacionesSelectivas()
        {
            _vista.MostrarEncabezado("Caso de Uso 4: Eliminaciones Selectivas");

            _vista.MostrarInfo("1. Eliminar Hoja", "Eliminando 'Zonas'...");
            var e1 = _arbol.Eliminar("Zonas");
            _vista.MostrarMensaje(e1.DetalleCaso, e1.Exito);
            _vista.ImprimirArbol(_arbol.Raiz);

            _vista.MostrarInfo("2. Eliminar Nodo con Un Solo Hijo", "Eliminando 'Proyectos'...");
            var e2 = _arbol.Eliminar("Proyectos");
            _vista.MostrarMensaje(e2.DetalleCaso, e2.Exito);
            _vista.ImprimirArbol(_arbol.Raiz);

            _vista.MostrarInfo("3. Eliminar Raíz (2 Hijos)", $"Eliminando Raíz actual: '{_arbol.Raiz?.Nombre}'...");
            var e3 = _arbol.Eliminar(_arbol.Raiz!.Nombre);
            _vista.MostrarMensaje(e3.DetalleCaso, e3.Exito);
            _vista.ImprimirArbol(_arbol.Raiz);
        }

        private void EjecutarRecorridosYMetricas()
        {
            _vista.MostrarEncabezado("Caso de Uso 5: Recorridos de Verificación y Métricas");

            _vista.MostrarRecorrido("Preorden", _arbol.Preorden());
            _vista.MostrarRecorrido("Inorden (Orden Ascendente / Clave BST)", _arbol.Inorden());
            _vista.MostrarRecorrido("Postorden", _arbol.Postorden());
            _vista.MostrarRecorrido("Por Niveles (BFS)", _arbol.PorNiveles());

            _vista.MostrarEncabezado("Métricas Finales");
            _vista.MostrarInfo("Altura del Árbol Final", _arbol.ObtenerAltura().ToString());
        }
    }
}