using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BanlineaTest.Contacts.Converters
{
    public class ImageNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte[] imageBytes;

            if (value == null) {
                return parameter;
            }

            if (value is string)
            {
                imageBytes = System.Convert.FromBase64String(value.ToString());
                return ImageSource.FromStream(() => new System.IO.MemoryStream(imageBytes));
            }

            imageBytes = value as byte[];
            if (imageBytes == null) {
                return parameter;
            }

            return ImageSource.FromStream(() => new System.IO.MemoryStream(imageBytes));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return value;
        }
    }
}
