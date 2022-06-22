using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty_Saloon.Model
{
    public partial class Service
    {
        public decimal PriceWithDiscount
        {
            get
            {
                if (Discount != null)
                {
                    return (decimal)(Convert.ToInt32(this.Cost) - (Convert.ToInt32(this.Cost)/100*this.Discount));
                }
                else
                {
                    return this.Cost;
                }
            }
            set { }
        }
        public string mainImagePath;
        public string MainImagePath
        {
            get { return mainImagePath; }
            set { mainImagePath = value; }
        }
        [NotMapped]
        public string MainImagePathSet
        {
            get { return "pack://siteoforigin:,,,/" + mainImagePath; }
        }
        public string dopImagesPath1;
        public string DopImagesPath1
        {
            get { return dopImagesPath1; }
            set { dopImagesPath1 = value; }
        }
        [NotMapped]
        public string DopImagesPath1Set
        {
            get { return "pack://siteoforigin:,,,/" + dopImagesPath1; }
        }
        public string dopImagesPath2;
        public string DopImagesPath2
        {
            get { return dopImagesPath2; }
            set { dopImagesPath2 = value; }
        }
        [NotMapped]
        public string DopImagesPath2Set
        {
            get { return "pack://siteoforigin:,,,/" + dopImagesPath2; }
        }
    }
}
