using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.WinControls;
using System.Windows.Forms;
using Utils;
using System.Xml.Linq;
using System.Xml;

namespace Taxi_AppMain
{
    class ENUtils
    {
        private static string theme = "Aqua";
        public static void ShowMessage(string message)
        {
            RadMessageBox.SetThemeName(theme);
            RadMessageBox.Show(message);


        }

        public static DialogResult ShowErrorMessage(string message)
        {
            RadMessageBox.SetThemeName(theme);
            DialogResult result = RadMessageBox.Show(message, "Warning!", MessageBoxButtons.YesNo);

            return result;
        }


        public static XDocument RemoveNamespaces(XDocument oldXml)
        {
            // FROM: http://social.msdn.microsoft.com/Forums/en-US/bed57335-827a-4731-b6da-a7636ac29f21/xdocument-remove-namespace?forum=linqprojectgeneral
            try
            {
                XDocument newXml = XDocument.Parse(System.Text.RegularExpressions.Regex.Replace(
                    oldXml.ToString(),
                    @"(xmlns:?[^=]*=[""][^""]*[""])",
                    "",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Multiline)
                );
                return newXml;
            }
            catch (XmlException error)
            {
                throw new XmlException(error.Message + " at Utils.RemoveNamespaces");
            }
        }

        /// <summary>
        /// Remove all xmlns:* instances from the passed XmlDocument to simplify our xpath expressions
        /// </summary>
        public static XDocument RemoveNamespaces(string oldXml)
        {
            XDocument newXml = XDocument.Parse(oldXml);
            return RemoveNamespaces(newXml);
        }

        /// <summary>
        /// Converts a string that has been HTML-enconded for HTTP transmission into a decoded string.
        /// </summary>
        /// <param name="escapedString">String to decode.</param>
        /// <returns>Decoded (unescaped) string.</returns>
        public static string UnescapeString(string escapedString)
        {
            return System.Web.HttpUtility.HtmlDecode(escapedString);
        }

    

    }
}
