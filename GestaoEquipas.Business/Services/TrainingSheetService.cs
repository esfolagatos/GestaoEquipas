using GestaoEquipas.Data.Models;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace GestaoEquipas.Business.Services
{
    public class TrainingSheetService
    {
        public void ExportToPdf(TrainingSheet sheet, string filePath)
        {
            var doc = new PdfDocument();
            var page = doc.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var fontTitle = new XFont("Verdana", 20, XFontStyle.Bold);
            var font = new XFont("Verdana", 12);

            int y = 40;
            gfx.DrawString($"Treino: {sheet.Date:yyyy-MM-dd}", fontTitle, XBrushes.Black, new XPoint(40, y));
            y += 30;
            gfx.DrawString(sheet.Notes, font, XBrushes.Black, new XPoint(40, y));
            y += 30;
            foreach (var ex in sheet.Exercises)
            {
                gfx.DrawString($"- {ex.Name}: {ex.Description}", font, XBrushes.Black, new XPoint(50, y));
                y += 20;
            }

            doc.Save(filePath);
        }
    }
}
