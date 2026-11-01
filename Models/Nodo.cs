namespace DocuTrack.Models
{
    public class Nodo
    {
        public string Nombre { get; set; }
        public bool EsCarpeta { get; set; }
        public Nodo? Izquierdo { get; set; }
        public Nodo? Derecho { get; set; }

        public Nodo(string nombre, bool esCarpeta)
        {
            Nombre = nombre;
            EsCarpeta = esCarpeta;
            Izquierdo = null;
            Derecho = null;
        }

        public bool EsHoja => Izquierdo == null && Derecho == null;
        public bool TieneUnSoloHijo => (Izquierdo != null && Derecho == null) || (Izquierdo == null && Derecho != null);
        public bool TieneDosHijos => Izquierdo != null && Derecho != null;
    }
}