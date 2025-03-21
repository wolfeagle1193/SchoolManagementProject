using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementProjecture
{
    public static class ThemeColor

    {
        public static List<string> ColorList = new List<string>()
        {
           "#E8A0A0", "#FFFF00", "#FF6347", "#8B0000", "#008000", "#800080", "#FFA500"," #00FFFF"," #FF00FF", "#FFAE00"
        };
        public static Color ChangeColorBrightness(Color color, double correctionFactor)
        {
            double blue = color.B;
            double red = color.R;
            double green = color.G;

            if (correctionFactor < 0)
            {

                correctionFactor = 1 + correctionFactor;
                blue *= correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
            }

            else
            {

                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }
            return Color.FromArgb(color.A,(byte)red,(byte)green,(byte)blue);
        }
    };
};
