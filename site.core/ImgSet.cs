using System.Collections.Generic;
using System.ComponentModel;

namespace site.core.Models
{
    public class ImgSet
    {
        public string Id { get; set; }

        [DisplayName("Название")]
        public string Title { get; set; }

        [DisplayName("Порядок")]
        public int Order { get; set; }

        [DisplayName("Тип")]
        public ImgSetType Type { get; set; }

        [DisplayName("Титульное фото")]
        public string Base64TitleImg { get; set; }

        public List<Img> Images { get; set; }

        public static ImgSet Empty
        {
            get
            {
                return new ImgSet
                {
                    Id = "",
                    Title = "",
                    Images = new List<Img> { }
                };
            }
        }
    }
}