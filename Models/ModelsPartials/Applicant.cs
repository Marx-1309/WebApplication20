//When regenerating / refreshing the model , copy this code to the newly generated file 


//Properties Only

//App_Applicant:


//public long ApplicantID { get; set; }
//public string Surname { get; set; }
//public string Firstname { get; set; }
//public string IDNumber { get; set; }
//public string ResidentialAddress { get; set; }
//public string PostalAddress { get; set; }
//public string TelephoneNumber { get; set; }
//public string FaxNumber { get; set; }
//[DataType(DataType.EmailAddress)]
//public string EmailAddress { get; set; }
//public string Occupation { get; set; }
//[DataType(DataType.Currency)]
//public decimal IncomePerMonth { get; set; }
//public string NameOfEmployer { get; set; }
//public string SpouseSurname { get; set; }
//public string SpouseFirstname { get; set; }
//public string SpouseIDNumber { get; set; }
//public string SpouseResidentialAddress { get; set; }
//public string SpousePostalAddress { get; set; }
//public string SpouseTelephoneNumber { get; set; }
//public string SpouseFaxNumber { get; set; }
//[DataType(DataType.EmailAddress)]
//public string SpouseEmailAddress { get; set; }
//public string SpouseOccupation { get; set; }
//[DataType(DataType.Currency)]
//public Nullable<decimal> SpouseIncomePerMonth { get; set; }
//public string SpouseNameOfEmployer { get; set; }
//public long PlotTypeID { get; set; }
//public long ErfSizeID { get; set; }
//[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
//public System.DateTime ApplicationDate { get; set; }
//public Nullable<bool> SignedByApplicant { get; set; }
//public Nullable<bool> SignedBySpouse { get; set; }
//public string CreatedBy { get; set; }
//[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
//public Nullable<System.DateTime> CreatedDate { get; set; }
//public string UpdatedBy { get; set; }
//[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
//public Nullable<System.DateTime> UpdatedDate { get; set; }
//public byte[] PHOTO { get; set; }
//public Nullable<bool> StageApplication { get; set; }
//public Nullable<bool> StageAllocation { get; set; }
//public Nullable<bool> StageSale { get; set; }
//public Nullable<bool> StageRegistration { get; set; }
//public Nullable<bool> StageValuation { get; set; }


//App_Erf:


//public long ErfNumberID { get; set; }
//public string ErfNumber { get; set; }
//public long ErfSize { get; set; }
//public long ErfAreaID { get; set; }
//public long PlotTypeID { get; set; }
//public long ErfTownshipID { get; set; }
//public string ErfComment { get; set; }
//public string CreatedBy { get; set; }
//[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
//public Nullable<System.DateTime> CreatedDate { get; set; }
//public string UpdatedBy { get; set; }
//[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
//public Nullable<System.DateTime> UpdatedDate { get; set; }
//[DataType(DataType.Currency)]
//public Nullable<decimal> ErfPurchasePrice { get; set; }
//[DataType(DataType.Currency)]
//public Nullable<decimal> ErfAdminFees { get; set; }
//[DataType(DataType.Currency)]
//public Nullable<decimal> ErfVat { get; set; }
//public Nullable<bool> StageAllocation { get; set; }
//public Nullable<bool> StageSale { get; set; }
//public Nullable<bool> StageRegistration { get; set; }
//public Nullable<bool> StageValuation { get; set; }
//[DataType(DataType.Currency)]
//public Nullable<decimal> ErfTotalPurchasePrice { get; set; }


//Sys_ErfArea:



//Sys_ErfCost:

//public long ErfCostID { get; set; }
//public long PlotTypeID { get; set; }
//[DataType(DataType.Currency)]
//public decimal CostPerSqm { get; set; }
//[DataType(DataType.Currency)]
//public decimal AdminFees { get; set; }

//public virtual Sys_PlotType Sys_PlotType { get; set; }



//Sys_ErfSize:


//Sys_ErfTownship:


//Sys_ErfZoning:


