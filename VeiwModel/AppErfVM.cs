using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication20.VeiwModel
{
    public class AppErfVM { 
    public long ErfNumberID { get; set; }
    [Required]
    public string ErfNumber { get; set; }
    public long ErfSize { get; set; }
    public long ErfAreaID { get; set; }
    public long PlotTypeID { get; set; }
    public long ErfTownshipID { get; set; }
    public string ErfComment { get; set; }
    public string CreatedBy { get; set; }
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public Nullable<System.DateTime> CreatedDate { get; set; }
    public string UpdatedBy { get; set; }
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public Nullable<System.DateTime> UpdatedDate { get; set; }
    [DataType(DataType.Currency)]
    public Nullable<decimal> ErfPurchasePrice { get; set; }
    [DataType(DataType.Currency)]
    public Nullable<decimal> ErfAdminFees { get; set; }
    [DataType(DataType.Currency)]
    public Nullable<decimal> ErfVat { get; set; }
    public Nullable<bool> StageAllocation { get; set; }
    public Nullable<bool> StageSale { get; set; }
    public Nullable<bool> StageRegistration { get; set; }
    public Nullable<bool> StageValuation { get; set; }
    [DataType(DataType.Currency)]
    public Nullable<decimal> ErfTotalPurchasePrice { get; set; }
}
}