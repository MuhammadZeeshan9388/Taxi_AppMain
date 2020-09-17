using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Taxi_AppMain.Classes
{
    public class ClsLogo
    {

        private Image _ImageFile;

        public Image ImageFile
        {
            get { return _ImageFile; }
            set { _ImageFile = value; }
        }

        private byte[] _ImageInBytes;

        public byte[] ImageInBytes
        {
            get { return _ImageInBytes; }
            set { _ImageInBytes = value; }
        }


    }
}
