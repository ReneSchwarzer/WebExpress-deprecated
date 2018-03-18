using WebExpress.Messages;
using WebExpress.Pages;

namespace WebExpress.Workers
{
    /// <summary>
    /// Arbeitet eine Anfrage ab. Dies erfolgt nebenläufig
    /// </summary>
    public class WorkerStream : WorkerBinary
    {
        /// <summary>
        /// Liefert oder setzt den Stream
        /// </summary>
        public new System.IO.StreamReader Ressource { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public WorkerStream(Path url, System.IO.StreamReader stream)
            : base(url)
        {
            Ressource = stream;
            base.Ressource = StreamToBytes(Ressource);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort</returns>
        public override Response Process(Request request)
        {
            var response = base.Process(request);

            return response;
        }

        /// <summary>
        /// Konvertiert das Icon in einen Byte-Array
        /// </summary>
        /// <param name="Bitmap"></param>
        /// <returns></returns>
        private static byte[] StreamToBytes(System.IO.StreamReader stream)
        {
            using (var br = new System.IO.BinaryReader(stream.BaseStream))
            {
                return br.ReadBytes((int)stream.BaseStream.Length);
            }
        }
    }
}
