using BlueSkynet.Infrastructure;
using System.IO;

namespace BlueSkynet.TestUtilities
{
    public class FakeDb
    {
        public static LiteBlueSkynetDatabase CreateInMemoryDatabase()
        {
            var mem = new MemoryStream();

            return new LiteBlueSkynetDatabase(mem);
        }
    }
}