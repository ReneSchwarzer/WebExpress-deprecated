using System.IO;

namespace WebExpress.WebApp.WebIndex.Storage
{
    public class IndexStorageDataStructureHeader : IndexStorageDataStructure
    {
        /// <summary>
        /// Returns or sets the file identifire.
        /// </summary>
        public string Identifier { get; internal set; }

        /// <summary>
        /// Returns the amount of space required on the storage device.
        /// </summary>
        public override uint SizeOf => 3;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The reference to the context of the index.</param>
        public IndexStorageDataStructureHeader(IndexStorageContext context)
            : base(context)
        {
            Addr = 0;
            Identifier = "wri";
        }

        /// <summary>
        /// Reads the record from the storage medium.
        /// </summary>
        /// <param name="reader">The reader for i/o operations.</param>
        public override void Read(BinaryReader reader)
        {
            reader.BaseStream.Seek((long)Addr, SeekOrigin.Begin);

            Identifier = new string(reader.ReadChars(3));
        }

        /// <summary>
        /// Writes the record to the storage medium.
        /// </summary>
        /// <param name="writer">The writer for i/o operations.</param>
        public override void Write(BinaryWriter writer)
        {
            writer.BaseStream.Seek((long)Addr, SeekOrigin.Begin);

            writer.Write(Identifier.ToCharArray(0, 3));
        }
    }
}
