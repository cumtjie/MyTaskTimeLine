using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace TaskTimeLineLab.Common
{
    public class Common
    {
        public static T Clone<T>(T RealObject)
        {
            using (Stream objStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(objStream,RealObject);
                    objStream.Seek(0, SeekOrigin.Begin);
                }
                catch (Exception)
                {
                    ;
                }
                return (T) formatter.Deserialize(objStream);
            }
        }
    }
}
