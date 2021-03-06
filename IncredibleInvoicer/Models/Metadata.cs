﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IncredibleInvoicer
{
    public class BanksMetadata
    {
        [Display(Name = "Bank Name")]
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string BankName;

        [Display(Name = "Bank A/C")]
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string ACno;

        [Display(Name = "Bank IFSC")]
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string IFSC;
    }

    public class CustomerMetadata
    {
        [Display(Name = "Company/Hotel Name")]
        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Name;


        [Display(Name = "Address Line 1")]
        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Address1;

        [Display(Name = "Address Line 2")]        
        [StringLength(150, MinimumLength = 3)]
        public string Address2;

        [Display(Name = "GSTIN")]        
        [StringLength(50, MinimumLength = 3)]
        public string TaxNo;

        [Display(Name = "Contact Person")]
        public string Contact;

        [Display(Name = "Contact Number")]
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string ContactNo;
    }

    public class InvoiceDetailMetadata
    {
        [Display(Name = "Quantity")]
        [Required]
        [Range(0.0, Double.MaxValue)]
        public decimal Qty;

        [Display(Name = "Units")]
        [Required]        
        public int UnitID;

        [Display(Name = "Rate")]
        [Required]
        [Range(0.0, Double.MaxValue)]
        public decimal Rate;
    }

    public class InvoiceMetadata
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Invoice Date")]
        [Required]
        public DateTime InvDate;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "PO Date")]
        public DateTime POdate;

        [Display(Name = "PO Number")]        
        [StringLength(50, MinimumLength = 3)]
        public string POno;
                  
    }

    public  class StateMetadata
    {
        [Display(Name = "State")]
        [Required]
        [StringLength(50)]
        public string State1;
    }

    public class TaxMetadata
    {
        [Display(Name = "Tax %")]
        [Required]
        public int TaxID;

        [Display(Name = "Tax Percentage")]
        [Required]
        [Range(0.0, Double.MaxValue)]
        public decimal Tax;
    }

    public class UnitMetadata
    {
        [Display(Name = "Units")]
        [Required]
        [StringLength(50)]
        public string Unit1;
    }
    

}