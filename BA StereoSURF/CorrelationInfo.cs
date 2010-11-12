using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.Xna.Framework;

namespace BA_StereoSURF
{
    public class CorrelationInfo
    {
        /* members */
        private bool _horizontal;

        private float _x; // x position at base image
        private float _y; // y position at base image
        private float _ref_x; // x position at refered image
        private float _ref_y; // y position at refered image        
        private float _z;   // target value of depth, calculated by method out of expected joint and real joint

        private Size _image_size;
        private Size _refImage_size;

        private Vector3 _joint_prospect;    // normalized aim linking Vector (calculated by the projection-matrices of the 2 images) 
        private Vector3 _joint;             // linking vector of PimgA(x,y,[z]) and PimgB(x,y,[z])

        /* constructor */
        /// <summary>
        /// Erzeugt ein Objekt, welches die Korrelationsinforamtionen berechnet
        /// </summary>
        /// <param name="x">x-Position des FeaturePoints auf Ausgangsbild</param>
        /// <param name="y">y-Position des FeaturePoints auf Ausgangsbild</param>
        /// <param name="x2">x-Position des FeaturePoints auf Bezugsbild</param>
        /// <param name="y2">y-Position des FeaturePoints auf Bezugsbild</param>
        /// <param name="imageSize">Abmessung des Ausgangsbildes in px</param>
        /// <param name="refImageSize">Abmessung des Bezugsbildes in px</param>
        /// <param name="jointProspect">Erwarteter Verbindungsvektor (normalisiert), errechnet aus den beiden Projektionsmatrizen der Bilder</param>
        public CorrelationInfo(float x, float y, float x2, float y2, Size imageSize, Size refImageSize, Vector3 jointProspect)
        {
            _horizontal = false;

            _x = x;
            _y = y;
            _ref_x = x2;
            _ref_y = y2;
            _z = float.NaN;

            _image_size = imageSize;
            _refImage_size = refImageSize;
            _joint_prospect = jointProspect;

            calculateDepth();
        }

        /* methods */
        /// <summary>
        /// Erzeugt den Verbindungsvektor der beiden Punkte
        /// </summary>
        private void calculateJoint()
        {
            // TODO: erstmal nur 2D auf dem Bild, irgendwie müsste hier schon der Erwartungsvektor 
            // mit eingerechnet werden um die Verschiebung der Punkte auszugleichen
            // aber bei exakt parallelen Kameras (gleicher Projektions-Matrix) sollte es so erst einmal gehen
            float scale_H = 1.0f;
            float scale_W = 1.0f;
            if (_image_size.Height != _refImage_size.Height || _image_size.Width != _refImage_size.Width)
            {
                scale_H = _refImage_size.Height / _image_size.Height;
                scale_W = _refImage_size.Width / _image_size.Width;
            }
            _joint = new Vector3((_ref_x * scale_W) - _x, (_ref_y * scale_H) - _y, 0);

            // Folgendes prüft ob Abweichung innerhalt der Toleranz
            if (Math.Abs((_ref_y * scale_H) - _y) / _image_size.Height > ImageCorrelation.ACCURACY_JOINT)
                _horizontal = false;
            else
                _horizontal = true;
        }

        /// <summary>
        /// Berechnet die Tiefeninformation dieser Korrelation anhand aller gegebenen Werte
        /// </summary>
        private void calculateDepth()
        {
            calculateJoint();

            if (_horizontal)
            {
                // TODO: ja ja ich weiß da gehört noch einiges mehr dazu ^^ will erstmal sehen obs überhaupt klappt 
                // (Abstand und Ausrichtung beider Kameras noch relevant!)
                _z = _joint.Length();
            }
        }

        /* props */
        /// <summary>
        /// Gibt true falls sich der Verbindungsvektor im Rahmen der Toleranz bewegt
        /// </summary>
        public bool Valid { get { return _horizontal; } }
        /// <summary>
        /// x-Position des FeaturePoints auf dem Ausgangsbild
        /// ([set] lässt die Tiefeninformation neu berechnen)
        /// </summary>
        public float Xa { get { return _x; } set { _x = value; calculateDepth(); } }
        /// <summary>
        /// y-Position des FeaturePoints auf dem Ausgangsbild
        /// ([set] lässt die Tiefeninformation neu berechnen)
        /// </summary>
        public float Ya { get { return _y; } set { _y = value; calculateDepth(); } }
        /// <summary>
        /// x-Position des FeaturePoints auf dem Bezugsbild
        /// ([set] lässt die Tiefeninformation neu berechnen)
        /// </summary>
        public float Xb { get { return _ref_x; } set { _ref_x = value; calculateDepth(); } }
        /// <summary>
        /// y-Position des FeaturePoints auf dem Bezugsbild
        /// ([set] lässt die Tiefeninformation neu berechnen)
        /// </summary>
        public float Yb { get { return _ref_y; } set { _ref_y = value; calculateDepth(); } }
        /// <summary>
        /// Liefert errechneten Tiefeninformation für diesen Punkt
        /// </summary>
        public float Depth { get { return _z; } }
        /// <summary>
        /// Erwarteter Verbindungsvektor (normalisiert), der sich aus den 2 Perspektiv-Matrizen errechnet
        /// ([set] lässt die Tiefeninformation neu berechnen)
        /// </summary>
        public Vector3 JointProspect { get { return _joint_prospect; } set { _joint_prospect = value; calculateDepth(); } }
        /// <summary>
        /// Verbindungsvektor, der sich aus den beiden Punkten und dem erwartetem Vektor ergibt.
        /// ([set] lässt die Tiefeninformation neu berechnen)
        /// </summary>
        public Vector3 Joint { get { return _joint; } set { _joint = value; calculateDepth(); } }

    }
}
