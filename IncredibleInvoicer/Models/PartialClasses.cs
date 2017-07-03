using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc; for dynamic roles
//using Microsoft.AspNet.Identity;for dynamic roles
//using IncredibleInvoicer.Models;for dynamic roles
//using Microsoft.AspNet.Identity.EntityFramework;for dynamic roles

namespace IncredibleInvoicer
{
    [MetadataType(typeof(CustomerMetadata))]
    public partial class Customer
    {
    }

    [MetadataType(typeof(InvoiceMetadata))]
    public partial class Invoices
    {
    }

    [MetadataType(typeof(BanksMetadata))]
    public partial class Banks
    {
    }

    [MetadataType(typeof(InvoiceDetailMetadata))]
    public partial class InvoiceDetail
    {
    }

    [MetadataType(typeof(StateMetadata))]
    public partial class State
    {
    }

    [MetadataType(typeof(TaxMetadata))]
    public partial class Taxes
    {
    }

    [MetadataType(typeof(UnitMetadata))]
    public partial class Unit
    {
    }


    ///// <summary>
    ///// Used for inputing Invoice Details
    ///// </summary>
    //public class NewEmp
    
    public class InvoiceDetailStr
    {
        public int InvoiceDetailID { get; set; }
        public Nullable<int> InvoiceID { get; set; }

        public string Item { get; set; }  //we are making this class just so that this can be string for autocomplete

        [Display(Name = "Quantity")]
        [Required]
        [Range(0.0, Double.MaxValue)]
        public decimal Qty { get; set; }

        [Display(Name = "Units")]
        [Required]
        public int UnitID { get; set; }

        [Display(Name = "Rate")]
        [Required]
        [Range(0.0, Double.MaxValue)]
        public decimal Rate { get; set; }

        [Display(Name = "Discount")]        
        [Range(0.0, Double.MaxValue)]
        public Nullable<decimal> Discount { get; set; }

        [Display(Name = "Tax %")]
        [Required]
        public int TaxID { get; set; }

        [Display(Name = "Misc. data")]
        public string Disc { get; set; }

        [Display(Name = "HSN Code")]
        public string HSNCode { get; set; }
    }

    ///// <summary>
    ///// Used for inputing Invoice Details
    ///// </summary>
    //public class NewEmp

    public class SummaryRptData
    {
        public int yr { get; set; }
        public int mont { get; set; }
        public int cont { get; set; }
        public decimal net { get; set; }
        public decimal ctax { get; set; }
    }


}