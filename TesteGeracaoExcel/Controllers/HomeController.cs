﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Office.Interop.Excel;

namespace TesteGeracaoControllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GeraExcel()
        {
            Application xlApp;
            Workbook xlWorkBook;
            Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //incluindo dados
            xlWorkSheet.Cells[1, 1] = "";
            xlWorkSheet.Cells[1, 2] = "Macoratti";
            xlWorkSheet.Cells[1, 3] = "Miriam";
            xlWorkSheet.Cells[1, 4] = "Jeffersom";

            xlWorkSheet.Cells[2, 1] = "Matemática";
            xlWorkSheet.Cells[2, 2] = "80";
            xlWorkSheet.Cells[2, 3] = "65";
            xlWorkSheet.Cells[2, 4] = "45";

            xlWorkSheet.Cells[3, 1] = "Química";
            xlWorkSheet.Cells[3, 2] = "78";
            xlWorkSheet.Cells[3, 3] = "72";
            xlWorkSheet.Cells[3, 4] = "60";

            xlWorkSheet.Cells[4, 1] = "Física";
            xlWorkSheet.Cells[4, 2] = "82";
            xlWorkSheet.Cells[4, 3] = "80";
            xlWorkSheet.Cells[4, 4] = "65";

            xlWorkSheet.Cells[5, 1] = "Português";
            xlWorkSheet.Cells[5, 2] = "75";
            xlWorkSheet.Cells[5, 3] = "82";
            xlWorkSheet.Cells[5, 4] = "68";

            Range chartRange;

            ChartObjects xlCharts = (ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
            ChartObject myChart = (ChartObject)xlCharts.Add(10, 80, 300, 250);
            Chart chartPage = myChart.Chart;

            chartRange = xlWorkSheet.get_Range("A1", "d5");
            chartPage.SetSourceData(chartRange, misValue);
            //chartPage.ChartType = XlChartType.xlColumnClustered;
            chartPage.ChartType = XlChartType.xl3DColumnClustered;

            xlWorkBook.SaveAs(@"D:\Work\Dropbox\PROJETOS AXYSWEB\xl3DColumnStacked.xls", XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            liberarObjetos(xlWorkSheet);
            liberarObjetos(xlWorkBook);
            liberarObjetos(xlApp);

            ViewBag.Message = "O excel foi gerado com sucesso.";

            return View("About");
        }

        private void liberarObjetos(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                ViewBag.Message = "Ocorreu um erro durante a liberação do objeto " + ex.ToString();
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}