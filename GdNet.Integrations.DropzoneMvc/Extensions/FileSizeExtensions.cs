using System;
using System.Globalization;
using System.IO;

namespace GdNet.Integrations.DropzoneMvc.Extensions
{
    public static class FileSizeExtensions
    {
        /// <summary>
        /// Converts a numeric value into a string that represents the number expressed as a size value in bytes,
        /// kilobytes, megabytes, or gigabytes, depending on the size.
        /// </summary>
        /// <param name="fileSize">The numeric value to be converted.</param>
        /// <returns>The converted string.</returns>
        public static string FormatByteSize(this long fileSize)
        {
            var unit = FileSizeUnit.B;
            var sizeAsDouble = Convert.ToDouble(fileSize);

            while (sizeAsDouble >= 1024 && unit < FileSizeUnit.YB)
            {
                sizeAsDouble = sizeAsDouble / 1024;
                unit++;
            }

            return string.Format("{0} {1}", sizeAsDouble.ToString("0.##", CultureInfo.CurrentUICulture), unit);
        }

        /// <summary>
        /// Converts a numeric value into a string that represents the number expressed as a size value in bytes,
        /// kilobytes, megabytes, or gigabytes, depending on the size.
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns>The converted string.</returns>
        public static string FormatByteSize(this FileInfo fileInfo)
        {
            return FormatByteSize(fileInfo.Length);
        }
    }
}
