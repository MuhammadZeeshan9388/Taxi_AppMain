using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.WindowsForms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Taxi_App
{
    public class GMapMarkerCustom : GMapMarker
    {
        private string _PDALocationName;

        public string PDALocationName
        {
            get { return _PDALocationName; }
            set { _PDALocationName = value; }
        }


        private string _WorkStatus;

        public string WorkStatus
        {
            get { return _WorkStatus; }
            set { _WorkStatus = value; }
        }



        private Image _MarkerImage;

        public Image MarkerImage
        {
            get { return _MarkerImage; }
            set { _MarkerImage = value; }
        }


        public GMapMarkerCustom(GMap.NET.PointLatLng pos, Image img)
            : base(pos)
        {

            this.MarkerImage = img;
        }




        public override void OnRender(System.Drawing.Graphics g)
        {
         
             if(this.MarkerImage!=null)
               g.DrawImage(this.MarkerImage, this.LocalPosition);

         
            base.OnRender(g);
        }

      


       
    }
}
