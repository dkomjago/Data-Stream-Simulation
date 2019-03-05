using System;
using System.Collections.Generic;

namespace DataStreamSimulation
{
    /// <summary>
    /// Output to Microsoft Excel
    /// </summary>
    public static class ExcelManager
    {
        #region Excel References
        static Microsoft.Office.Interop.Excel.Application oXL;
        static Microsoft.Office.Interop.Excel._Workbook oWB;
        static Microsoft.Office.Interop.Excel._Worksheet oSheet;
        #endregion

        public static void Write(List<SimulationData> excelData)
        {
            foreach (var process in System.Diagnostics.Process.GetProcessesByName("Excel"))
            {
                process.Kill();
            }
            oXL = new Microsoft.Office.Interop.Excel.Application
            {
                Visible = true
            };

            //Add new Excel workbook
            oWB = oXL.Workbooks.Add("");
            oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;
            oSheet.EnableSelection = Microsoft.Office.Interop.Excel.XlEnableSelection.xlNoSelection;
            var i = 1;
            foreach (var data in excelData)
            {
                //Populate excel worksheet with data
                oSheet.Cells[i, 1] = data.Time;
                oSheet.Cells[i, 2] = data.Buffer;
                oSheet.Cells[i, 3] = data.BandWidth;
                oSheet.Cells[i, 4] = data.BitRate;

                i += 1;
            }
            //Draw line chart based on data
            Microsoft.Office.Interop.Excel.ChartObjects xlCharts = (Microsoft.Office.Interop.Excel.ChartObjects)oSheet.ChartObjects(Type.Missing);
            Microsoft.Office.Interop.Excel.ChartObject myChart = xlCharts.Add(10, 80, 300, 250);
            Microsoft.Office.Interop.Excel.Chart chartPage = myChart.Chart;
            chartPage.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine;
            var seriesCollection = (Microsoft.Office.Interop.Excel.SeriesCollection)chartPage.SeriesCollection();
            Microsoft.Office.Interop.Excel.Series series1 = seriesCollection.NewSeries();
            series1.Name = "Długość Bufora";
            series1.XValues = oSheet.get_Range("A1", "A" + i);
            series1.Values = oSheet.get_Range("B1", "B" + i);
            Microsoft.Office.Interop.Excel.Series series2 = seriesCollection.NewSeries();
            series2.Name = "Pasmo";
            series2.XValues = oSheet.get_Range("A1", "A" + i);
            series2.Values = oSheet.get_Range("C1", "C" + i);
            Microsoft.Office.Interop.Excel.Series series3 = seriesCollection.NewSeries();
            series3.Name = "Przeplywnosc fragmentu";
            series3.XValues = oSheet.get_Range("A1", "A" + i);
            series3.Values = oSheet.get_Range("D1", "D" + i);
        }
    }
}
