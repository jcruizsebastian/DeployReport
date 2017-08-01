using System.Collections.Generic;
using System.IO;
using System.Linq;
using DeploymentReport;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            JiraRepository jirarepo = new JiraRepository();

            GenerateExcel(new List<Issue>() { jirarepo.Get("CORPORT-1845"), jirarepo.Get("CORCR10-12"), jirarepo.Get("CORPORT-1825"), jirarepo.Get("CORPORT-1841"), jirarepo.Get("CORPORT-1814"), jirarepo.Get("CORPORT-1831"), jirarepo.Get("CORPORT-1850"), jirarepo.Get("CORPORT-1848"), jirarepo.Get("CORPORT-1818"), jirarepo.Get("CORPORT-1849"), jirarepo.Get("CORPORT-1840"), jirarepo.Get("CORCR10-12"), jirarepo.Get("CORCR10-11"), jirarepo.Get("CORPORT-1855"), jirarepo.Get("CORPORT-1839"), jirarepo.Get("CORPORT-1813"), jirarepo.Get("CORPORT-1812"), jirarepo.Get("CORPORT-1827"), jirarepo.Get("CORPORT-1842"), jirarepo.Get("CORPORT-1835"), jirarepo.Get("CORPORT-1853") });
        }


        public static void GenerateExcel(List<Issue> issues)
        {
            IWorkbook workbook = new XSSFWorkbook();

            ISheet sheet1 = workbook.CreateSheet("Datos");
            
            int rowCounter = 0;

            sheet1.CreateRow(rowCounter++).CreateCell(0).SetCellValue("Nueva subida");

            int cellCounter = 1;

            IRow rowComponents = sheet1.CreateRow(rowCounter++);

            var allcomponentsNames = issues.SelectMany(x => x.Components).Select(y => y.Name).Distinct().ToList();
            foreach (string componentName in allcomponentsNames)
            {
                rowComponents.CreateCell(cellCounter++).SetCellValue(componentName);
            }
            

            IFormulaEvaluator evaluator = workbook.GetCreationHelper().CreateFormulaEvaluator();

            foreach (Issue issue in issues)
            {
                cellCounter = 0;

                IRow row = sheet1.CreateRow(rowCounter++);
                var cell = row.CreateCell(cellCounter++);
                cell.SetCellType(CellType.Formula);
                cell.SetCellFormula(string.Format("Hyperlink(\"http://jira.openfinance.es/browse/{0}\",\"{0}\")", issue.ID));
                evaluator.NotifySetFormula(cell);
                evaluator.Evaluate(cell);
                evaluator.ClearAllCachedResultValues();

                foreach (string componentName in allcomponentsNames)
                {
                    if (issue.Components.Count(x => x.Name == componentName) > 0)
                    {
                        var cellCross = row.CreateCell(cellCounter);
                        cellCross.SetCellValue("X");
                    }
                    cellCounter++;
                }
            }

            evaluator.EvaluateAll();

            FileStream sw = File.Create("test.xlsx");

            workbook.Write(sw);



            sw.Close();
        }
    }
}
