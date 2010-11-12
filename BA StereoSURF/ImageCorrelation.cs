using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

using Microsoft.Xna.Framework;

using OpenSURFcs;

namespace BA_StereoSURF
{
    public class ImageCorrelation
    {       
        /* statics */
        // accuracy in percent
        public const float ACCURACY_JOINT       = 0.01f;
        public const float ACCURACY_ORIENTATION = 0.05f;
        public const float ACCURACY_SCALE       = 20f; // total Pixel-difference
        public const float ACCURACY_SURF        = 0.10f;

        /* members */
        private ExtendedImage _baseImage;
        private List<ExtendedImage> _refImages;
        private Dictionary<ExtendedImage, List<CorrelationInfo>> _correlations;

        /* constructors */
        /// <summary>
        /// Erzeugt ein Objekt, das die Beziehung eines Bildes zu den Anderen repräsentiert.
        /// </summary>
        /// <param name="baseImage">Ausgangsbild mit gefülltem SurfDescriptor</param>
        public ImageCorrelation(ExtendedImage baseImage)
        {
            _baseImage = baseImage;
            _refImages = new List<ExtendedImage>();
            _correlations = new Dictionary<ExtendedImage, List<CorrelationInfo>>();
        }

        /* methods */
        /// <summary>
        /// Fügt diverses weitere Bild hinzu um daraus weitere Korrelationen zu errechnen
        /// </summary>
        /// <param name="eImg">Bild mit zuvor berechneten SURF-Daten</param>
        public void AddReferenceImage(ExtendedImage eImg)
        {
            if (eImg.InterestPointsSURF.Count > 0)
            {
                _refImages.Add(eImg);
                _correlations.Add(eImg, getCorrelationInfos(eImg));
            }    
        }   
        /// <summary>
        /// Fügt diverse weitere Bilder hinzu um daraus weitere Korrelationen zu errechnen
        /// </summary>
        /// <param name="range">Bilder mit zuvor berechneten SURF-Daten</param>
        public void AddReferenceImages(List<ExtendedImage> range)
        {
            range.ForEach((item) => { AddReferenceImage(item); });
        }
        /// <summary>
        /// Gibt eine Liste mit allen gefundenen Korrelationen zurück
        /// </summary>
        /// <param name="eImg">Referenzbild</param>
        /// <returns>Eine Liste mit allen Korrelationsinformationen</returns>
        public List<CorrelationInfo> GetCorrelationInfo(ExtendedImage eImg)
        {
            if (_correlations.ContainsKey(eImg))
                return _correlations[eImg];
            else
                return null;
        }
        /// <summary>
        /// Liefert ein fertiges Depthmap-Bild in der Perspektive des Ausgangsbildes
        /// </summary>
        /// <param name="simple">Nur ein Referenzbild wird genutzt</param>
        /// <param name="hdr">Resultierendes Bild hat 32bit Farbtiefe</param>
        /// <returns></returns>
        public Image GenerateDepthmap(bool simple, bool hdr)
        {
            if (simple)
            {

            }
            else
            {   // iterativ mit allen Bildern der Range

            }
        }

        private List<CorrelationInfo> getCorrelationInfos(ExtendedImage eImg)
        {
            List<CorrelationInfo> returnList = new List<CorrelationInfo>();
            foreach (IPoint ip in eImg.InterestPointsSURF)
            {
                // das lustige Finde den richtigen KeyPoint zum KeyPoint-Spiel
                float scale_W = eImg.Image.Width / _baseImage.Image.Width;
                float scale_H = eImg.Image.Height / _baseImage.Image.Height;

                // 1. angle-guess
                List<IPoint> withCorrectAngle = _baseImage.InterestPointsSURF.FindAll(delegate(IPoint thisIPoint)
                {
                    if (thisIPoint.laplacian == ip.laplacian)
                    {
                        if (Math.Abs(thisIPoint.orientation - ip.orientation) / (2 * Math.PI) < ImageCorrelation.ACCURACY_ORIENTATION)
                        {
                            if (Math.Abs(Convert.ToInt32(5f * ip.scale) - Convert.ToInt32(5f * thisIPoint.scale)) < ImageCorrelation.ACCURACY_SCALE)
                            {
                                return true;
                            }
                            else { return false; }
                        }
                        else { return false; }
                    } 
                    else { return false; }
                });

                // 2. Translation guess
                // TODO:    wenn der DescriptorProof eingebastelt ist, müsste man hier mehrere durchlassen, 
                //          die in der Nähe sind (evtl. auch nochmal mit Winkel & PatternScale abgleichen
                float best_distance = float.MaxValue;
                IPoint best_fit = null;
                foreach (IPoint base_ip in withCorrectAngle)
                {   // catch the closest
                    Vector2 v2 = new Vector2(base_ip.x - ip.x, base_ip.y - ip.y);
                    float d = v2.Length();
                    if (d < best_distance)
                    {
                        best_distance = d;
                        best_fit = base_ip;
                    }
                }
                if (best_fit != null) 
                {
                    CorrelationInfo ci = new CorrelationInfo(best_fit.x, best_fit.y, ip.x, ip.y, _baseImage.Image.Size, eImg.Image.Size, Vector3.Right);
                    if (ci.Valid)
                    {
                        returnList.Add(ci);
                        //System.Diagnostics.Debug.WriteLine(String.Format("CP #{0}:\tx={1}\ty={2}\trot={3}", returnList.Count, ci.Xa, ci.Ya, best_fit.orientation));
                        // ich glaubs nicht da kommt ja auf anhieb was bei raus xD
                    }
                }

                // TODO:    3. rotation-normalized descriptor proof                            

            }
            return returnList;
        }

        /* props */ 

    }
}
