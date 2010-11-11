using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace OpenSURFcs
{
  public class IPoint : ICloneable
  {
    /// <summary>
    /// Default ctor
    /// </summary>
    public IPoint()
    {
      orientation = 0;
    }

    /// <summary>
    /// Coordinates of the detected interest point
    /// </summary>
    public float x, y;

    /// <summary>
    /// Detected scale
    /// </summary>
    public float scale;

    /// <summary>
    /// Response of the detected feature (strength)
    /// </summary>
    public float response;

    /// <summary>
    /// Orientation measured anti-clockwise from +ve x-axis
    /// </summary>
    public float orientation;

    /// <summary>
    /// Sign of laplacian for fast matching purposes
    /// </summary>
    public int laplacian;

    /// <summary>
    /// Descriptor vector
    /// </summary>
    public int descriptorLength;
    public float [] descriptor = null;
    public void SetDescriptorLength(int Size)
    {
      descriptorLength = Size;
      descriptor = new float[Size];
    }

    public Object Clone()
    {
        return this.MemberwiseClone();
    }

    // C#
    public static object DeepClone(object obj)
    {
        // Create a "deep" clone of 
        // an object. That is, copy not only
        // the object and its pointers
        // to other objects, but create 
        // copies of all the subsidiary 
        // objects as well. This code even 
        // handles recursive relationships.

        object objResult = null;
        using (MemoryStream ms = new MemoryStream())
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, obj);

            // Rewind back to the beginning 
            // of the memory stream. 
            // Deserialize the data, then
            // close the memory stream.
            ms.Position = 0;
            objResult = bf.Deserialize(ms);
        }
        return objResult;
    }
  }
}
