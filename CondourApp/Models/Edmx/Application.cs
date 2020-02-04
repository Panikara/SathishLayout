//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CondourApp.Models.Edmx
{
    using System;
    using System.Collections.Generic;
    
    public partial class Application
    {
        public int ApplicationId { get; set; }
        public string ApplicationNum { get; set; }
        public Nullable<int> BankId { get; set; }
        public System.DateTime SampleMonth { get; set; }
        public string FileType { get; set; }
        public Nullable<int> LocationId { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<int> RegionId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string Scheme { get; set; }
        public string AssetDetails { get; set; }
        public string ApplicantsName { get; set; }
        public Nullable<int> ReferredById { get; set; }
        public System.DateTime DOB { get; set; }
        public string TUID { get; set; }
        public string EmployeStatus { get; set; }
        public string ApplicantResidenceAddress { get; set; }
        public string OfficeName { get; set; }
        public string ContactNo { get; set; }
        public Nullable<long> LoanAmount { get; set; }
        public string DealerDsaDstName { get; set; }
        public string SOName { get; set; }
        public string FieldExectiveName { get; set; }
        public Nullable<System.DateTime> DateOfPickup { get; set; }
        public Nullable<System.DateTime> DateOfReporting { get; set; }
        public Nullable<int> TAT { get; set; }
        public string PickUpDocuments { get; set; }
        public string MandatoryTrigger { get; set; }
        public Nullable<int> StatusId { get; set; }
        public string Remarks { get; set; }
        public string SuspectReason1 { get; set; }
        public string SuspectReason2 { get; set; }
        public string KYCDetailsIdProof { get; set; }
        public string KYCDetailsAddProof { get; set; }
        public string OtherKYC { get; set; }
        public string UserBankName { get; set; }
        public string BankAccountNumber { get; set; }
        public Nullable<int> RV { get; set; }
        public Nullable<int> BV { get; set; }
        public Nullable<int> PropertyAddressVisit { get; set; }
        public Nullable<int> ResiProof { get; set; }
        public Nullable<int> BusinessProof { get; set; }
        public Nullable<int> PanCard { get; set; }
        public Nullable<int> SalarySlip { get; set; }
        public Nullable<int> DL { get; set; }
        public Nullable<int> Passport { get; set; }
        public Nullable<int> RationCard { get; set; }
        public Nullable<int> VoterId { get; set; }
        public Nullable<int> AadharCard { get; set; }
        public Nullable<int> UtilityBill { get; set; }
        public Nullable<int> PhoneBill { get; set; }
        public Nullable<int> BankStatement { get; set; }
        public Nullable<int> ITR { get; set; }
        public Nullable<int> RocVat { get; set; }
        public Nullable<int> SalesTaxSlip { get; set; }
        public Nullable<int> F16 { get; set; }
        public Nullable<int> Financial { get; set; }
        public Nullable<int> RCCopy { get; set; }
        public Nullable<int> SaleDeed { get; set; }
        public Nullable<int> RentAgreement { get; set; }
        public Nullable<int> EncumbranceCertificate { get; set; }
        public Nullable<int> PropertyRegistrationCheck { get; set; }
        public Nullable<int> StampDutyConfirmation { get; set; }
        public Nullable<int> SanactionPlan { get; set; }
        public Nullable<int> CommencementCertificates { get; set; }
        public Nullable<int> IndexLi { get; set; }
        public Nullable<int> OCR { get; set; }
        public Nullable<int> BuilderNoc { get; set; }
        public Nullable<int> SocityNoc { get; set; }
        public Nullable<int> ULLC { get; set; }
        public Nullable<int> LayoutCopy { get; set; }
        public Nullable<int> AccountActitvity { get; set; }
        public string ReportSent { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    
        public virtual Bank Bank { get; set; }
        public virtual Product Product { get; set; }
        public virtual Location Location { get; set; }
        public virtual Product Product1 { get; set; }
        public virtual ReferredBy ReferredBy { get; set; }
        public virtual Region Region { get; set; }
        public virtual Status Status { get; set; }
        public virtual Branch Branch { get; set; }
    }
}
