using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace site.core
{
    public class Img
    {
        public string Id { get; set; }

        public string ImgSetId { get; set; }

        public int Order { get; set; }

        public bool IsTitle { get; set; }

        public int XPixelOffset { get; set; }

        public int YPixelOffset { get; set; }

        public string Base64Data { get; set; }
    }
}
