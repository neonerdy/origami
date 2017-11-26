
/*===========================================================
 * 
 * Origami (Object Relational Gateway Microarchitecture)
 * 
 * Lightweight Enterprise Application Framework
 *
 * Version  : 3.0
 * Author   : Ariyanto
 * E-Mail   : neonerdy@yahoo.com
 *  
 * 
 * © 2009, Under Apache Licence 
 * 
 *==========================================================
 */

using System;
using System.Data;
using System.Data.Common;
using System.IO;

namespace Origami.Data
{
    public enum DocType
    {
        Excel, Csv, Html, Xml
    }

    public class Transformer
    {
        private DbHelper dbHelper;

        public Transformer(DbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public void Export2Html(string sql, string fileName)
        {
            StreamWriter htmlDoc;

            htmlDoc = new StreamWriter(fileName);

            htmlDoc.Write("<HTML><HEAD><TITLE></TITLE></HEAD>\n");
            htmlDoc.Write("<BODY>\n");
            htmlDoc.Write("<TABLE BORDER=1>\n");

            DataSet dataSet = dbHelper.ExecuteDataSet(sql);

            htmlDoc.Write("\t<TR>\n");
            foreach (DataColumn col in dataSet.Tables[0].Columns)
            {
                htmlDoc.Write("\t\t<TD><B>" + col.ColumnName + "</B></TD>\n");
            }
            htmlDoc.Write("\t</TR>\n");

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                htmlDoc.Write("\t<TR>\n");
                foreach (DataColumn col in dataSet.Tables[0].Columns)
                {
                    htmlDoc.Write("\t\t<TD>" + row[col.ColumnName] + "</TD>\n");
                }
                htmlDoc.Write("\t</TR>\n");
            }

            htmlDoc.Write("</TABLE>\n");
            htmlDoc.Write("</BODY></HTML>\n");

            htmlDoc.Close();
        }


        public void Export2Xml(string sql, string fileName)
        {
            StreamWriter xmlDoc;

            xmlDoc = new StreamWriter(fileName);

            DataSet dataSet = dbHelper.ExecuteDataSet(sql);

            xmlDoc.Write("<? xml version=\"1.0\">\n");
            xmlDoc.Write("<DataSet>\n");

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                xmlDoc.Write("\t<Data>\n");
                foreach (DataColumn col in dataSet.Tables[0].Columns)
                {
                    xmlDoc.Write("\t\t<" + col.ColumnName + ">" + row[col.ColumnName] + "</" + col.ColumnName + ">\n");
                }
                xmlDoc.Write("\t</Data>\n");
            }

            xmlDoc.Write("</DataSet>");
            xmlDoc.Close();
        }


        public void Export2Csv(string sql, string fileName)
        {
            StreamWriter csvDoc = new StreamWriter(fileName);

            DataSet ds = dbHelper.ExecuteDataSet(sql);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string rows = "";

                foreach (DataColumn col in ds.Tables[0].Columns)
                {
                    string colName = row[col.ColumnName] + ";";
                    rows = rows + colName;
                }
                csvDoc.Write(rows.Substring(0, rows.Length - 1));
                csvDoc.Write("\n");
            }
            csvDoc.Close();

        }


        public void Export2Excel(string sql, string fileName)
        {
            StreamWriter excelDoc;

            DataSet source = dbHelper.ExecuteDataSet(sql);

            excelDoc = new StreamWriter(fileName);

            const string startExcelXML = "<xml version>\r\n<Workbook " +
                  "xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n" +
                  " xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n " +
                  "xmlns:x=\"urn:schemas-    microsoft-com:office:" +
                  "excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:" +
                  "office:spreadsheet\">\r\n <Styles>\r\n " +
                  "<Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n " +
                  "<Alignment ss:Vertical=\"Bottom\"/>\r\n <Borders/>" +
                  "\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>" +
                  "\r\n <Protection/>\r\n </Style>\r\n " +
                  "<Style ss:ID=\"BoldColumn\">\r\n <Font " +
                  "x:Family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n " +
                  "<Style     ss:ID=\"StringLiteral\">\r\n <NumberFormat" +
                  " ss:Format=\"@\"/>\r\n </Style>\r\n <Style " +
                  "ss:ID=\"Decimal\">\r\n <NumberFormat " +
                  "ss:Format=\"0.0000\"/>\r\n </Style>\r\n " +
                  "<Style ss:ID=\"Integer\">\r\n <NumberFormat " +
                  "ss:Format=\"0\"/>\r\n </Style>\r\n <Style " +
                  "ss:ID=\"DateLiteral\">\r\n <NumberFormat " +
                  "ss:Format=\"mm/dd/yyyy;@\"/>\r\n </Style>\r\n " +
                  "</Styles>\r\n ";
            const string endExcelXML = "</Workbook>";

            int rowCount = 0;
            int sheetCount = 1;

            excelDoc.Write(startExcelXML);
            excelDoc.Write("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
            excelDoc.Write("<Table>");
            excelDoc.Write("<Row>");

            for (int x = 0; x < source.Tables[0].Columns.Count; x++)
            {
                excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                excelDoc.Write(source.Tables[0].Columns[x].ColumnName);
                excelDoc.Write("</Data></Cell>");
            }
            excelDoc.Write("</Row>");
            foreach (DataRow x in source.Tables[0].Rows)
            {
                rowCount++;
                //if the number of rows is > 64000 create a new page to continue output

                if (rowCount == 64000)
                {
                    rowCount = 0;
                    sheetCount++;
                    excelDoc.Write("</Table>");
                    excelDoc.Write(" </Worksheet>");
                    excelDoc.Write("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
                    excelDoc.Write("<Table>");
                }
                excelDoc.Write("<Row>"); //ID=" + rowCount + "

                for (int y = 0; y < source.Tables[0].Columns.Count; y++)
                {
                    System.Type rowType;
                    rowType = x[y].GetType();
                    switch (rowType.ToString())
                    {
                        case "System.String":
                            string XMLstring = x[y].ToString();
                            XMLstring = XMLstring.Trim();
                            XMLstring = XMLstring.Replace("&", "&");
                            XMLstring = XMLstring.Replace(">", ">");
                            XMLstring = XMLstring.Replace("<", "<");
                            excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                           "<Data ss:Type=\"String\">");
                            excelDoc.Write(XMLstring);
                            excelDoc.Write("</Data></Cell>");
                            break;

                        case "System.DateTime":

                            DateTime XMLDate = (DateTime)x[y];
                            string XMLDatetoString = ""; //Excel Converted Date

                            XMLDatetoString = XMLDate.Year.ToString() +
                                 "-" +
                                 (XMLDate.Month < 10 ? "0" +
                                 XMLDate.Month.ToString() : XMLDate.Month.ToString()) +
                                 "-" +
                                 (XMLDate.Day < 10 ? "0" +
                                 XMLDate.Day.ToString() : XMLDate.Day.ToString()) +
                                 "T" +
                                 (XMLDate.Hour < 10 ? "0" +
                                 XMLDate.Hour.ToString() : XMLDate.Hour.ToString()) +
                                 ":" +
                                 (XMLDate.Minute < 10 ? "0" +
                                 XMLDate.Minute.ToString() : XMLDate.Minute.ToString()) +
                                 ":" +
                                 (XMLDate.Second < 10 ? "0" +
                                 XMLDate.Second.ToString() : XMLDate.Second.ToString()) +
                                 ".000";
                            excelDoc.Write("<Cell ss:StyleID=\"DateLiteral\">" +
                                         "<Data ss:Type=\"DateTime\">");
                            excelDoc.Write(XMLDatetoString);
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.Boolean":
                            excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                        "<Data ss:Type=\"String\">");
                            excelDoc.Write(x[y].ToString());
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            excelDoc.Write("<Cell ss:StyleID=\"Integer\">" +
                                    "<Data ss:Type=\"Number\">");
                            excelDoc.Write(x[y].ToString());
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.Decimal":
                        case "System.Double":
                            excelDoc.Write("<Cell ss:StyleID=\"Decimal\">" +
                                  "<Data ss:Type=\"Number\">");
                            excelDoc.Write(x[y].ToString());
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.DBNull":
                            excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                  "<Data ss:Type=\"String\">");
                            excelDoc.Write("");
                            excelDoc.Write("</Data></Cell>");
                            break;
                        default:
                            throw (new Exception(rowType.ToString() + " not handled."));
                    }
                }
                excelDoc.Write("</Row>");
            }

            excelDoc.Write("</Table>");
            excelDoc.Write(" </Worksheet>");
            excelDoc.Write(endExcelXML);
            excelDoc.Close();

        }

    }
}
