using DocuTrack.Controllers;

namespace DocuTrack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var controller = new DocumentoController();
            controller.EjecutarTaller();
        }
    }
}
