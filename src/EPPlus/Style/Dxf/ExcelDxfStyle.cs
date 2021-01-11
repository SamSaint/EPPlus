/*************************************************************************************************
  Required Notice: Copyright (C) EPPlus Software AB. 
  This software is licensed under PolyForm Noncommercial License 1.0.0 
  and may only be used for noncommercial purposes 
  https://polyformproject.org/licenses/noncommercial/1.0.0/

  A commercial license to use this software can be purchased at https://epplussoftware.com
 *************************************************************************************************
  Date               Author                       Change
 *************************************************************************************************
  12/28/2020         EPPlus Software AB       EPPlus 5.6
 *************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using System.Drawing;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Utils.Extensions;
using OfficeOpenXml.Drawing.Theme;

namespace OfficeOpenXml.Style.Dxf
{
    /// <summary>
    /// Differential formatting record used in conditional formatting
    /// </summary>
    public class ExcelDxfStyle : ExcelDxfStyleBase
    {
        internal ExcelDxfStyle(XmlNamespaceManager nameSpaceManager, XmlNode topNode, ExcelStyles styles, string dxfIdPath) 
            : base(nameSpaceManager,topNode, styles, dxfIdPath)
        {
            Font = new ExcelDxfFont(styles);
            if (topNode != null)
            {
                Font.GetValuesFromXml(_helper);
            }
        }
        internal override int DxfId 
        {
            get 
            {
                return _helper.GetXmlNodeInt(_dxfIdPath);
            }
            set
            {
                _helper.SetXmlNodeInt(_dxfIdPath, value);
            }
        }
        /// <summary>
        /// Font formatting settings
        /// </summary>
        public ExcelDxfFont Font { get; set; }
        /// <summary>
        /// Clone the object
        /// </summary>
        /// <returns>A new instance of the object</returns>
        protected internal override DxfStyleBase Clone()
        {
            var s = new ExcelDxfStyle(_helper.NameSpaceManager, null, _styles, _dxfIdPath);
            s.Font = (ExcelDxfFont)Font.Clone();
            s.NumberFormat = (ExcelDxfNumberFormat)NumberFormat.Clone();
            s.Fill = (ExcelDxfFill)Fill.Clone();
            s.Border = (ExcelDxfBorderBase)Border.Clone();
            return s;
        }
        protected internal override void CreateNodes(XmlHelper helper, string path)
        {
            if (Font.HasValue) Font.CreateNodes(helper, "d:font");
            base.CreateNodes(helper, path);
        }
        public override bool HasValue
        {
            get
            {
                return Font.HasValue || base.HasValue;
            }
        }
        protected internal override string Id
        {
            get
            {
                return Font.Id + base.Id;
            }
        }
    }
}
