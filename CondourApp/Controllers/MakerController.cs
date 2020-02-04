using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CondourApp.Models.Edmx;
using CondourApp.Models.DB;
using System.Data.OleDb;
using System.Data;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;

namespace CondourApp.Controllers
{
    public class MakerController : Controller
    {
        // GET: Maker
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SubmitApp()
        {
            try
            {
                CommonDBOperations objCommDbOper = new CommonDBOperations();
                ViewBag.Banks = new SelectList(objCommDbOper.GetAllBanks(), "BankId", "BankName");
                ViewBag.Locations = new SelectList(objCommDbOper.GetAllLocations(), "LocationId", "Location1");
                ViewBag.Branches = new SelectList(objCommDbOper.GetAllBranches(), "BranchId", "BranchName");
                ViewBag.Products = new SelectList(objCommDbOper.GetAllProducts(), "ProductId", "Product1");
            }
           catch(Exception ex)
            {

            }

            return View();
        }
        [HttpPost]
        public ActionResult SubmitApp(Application App)
        {
            try
            {
                CommonDBOperations objCommDbOper = new CommonDBOperations();
                objCommDbOper.SaveApp(App);
            }
            catch(Exception ex)
            {
               
            }

            return View();
        }

        public ActionResult BulkUpload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BulkUpload(HttpPostedFileBase file)
        {
            try
            {
                string path = Path.GetFullPath(file.FileName);
             DataTable appColl=   ReadAsDataTable(file.InputStream);
                foreach(DataRow app in appColl.Rows)
                {
                    SaveApp(app);
                }
            }
            catch(Exception ex)
            {

            }


           return View();
        }

        private static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            string value = string.Empty;
            if (cell.CellValue!=null)
            {
                 value = cell.CellValue.InnerXml;
            }
            
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }
        public static DataTable ReadAsDataTable(Stream stream)
        {
            DataTable dataTable = new DataTable();
            using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(stream, false))
            {
                WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                string relationshipId = sheets.First().Id.Value;
                WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
                Worksheet workSheet = worksheetPart.Worksheet;
                SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                IEnumerable<Row> rows = sheetData.Descendants<Row>();

                foreach (Cell cell in rows.ElementAt(0))
                {
                    dataTable.Columns.Add(GetCellValue(spreadSheetDocument, cell));
                }

                foreach (Row row in rows)
                {
                    try
                    {
                        DataRow dataRow = dataTable.NewRow();
                        try
                        {
                            for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
                            {
                                dataRow[i] = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(i));
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                        dataTable.Rows.Add(dataRow);
                    }
                    catch(Exception ex)
                    {
                       // continue;
                    }
                }

            }
            dataTable.Rows.RemoveAt(0);

            return dataTable;
        }

       private void SaveApp(DataRow row)
        {
            try
            {
                CommonDBOperations objCommDbOper = new CommonDBOperations();
                Application app = CnvtToModel(row);


                objCommDbOper.SaveApp(app);
            }
            catch(Exception ex)
            {

            }
        }
        private Application CnvtToModel(DataRow row)
        {
            Application app = new Application();
            try
            {
                string sm = row["Sample Month\n(MMM-YY)"].ToString();
                string dob = row["DOB (DD/MMM/YY)"].ToString();
                double sampleMonth = double.Parse(sm);
                double dateOfBirth = double.Parse(dob);
                app.ApplicationNum = row["Application Reference Number"].ToString();
                app.SampleMonth= DateTime.FromOADate(sampleMonth);
                app.FileType=     row["New / Rework File"].ToString();
                app.Scheme= row["Scheme"].ToString();
                app.AssetDetails = row["Asset Details"].ToString();
                app.ApplicantsName = row["Applicant's Name"].ToString();
                app.DOB = DateTime.FromOADate(dateOfBirth);
                app.TUID = row["TU ID"].ToString();
                app.EmployeStatus = row["Employment Status Salaried / Self Employed"].ToString();

                //Bank Name
                app.BankId = Convert.ToInt32(row["Bank Name"].ToString());
                app.LocationId = Convert.ToInt32(row["Location"].ToString());
                app.BranchId= Convert.ToInt32(row["Branch Name"].ToString());
                app.RegionId= Convert.ToInt32(row["Region"].ToString());
                app.ProductId= Convert.ToInt32(row["Product"].ToString());
                app.Scheme = row["Scheme"].ToString();
                app.AssetDetails = row["Asset Details"].ToString();
                app.ApplicantResidenceAddress= row["Applicant Residence Address"].ToString();
                app.OfficeName = row["Office Name"].ToString();
                //  app.o = row["Office Address"].ToString();
                app.ContactNo= row["Contact No"].ToString();
                app.LoanAmount= Convert.ToInt32(row["Loan Amount"].ToString());
                app.DealerDsaDstName = row["Dealer/ DSA/ DST Name"].ToString();
                app.SOName= row["SO Name"].ToString();
                app.FieldExectiveName = row["Field Executive Name"].ToString();
                //app.DateOfPickup= row["Date Of Pickup (DD/MMM/YY)"].ToString();
                //app.DateOfReporting= row["Date Of Reporting (DD/MMM/YY)"].ToString();
                app.TAT = Convert.ToInt32(row["TAT (Date Pickup to Date Reporting)"].ToString());
                app.PickUpDocuments= row["Pick Up Documents"].ToString();
                app.MandatoryTrigger = row["Mandatory / Trigger"].ToString();
                app.ReferredById = Convert.ToInt32(row["Referred By"].ToString());
                app.StatusId= Convert.ToInt32(row["Status"].ToString());
                app.Remarks = row["Remarks"].ToString();
                app.SuspectReason1 = row["Suspect Reason - 1 Only if a Case is SUSPECT"].ToString();
                app.SuspectReason2 = row["Suspect Reason - 2"].ToString();
                app.KYCDetailsIdProof = row["KYC Details  - ID Proof"].ToString();
                app.KYCDetailsAddProof = row["KYC Details  - Add Proof"].ToString();
                app.OtherKYC = row["Other KYC"].ToString();
                // app.k=row["KYC Details  - Co-Applicant ID Proof / Others"].toString();
                //app.k=row["KYC Details  - Co-Applicant Add Proof"].tostring();
                app.UserBankName = row["UserBank Name"].ToString();
                app.BankAccountNumber = row["Bank Account Number"].ToString();
                app.RV = Convert.ToInt32(row["RV"].ToString());
                app.BV = Convert.ToInt32(row["BV"].ToString());
                app.PropertyAddressVisit= Convert.ToInt32(row["Property Address Visit"].ToString());
                app.ResiProof= Convert.ToInt32(row["Resi Proof"].ToString());
                app.BusinessProof= Convert.ToInt32(row["Resi Proof"].ToString());
                app.PanCard = Convert.ToInt32(row["Pan Card"].ToString());
                app.SalarySlip= Convert.ToInt32(row["Salary Slip"].ToString());
                app.DL= Convert.ToInt32(row["DL"].ToString());
                app.Passport= Convert.ToInt32(row["Passport"].ToString());
                app.RationCard= Convert.ToInt32(row["Ration Card"].ToString());
                app.VoterId= Convert.ToInt32(row["Voters Id"].ToString());
                app.AadharCard= Convert.ToInt32(row["Aadhar Card"].ToString());
                app.UtilityBill= Convert.ToInt32(row["Utility Bill"].ToString());
                app.PhoneBill= Convert.ToInt32(row["Phone Bill"].ToString());
                app.BankStatement= Convert.ToInt32(row["Bank Statement"].ToString());
                app.ITR= Convert.ToInt32(row["ITR"].ToString());
                app.RocVat= Convert.ToInt32(row["Roc/ Vat"].ToString());
                app.SalesTaxSlip= Convert.ToInt32(row["Sales Tax Registration Receipt"].ToString());
                app.F16= Convert.ToInt32(row["F-16"].ToString());
                app.Financial= Convert.ToInt32(row["Financials"].ToString());
                app.RCCopy = Convert.ToInt32(row["RC Copy"].ToString());
                app.SaleDeed= Convert.ToInt32(row["Sale Deed/ Agreements"].ToString());
                app.RentAgreement= Convert.ToInt32(row["Rent Agreement"].ToString());
                app.EncumbranceCertificate= Convert.ToInt32(row["Encumbrance Certificate"].ToString());
                app.PropertyRegistrationCheck= Convert.ToInt32(row["Property Registration Check"].ToString());
                app.StampDutyConfirmation= Convert.ToInt32(row["Stamp Duty Confirmation"].ToString());
                app.SanactionPlan= Convert.ToInt32(row["Sanction Plan"].ToString());
                app.CommencementCertificates= Convert.ToInt32(row["Commencement Certificates"].ToString());
                app.IndexLi= Convert.ToInt32(row["Index Ii"].ToString());
                app.OCR= Convert.ToInt32(row["OCR(Own Contribution Receipt)"].ToString());
                app.BuilderNoc= Convert.ToInt32(row["Builder Noc"].ToString());
                app.SocityNoc= Convert.ToInt32(row["Society Noc"].ToString());
                app.ULLC= Convert.ToInt32(row["ULCC (Urban Land Ceiling)"].ToString());
                app.LayoutCopy= Convert.ToInt32(row["Layout Copy"].ToString());
                app.AccountActitvity= Convert.ToInt32(row["Count Of Activity"].ToString());

            }
            catch (Exception ex)
            {

            }
            return app;
        }
    }
}