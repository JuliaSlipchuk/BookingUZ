using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Booking.ViewModels;
using Booking.ViewModelsAndSubViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;
using Spire.Email;
using Spire.Email.IMap;
using Spire.Email.Smtp;


namespace Booking.PdfAndEmail
{
    public static class WorkWithPDFAndEmail
    {
        public static List<string> CreatePdfFileAndSendEmail(List<TicketVM> tickets, InformAboutPassengers personsInf)
        {
            List<string> filesPaths = new List<string>();
            for (int i = 0; i < tickets.Count(); i++)
            {
                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    Document document = new Document(PageSize.A4, 10, 10, 70, 10);

                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();

                    string text = "Landing document";
                    Paragraph paragraph = new Paragraph();
                    paragraph.SpacingBefore = 10;
                    paragraph.SpacingAfter = 10;
                    paragraph.Alignment = Element.ALIGN_CENTER;
                    paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 18f, BaseColor.BLACK);
                    paragraph.Add(text);
                    document.Add(paragraph);

                    PdfPTable table = new PdfPTable(4);

                    PdfPCell cell = new PdfPCell(new Phrase());

                    table.AddCell("First name and last name");
                    table.AddCell(tickets[i].FirstName + " " + tickets[i].LastName);
                    table.AddCell("Train number");
                    table.AddCell(Convert.ToString(tickets[i].TrainNumber));

                    table.AddCell("Departure station");
                    table.AddCell(tickets[i].StartStation);
                    table.AddCell("Carriage order");
                    table.AddCell(tickets[i].CarriageType + " " + tickets[i].CarriageOrder);

                    table.AddCell("Arrival station");
                    table.AddCell(tickets[i].EndStation);
                    table.AddCell("Seat number");
                    table.AddCell(tickets[i].SeatNumber + " " + tickets[i].PersonType);

                    table.AddCell("Departure date time");
                    table.AddCell(tickets[i].DepartureDateTime.ToString("dd.MM.yyyy HH:mm"));
                    cell = new PdfPCell(new Phrase("Service"));
                    cell.Rowspan = 2;
                    table.AddCell(cell);
                    if (tickets[i].HaveTea)
                    {
                        cell = new PdfPCell(new Phrase("One tea"));
                    }
                    else
                    {
                        cell = new PdfPCell(new Phrase("No tea"));
                    }
                    table.AddCell(cell);
                    table.AddCell("Arrival date time");
                    table.AddCell(tickets[i].ArrivalDateTime.ToString("dd.MM.yyyy HH:mm"));
                    if (tickets[i].HaveBed)
                    {
                        cell = new PdfPCell(new Phrase("Bed"));
                    }
                    else
                    {
                        cell = new PdfPCell(new Phrase("No bed"));
                    }
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Price"));
                    cell.Colspan = 2;
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(Convert.ToString(tickets[i].Price)));
                    cell.Colspan = 2;
                    table.AddCell(cell);

                    document.Add(table);

                    document.Close();

                    byte[] bytes = memoryStream.ToArray();
                    string fileName = tickets[i].FirstName + "_" + tickets[i].LastName + "_" + tickets[i].StartStation + "_" + tickets[i].EndStation + "_" + personsInf.BirthDate[i].ToString("dd-MM-yyyy_HH.mm.ss") + "_" + tickets[i].DepartureDateTime.ToString("dd-MM-yyyy_HH.mm.ss") + ".pdf";
                    File.WriteAllBytes(@"D:\Virtuace\HW6\Booking\Booking\Tickets\" + fileName, bytes);
                    filesPaths.Add(@"D:\Virtuace\HW6\Booking\Booking\Tickets\" + fileName);

                    TransferTicketsEmail(fileName, personsInf.Email[i], tickets[i].DepartureDateTime, tickets[i].StartStation, tickets[i].EndStation);
                }
            }

            return filesPaths;
        }

        public static void TransferTicketsEmail(string fileName, string email, DateTime departDateTime, string startStat, string endStat)
        {
            MailAddress from = "julia2015olex@gmail.com";
            MailAddress to = email;

            MailMessage message = new MailMessage(from, to);

            message.Subject = "Ticket";
            message.BodyText = "It is your ticket for departure date time: " + departDateTime.ToString("dd.MM.yyyy HH:mm") + " from " + startStat + " to " + endStat;
            message.Date = DateTime.Now;
            message.Attachments.Add(new Attachment(@"D:\Virtuace\HW6\Booking\Booking\Tickets\" + fileName));

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.ConnectionProtocols = ConnectionProtocols.Ssl;
            smtp.Username = from.Address;
            smtp.Password = "grandrvua";
            smtp.Port = 587;

            smtp.SendOne(message);
        }

        public static string MergePDFFiles(List<string> filesPaths)
        {
            Spire.Pdf.PdfDocument[] tickets = new Spire.Pdf.PdfDocument[filesPaths.Count()];
            for (int i = 0; i < tickets.Length; i++)
            {
                tickets[i] = new Spire.Pdf.PdfDocument(filesPaths[i]);
            }

            Spire.Pdf.PdfDocument document = new Spire.Pdf.PdfDocument();
            for(int i = 0; i < tickets.Length; i++)
            {
                document.InsertPage(tickets[i], 0);
            }
            string now = DateTime.Now.ToString("yyyy/MM/dd_HH.mm.ss.fff");
            document.SaveToFile(@"D:\Virtuace\HW6\Booking\Booking\Tickets\" + now + ".pdf");

            return @"D:\Virtuace\HW6\Booking\Booking\Tickets\" + now + ".pdf";
        }
    }
}