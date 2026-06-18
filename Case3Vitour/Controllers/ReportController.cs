using Microsoft.AspNetCore.Mvc;
using Case3Vitour.Services.CategoryServices;
using Case3Vitour.Services.ReservationServices;
using Case3Vitour.Services.ReviewServices;
using Case3Vitour.Services.TourServices;
using Case3Vitour.Services.ReportServices;

// iText 9 Modern Paketleri
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

// Excel Paketi
using ClosedXML.Excel;

namespace Case3Vitour.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly ITourService _tourService;
        private readonly IReviewService _reviewService;
        private readonly ICategoryService _categoryService;
        private readonly IReservationService _reservationService;

        public ReportController(IReportService reportService, ITourService tourService, IReviewService reviewService, ICategoryService categoryService, IReservationService reservationService)
        {
            _reportService = reportService;
            _tourService = tourService;
            _reviewService = reviewService;
            _categoryService = categoryService;
            _reservationService = reservationService;
        }

        public async Task<IActionResult> ReportList()
        {
            ViewBag.KpiCards = await _reportService.GetKpiCardsAsync();
            ViewBag.MonthlyGoals = await _reportService.GetMonthlyGoalsAsync();
            ViewBag.TopTours = await _reportService.GetTopTourListAsync();
            ViewBag.RevenueChart = await _reportService.GetMonthlyRevenueChartAsync();
            return View();
        }

        // --- 1. TUR REZERVASYONLARI PDF RAPORU ---
        public async Task<IActionResult> DownloadTourReservationsPdf(string id)
        {
            var tour = await _tourService.GetTourByIdAsync(id);
            var reservations = await _reservationService.GetReservationsByTourIdAsync(id);

            using (var stream = new MemoryStream())
            {
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                // iText Türkçe Karakter Desteği
                string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                iText.Kernel.Font.PdfFont turkishFont = iText.Kernel.Font.PdfFontFactory.CreateFont(fontPath, iText.IO.Font.PdfEncodings.IDENTITY_H);
                document.SetFont(turkishFont);

                // ÇÖKME HATASI ÇÖZÜMÜ: Tur silinmişse null gelmesini engelliyoruz
                string tourTitle = tour != null ? tour.Title : "Silinmiş / Bilinmeyen Tur";

                Paragraph header = new Paragraph($"{tourTitle} - Rezervasyon Listesi")
                    .SetFontSize(16)
                    .SetTextAlignment(TextAlignment.CENTER);
                document.Add(header);

                document.Add(new Paragraph("\n"));

                Table table = new Table(UnitValue.CreatePercentArray(new float[] { 40, 40, 20 })).UseAllAvailableWidth();

                table.AddHeaderCell(new Cell().Add(new Paragraph("Müşteri Ad Soyad")));
                table.AddHeaderCell(new Cell().Add(new Paragraph("E-posta")));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Kişi Sayısı")));

                foreach (var res in reservations)
                {
                    table.AddCell(res.NameSurname);
                    table.AddCell(res.Email);
                    table.AddCell(res.PersonCount.ToString());
                }

                document.Add(table);
                document.Close();
                return File(stream.ToArray(), "application/pdf", $"Rezervasyon_Listesi_{id}.pdf");
            }
        }

        // --- 2. EXCEL: TURLAR LİSTESİ ---
        public async Task<IActionResult> DownloadToursExcel()
        {
            var tours = await _tourService.GetAllTourAsync();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Turlar");
                worksheet.Cell(1, 1).Value = "Tur Başlığı";
                worksheet.Cell(1, 2).Value = "Fiyat";
                worksheet.Cell(1, 3).Value = "Kapasite";
                worksheet.Cell(1, 4).Value = "Durum";

                var header = worksheet.Range("A1:D1");
                header.Style.Font.Bold = true;
                header.Style.Fill.BackgroundColor = XLColor.FromHtml("#0f8b8d");
                header.Style.Font.FontColor = XLColor.White;

                int row = 2;
                foreach (var t in tours)
                {
                    worksheet.Cell(row, 1).Value = t.Title;
                    worksheet.Cell(row, 2).Value = t.Price;
                    worksheet.Cell(row, 3).Value = t.Capacity;
                    worksheet.Cell(row, 4).Value = t.Status ? "Aktif" : "Pasif";
                    row++;
                }
                worksheet.Columns().AdjustToContents();

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Vitour_Turlar.xlsx");
            }
        }

        // --- 3. PDF: KATEGORİ LİSTESİ ---
        public async Task<IActionResult> DownloadCategoriesPdf()
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            using var stream = new MemoryStream();
            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            // EKSİK OLAN TÜRKÇE FONT DESTEĞİ BURAYA DA EKLENDİ
            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            iText.Kernel.Font.PdfFont turkishFont = iText.Kernel.Font.PdfFontFactory.CreateFont(fontPath, iText.IO.Font.PdfEncodings.IDENTITY_H);
            document.SetFont(turkishFont);

            Paragraph p = new Paragraph("VİTOUR KATEGORİ RAPORU")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(18);

            document.Add(p);
            document.Add(new Paragraph("\n"));

            Table table = new Table(3).UseAllAvailableWidth();
            table.AddHeaderCell(new Cell().Add(new Paragraph("Kategori ID")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Kategori Adı")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Durum")));

            foreach (var c in categories)
            {
                table.AddCell(c.CategoryId);
                table.AddCell(c.CategoryName);
                table.AddCell(c.CategoryStatus ? "Aktif" : "Pasif");
            }

            document.Add(table);
            document.Close();
            return File(stream.ToArray(), "application/pdf", "Kategoriler.pdf");
        }

        // --- 4. EXCEL: YORUMLAR LİSTESİ ---
        public async Task<IActionResult> DownloadReviewsExcel()
        {
            var reviews = await _reviewService.GetAllReviewAsync();
            var tours = await _tourService.GetAllTourAsync();

            var tourDict = tours.ToDictionary(x => x.TourId, x => x.Title);

            using (var workbook = new ClosedXML.Excel.XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Yorumlar");

                worksheet.Cell(1, 1).Value = "İsim Soyad";
                worksheet.Cell(1, 2).Value = "Yorum";
                worksheet.Cell(1, 3).Value = "Puan";
                worksheet.Cell(1, 4).Value = "Tarih";
                worksheet.Cell(1, 5).Value = "Durum";
                worksheet.Cell(1, 6).Value = "Tur Adı";

                var headerRange = worksheet.Range("A1:F1");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;
                headerRange.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                int row = 2;
                foreach (var r in reviews)
                {
                    var tourTitle = tourDict.ContainsKey(r.TourId) ? tourDict[r.TourId] : "Bilinmiyor";

                    worksheet.Cell(row, 1).Value = r.NameSurname;
                    worksheet.Cell(row, 2).Value = r.Comment;
                    worksheet.Cell(row, 3).Value = r.Score;
                    worksheet.Cell(row, 4).Value = r.ReviewDate.ToString("dd.MM.yyyy");
                    worksheet.Cell(row, 5).Value = r.Status ? "Aktif" : "Pasif";
                    worksheet.Cell(row, 6).Value = tourTitle;

                    row++;
                }
                worksheet.Columns().AdjustToContents();

                var table = worksheet.Range(1, 1, row - 1, 6).CreateTable();
                table.Theme = ClosedXML.Excel.XLTableTheme.TableStyleMedium9;

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Yorumlar.xlsx");
                }
            }
        }

        // --- 5. EXCEL: TÜM REZERVASYONLAR ---
        public async Task<IActionResult> DownloadReservationsExcel()
        {
            var reservations = await _reservationService.GetAllReservationAsync();
            var tours = await _tourService.GetAllTourAsync();
            var tourDict = tours.ToDictionary(x => x.TourId, x => x.Title);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Rezervasyonlar");

                worksheet.Cell(1, 1).Value = "Kayıt Kodu";
                worksheet.Cell(1, 2).Value = "Ad Soyad";
                worksheet.Cell(1, 3).Value = "E-posta";
                worksheet.Cell(1, 4).Value = "Tur Adı";
                worksheet.Cell(1, 5).Value = "Kişi Sayısı";
                worksheet.Cell(1, 6).Value = "Toplam Fiyat";
                worksheet.Cell(1, 7).Value = "Tarih";

                var headerRange = worksheet.Range("A1:G1");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#0f8b8d");
                headerRange.Style.Font.FontColor = XLColor.White;

                int row = 2;
                foreach (var r in reservations)
                {
                    var tourTitle = tourDict.ContainsKey(r.TourId) ? tourDict[r.TourId] : "Bilinmiyor";

                    worksheet.Cell(row, 1).Value = r.ReservationCode;
                    worksheet.Cell(row, 2).Value = r.NameSurname;
                    worksheet.Cell(row, 3).Value = r.Email;
                    worksheet.Cell(row, 4).Value = tourTitle;
                    worksheet.Cell(row, 5).Value = r.PersonCount;
                    worksheet.Cell(row, 6).Value = r.TotalPrice;
                    worksheet.Cell(row, 7).Value = r.ReservationDate.ToString("dd.MM.yyyy");
                    row++;
                }

                worksheet.Columns().AdjustToContents();

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Tum_Rezervasyonlar.xlsx");
            }
        }

        // --- 6. PDF: TÜM REZERVASYONLAR ---
        public async Task<IActionResult> DownloadReservationsPdf()
        {
            var reservations = await _reservationService.GetAllReservationAsync();

            using var stream = new MemoryStream();
            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            iText.Kernel.Font.PdfFont turkishFont = iText.Kernel.Font.PdfFontFactory.CreateFont(fontPath, iText.IO.Font.PdfEncodings.IDENTITY_H);
            document.SetFont(turkishFont);

            Paragraph header = new Paragraph("Tüm Rezervasyonlar Listesi")
                .SetFontSize(16)
                .SetTextAlignment(TextAlignment.CENTER);
            document.Add(header);
            document.Add(new Paragraph("\n"));

            Table table = new Table(UnitValue.CreatePercentArray(new float[] { 20, 30, 20, 15, 15 })).UseAllAvailableWidth();

            table.AddHeaderCell(new Cell().Add(new Paragraph("Kod")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Ad Soyad")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("E-posta")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Kişi")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Tutar")));

            foreach (var res in reservations)
            {
                table.AddCell(res.ReservationCode ?? "-");
                table.AddCell(res.NameSurname ?? "-");
                table.AddCell(res.Email ?? "-");
                table.AddCell(res.PersonCount.ToString());
                table.AddCell(res.TotalPrice.ToString("N0") + " ₺");
            }

            document.Add(table);
            document.Close();

            return File(stream.ToArray(), "application/pdf", "Tum_Rezervasyonlar.pdf");
        }
    }
}