using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DS.DAL.ComplexScripting
{
    public static class Colors
    {
        public static Color getColors(int i)
        {
            Color[] colorArray = new Color[69]
            {
                Color.AntiqueWhite,
                Color.Azure,
                Color.Beige,
                Color.Bisque,
                Color.BlanchedAlmond,
                Color.BurlyWood,
                Color.Cornsilk,
                Color.LightSkyBlue,
                Color.DarkKhaki,
                Color.FloralWhite,
                Color.Gainsboro,
                Color.GhostWhite,
                Color.Khaki,
                Color.LawnGreen,
                Color.LemonChiffon,
                Color.DarkSalmon,
                Color.LightBlue,
                Color.LightCyan,
                Color.LightGoldenrodYellow,
                Color.LightGreen,
                Color.LightGray,
                Color.LightPink,
                Color.DarkGray,
                Color.YellowGreen,
                Color.LightSkyBlue,
                Color.Yellow,
                Color.WhiteSmoke,
                Color.Violet,
                Color.DarkTurquoise,
                Color.Thistle,
                Color.Tan,
                Color.SpringGreen,
                Color.Snow,
                Color.Aquamarine,
                Color.SkyBlue,
                Color.Silver,
                Color.SeaShell,
                Color.SandyBrown,
                Color.SkyBlue,
                Color.Salmon,
                Color.RosyBrown,
                Color.PowderBlue,
                Color.Plum,
                Color.Pink,
                Color.PeachPuff,
                Color.PaleVioletRed,
                Color.PapayaWhip,
                Color.PaleTurquoise,
                Color.PaleGreen,
                Color.PaleGoldenrod,
                Color.Orchid,
                Color.Orange,
                Color.OldLace,
                Color.NavajoWhite,
                Color.MistyRose,
                Color.MintCream,
                Color.Moccasin,
                Color.MediumTurquoise,
                Color.MediumSpringGreen,
                Color.MediumSlateBlue,
                Color.MediumSeaGreen,
                Color.LightSkyBlue,
                Color.MediumAquamarine,
                Color.Magenta,
                Color.Linen,
                Color.LightYellow,
                Color.LightSteelBlue,
                Color.LightSkyBlue,
                Color.LightSalmon
            };
            ColorConverter colorConverter = new ColorConverter();
            return colorArray[i];
        }
    }
}
