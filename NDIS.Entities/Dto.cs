

using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NDIS.Entities
{
    public class GetAdminProviderListResponse : Provider
    {
        public string? Status { get { return Settings.SetStatus(Convert.ToInt32(this.IsActive)); } }

        public int? TotalRecords { get; set; }
        public string? CreatedDateStr { get { return Settings.SetDateFormat(Convert.ToDateTime(this.CreatedDate)); } }
    }
    public class GetAdminProviderListParams : paggingEntity
    {

    }
    public class GetAdminCompanyListParams : paggingEntity
    {
        public int? CompanyId { get; set; }
    }
    public class AddCompanyParams:CompanyEntity{}
    public class AddCompanyResponse{
        public int? CompanyId { get; set; }



    }

    public class AddCategoryPriceParam
    {
        public int? SupportCategoryId { get; set; }
        public int? ClientBudgetId { get; set; }
        public decimal?  CategoryPrice { get; set; }
}
    public class AddProviderResponse
    {
        public int? ProviderId { get; set; }



    }
    public class AdminAddProvider : Provider
    {
    }
    public class AdminAddCompany : CompanyEntity
    {
        public List<CompanyTypes> CompanyTypes { get; set; }
    }
    public class CompanyTypes
    {
        public int? CompanyTypeId { get; set; }
        public string? CompanyType { get; set; }

    }
    public class GetAdminCompanyListResponse : CompanyEntity
    {
        public int? TotalRecords { get; set; }

        public string? CompanyType { get; set; }

        public string? Status { get { return Settings.SetStatus(Convert.ToInt32(this.IsActive)); } }


        public string? CreatedDateStr { get { return Settings.SetDateFormat(Convert.ToDateTime(this.CreatedDate)); } }

    }
    public class GetAdminClientsListParams : paggingEntity
    {
        public int? CompanyId { get; set; }
        public int? Status { get; set; }
    }

    public class GetAdminClientsListResponse
    {
        public int ClientId { get; set; }

        public int? CompanyId { get; set; }

        public string FirstName { get; set; } = null!;

        public string? LastName { get; set; }
        public string? CompanyName { get; set; }

        public string? DateOfBirth { get; set; }

        public int? GenderId { get; set; }
        public int? TotalRecords { get; set; }

        public string? MobileNumber { get; set; }

        public string? EmailId { get; set; }

       
        public string? ClientAddress { get; set; }

        public int IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedDateStr { get { return Settings.SetDateFormat(this.CreatedDate); } }
        public string? Status { get { return Settings.SetStatus(this.IsActive); } }
        public string? Name { get { return this.FirstName + " " + this.LastName; } }
    }

    public class GetAdminAddClient
    {
        public int ClientId { get; set; }
        [BindRequired()]
        public int? CompanyId { get; set; }
        [Required(ErrorMessage = "Please enter Firstname")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Please enter Lastname")]
        public string? LastName { get; set; }

        //[DataType(DataType.Date, ErrorMessage = "Date is required")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string? DateOfBirth { get; set; }

        public int? GenderId { get; set; }
        [Required(ErrorMessage = "Please enter Mobile")]
        public string? MobileNumber { get; set; }

        public string? EmailId { get; set; }

        public string? ClientAddress { get; set; }
        public List<AusState> States { get; set; }

        public int IsActive { get; set; }
        [Required(ErrorMessage = "Please Select State")]
        public int StateId { get; set; }

        public string? SupportCoordinator { get; set; }
        public string? SCCompany { get; set; }
        public string? SCPhone { get; set; }
        public string? SCEmail { get; set; }
        public string? PlanManager { get; set; }
        public string? PMPhone { get; set; }
        public string? PMEmail { get; set; }

    }

    public class GetAdminClientBudgetListParams : paggingEntity
    {
        public string? CompanyId { get; set; }
    }
    public class GetAdminClientBudgetListResponse
    {
        public int ClientBudgetId { get; set; }

        public string? CompanyId { get; set; }
        public string? CompanyName { get; set; }

        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? ProviderName { get; set; }
        public string? NDISRefno { get; set; }

        public string? Name { get { return this.FirstName + " " + this.LastName; } }
        public int? ClientId { get; set; }
        public string? Client { get; set; }
        public int? TotalRecords { get; set; }

        public decimal? ProposedBudget { get; set; }

        public DateTime? StartDate { get; set; }
        public string? StartDateStr { get { return Settings.SetDateFormat(this.StartDate); } }
        public string? EndDateStr { get { return Settings.SetDateFormat(this.EndDate); } }

        public DateTime? EndDate { get; set; }

        public int? ProviderId { get; set; }
        public string? Provider { get; set; }
        public int? IsActive { get; set; }
        public string? Status { get { return Settings.SetStatus(Convert.ToInt32(this.IsActive)); } }
        public string? Info { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string? CreatedDateStr { get { return Settings.SetDateFormat(this.CreatedDate); } }

    }

    public class AddUpdateAdminClientBudget
    {
        [Required(ErrorMessage = "Please enter LastName")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Please enter Firstname")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Please enter Mobile")]
        public string? MobileNumber { get; set; }
        public int ClientBudgetId { get; set; }
        public string? DateOfBirth { get; set; }
        public string? EmailId { get; set; }

        public string? ClientAddress { get; set; }
        public string? CompanyId { get; set; }
        [BindRequired()]
        public int? ClientId { get; set; }
        [Required(ErrorMessage = "Please enter ProposedBudget")]
        public decimal? ProposedBudget { get; set; }
        [Required(ErrorMessage = "Please enter StartDate")]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "Please enter EndDate")]

        public string? NDISRefno { get; set; }
        [Required(ErrorMessage = "Please enter EndDate")]
        public DateTime? EndDate { get; set; }
        [BindRequired()]
        public int? ProviderId { get; set; }
        public int IsActive { get; set; }
        
        public string? Info { get; set; }

        public DateTime? CreatedDate { get; set; }
        //public List<CompanyEntity> Companies { get; set; }
        public List<Client> Clients { get; set; }
        public List<Provider> Providers { get; set; }


        public string? SupportCoordinator { get; set; }
        public string? SCCompany { get; set; }
        public string? SCPhone { get; set; }
        public string? SCEmail { get; set; }
        public string? PlanManager { get; set; }
        public string? PMPhone { get; set; }
        public string? PMEmail { get; set; }
        public List<SupportCategory> SupportCategories { get; set; }
        public List<AusState> States { get; set; }
        public int StateId { get; set; }
    }

    public class GetSupportItemPriceParam
    {
        public int StateId { get; set; }
        public int SupportItemId { get; set; }

    }

    public class GetSupportItemPriceResponse
    {
        public decimal Price { get; set; }
        public int SupportItemId { get; set; }
        public int StateId { get; set; }
        public int SupportIemPriceListId { get; set; }
        public string OtherInfo { get; set; }

    }

    public class AdminSupportCategoryResponse
    {
        public string? SupportCategoryName { get; set; }
        public int? SupportCategoryId { get; set; }
        public decimal? SupportCategoryBudget { get; set; }
        public string? ItemPriceStr { get { return Settings.SetPriceFormat(this.SupportCategoryBudget.ToString()); } }

        public DateTime? CreatedDate { get; set; }
        public string? CreatedDateStr { get { return Settings.SetDateFormat(this.CreatedDate); } }
    }

    public class GetAdminClientSupportItemsList: ClientSupportItem
    {
        //public int? TotalRecords { get; set; }
        public int? TotalRecords { get; set; }

        public string? DateStr { get { return Settings.SetDateFormat(Convert.ToDateTime(this.Date)); } }
        public DateTime? Date { get; set; }
        public string? DayofWeek { get; set; }
        public string? SuppItemName { get; set; }
        public string? CustomName { get; set; }
        public string? SupportItemNumber { get; set; }
        public string? ShiftStartTime { get; set; }
        public string? ShiftEndTime { get; set; }
        public string? SupportCategoryName { get; set; }
        public int? SupportCategoryId { get; set; }
        public string? NameOfService { get; set; }
        public string? WorkerName { get; set; }
        public int? SupportItemId { get; set; }
        public decimal? HoursCount { get; set; }

        public decimal? ItemPrice { get; set; }
        public decimal? SupportCategoryBudget { get; set; }
        public string? SupportCategoryBudgetStr { get { return Settings.SetPriceFormat(this.SupportCategoryBudget.ToString()); } }
        public string? ItemPriceStr { get { return Settings.SetPriceFormat(this.ItemPrice.ToString()); } }

        public decimal? LineAmount { get; set; }
        public string? LineAmountStr { get { return Settings.SetPriceFormat(this.LineAmount.ToString()); } }

        
        public string? SupportItemName { get; set; }
        public string? CreatedDateStr { get { return Settings.SetDateFormat(this.CreatedDate); } }

        public string? Status { get { return Settings.SetStatus(Convert.ToInt32(this.IsActive)); } }
        public string? itemBudgetStr{ get { return Settings.SetPriceFormat(this.ItemBudget.ToString()); } }
        
        public decimal? TotalItemAmount { get; set; }
        public string? TotalItemAmounteStr { get { return Settings.SetPriceFormat(this.TotalItemAmount.ToString()); } }
        public string? PricePerHourStr { get { return Settings.SetPriceFormat(this.PricePerHour.ToString()); } }

    }

    public class AddEditAdminClientSupportItems : SupportItem
    {
        public int? TotalRecords { get; set; }
        public decimal? StatePrice { get; set; }
        public string? StatePriceStr { get { return Settings.SetPriceFormat(this.StatePrice.ToString()); } }


        public Int32 ClientBudgetId { get; set; } 
        
    }

    public class AdminClientSupportItemsParams:paggingEntity {
        public int? ClientBudgetId { get; set; }
        public int? SupportCategoryId { get; set; }
    }

    public class AdminClientSupportCategoryParams : paggingEntity
    {
        public int? ClientBudgetId { get; set; }
    }

    public class AddClientSupportItemParam
    {
        public int? ClientBudgetId { get; set; }
        public int? CompanyId { get; set; }
        public int? ClientSupportItemId { get; set; }
        public int? SupportItemId { get; set; }
        public decimal? ItemBudget { get; set; }
        public decimal? SupprotCategorybudget { get; set; }
        public decimal? PricePerHour { get; set; }
        public int? SupportCategoryId { get; set; }

    }

    public class UpdateClientSupportItemParam
    {
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int? FrequencyId { get; set; }
        public int? ClientSupportItemId { get; set; }
        public string? WeekArray { get; set; }
    }
    public class AddClientSupportItem
    {
        public int? ClientBudgetId { get; set; }
        public int? CompanyId { get; set; }
        public int? StateId { get; set; }
        public string? StateName { get; set; }
        public int? ClientSupportItemId { get; set; }
        public int? SupportItemId { get; set; }
        public int? SupportCategoryId { get; set; }
        public decimal? ProposedBudget { get; set; }
        public decimal? UsedBudget { get; set; }
        public decimal? RemainingBudget { get; set; }
        public string? ProposedBudgetStr { get { return Settings.SetPriceFormat(this.ProposedBudget.ToString()); } }
        public string? UsedBudgetStr { get { return Settings.SetPriceFormat(this.UsedBudget.ToString()); } }
        public string? RemainingBudgetStr { get { return Settings.SetPriceFormat(this.RemainingBudget.ToString()); } }

        public DateTime? CreatedDate { get; set; }
        public DateTime? ItemBudget { get; set; }
        public List<SupportCategory> SupportCategories { get; set; }
    }

    public class GetEditClientSupportItem : ClientSupportItem
    {
        public string SupportItemName { get; set; }
        public int? DayMandatoryId { get; set; }
        public List<Provider> Providers { get; set; }
        public List<ScheduledFrequencies> ScheduledFrequencies { get; set; }
        public string[] WeekArray { get; set; }
    }

    public class EditClientSupportItemParam: ClientSupportItem
    {
    }

    public class GetClientSupportItemScheduleResponse 
    {
        public int ClientBudgetId { get; set; }
        public int? SupportItemId { get; set; }
        public decimal SurPlus { get; set; }
        public string? SurPlusStr { get { return Settings.SetPriceFormat(this.SurPlus.ToString()); } }

        public decimal Funding { get; set; }
        public string? FundingStr { get { return Settings.SetPriceFormat(this.Funding.ToString()); } }

        public decimal SumOfSurvices { get; set; }
        public string? SumOfSurvicesStr { get { return Settings.SetPriceFormat(this.SumOfSurvices.ToString()); } }

        public DateTime? FirstVisitDate { get; set; }
        public string? FirstVisitDateStr { get { return Settings.SetDateFormat(this.FirstVisitDate); } }
        public int? ClientSupportItemId { get; set; }
        public int? TotalVisits { get; set; }
        public DateTime? LastVisitDate { get; set; }
        public string? LastVisitDateStr { get { return Settings.SetDateFormat(this.LastVisitDate); } }

    }

    public class ClientSupportItemScheduleParam : paggingEntity
    {
        public int? SupportItemId { get; set; }

    }

    public class ClientSupportItemScheduleList
    {
        public int? TotalRecords { get; set; }

        public string? DateStr { get { return Settings.SetDateFormat(Convert.ToDateTime( this.Date)); } }
        public DateTime? Date { get; set; }
        public string ? DayofWeek { get; set; }
        public string ? SuppItemName { get; set; }
        public string? ShiftStartTime { get; set; }
        public string? ShiftEndTime { get; set; }
        public string? NameOfService { get; set; }
        public string? WorkerName { get; set; }
        public int ? SupportItemId { get; set; }
        public decimal? HoursCount { get; set; }
       
        public decimal? ItemPrice { get; set; }
        public string? ItemPriceStr { get { return Settings.SetPriceFormat(this.ItemPrice.ToString()); } }

        public decimal? LineAmount { get; set; }
        public string? LineAmountStr { get { return Settings.SetPriceFormat(this.LineAmount.ToString()); } }

        

    }

    public class AusState
    {
        public int? StateId { get; set; }
        
        public string? StateName { get; set; }


    }

    public class SupportCategory
    {
        public int? SupportCategoryId { get; set; }
        [Required(ErrorMessage = "Please enter Support Category Name")]
        public string? SupportCategoryName { get; set; }

        public int? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? TotalRecords { get; set; }

        public string? CreatedDateStr { get { return Settings.SetDateFormat(this.CreatedDate); } }

        public string? Status { get { return Settings.SetStatus(Convert.ToInt32(this.IsActive)); } }
    }

    public class GetSupportCategoryResponse: SupportCategory
    {

    }

    public class GetSupportCategoryParams: paggingEntity
    {

    }

    public class AddSupportCategoryResponse 
    {
        public int? SupportCategoryId { get; set; }

    }


    public class GetSupportItemResponse : SupportItem
    {
        public int? TotalRecords { get; set; }
        public string? SupportCategoryName { get; set; }

        public string? CreatedDateStr { get { return Settings.SetDateFormat(this.CreatedDate); } }

        public string? IsActive { get { return Settings.SetStatus(Convert.ToInt32(this.Status)); } }
    }

    public class GetSupportItemParams : paggingEntity
    {

    }

   

    public class ConfirmTempScheduleParam
    {
        public int ClientBudgetId { get; set; }
        public int SupportItemId { get; set; }
    }

    public class SupportItemPriceList
    {
        [Required(ErrorMessage = "Please select Support Item")]
        public int SupportItemId { get; set; }
        [Required(ErrorMessage = "Please select State")]
        public int StateId { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        [Required(ErrorMessage = "Please enter price")]
        public decimal Price { get; set; }
        public int SupportItemPriceListId { get; set; }
        public int? SupportCategoryId { get; set; }
        public string SupportItemName { get; set; }
    }

    public class GetSupportItemPriceListResponse : SupportItemPriceList
    {
        public int? TotalRecords { get; set; }
        public string? SupportItemName { get; set; }
        public string? StateName { get; set; }

        public string? CreatedDateStr { get { return Settings.SetDateFormat(this.CreatedDate); } }

        public string? IsActive { get { return Settings.SetStatus(Convert.ToInt32(this.Status)); } }
    }

    public class GetSupportItemPriceListParams : paggingEntity
    {

    }

    public class AddSupportItemPriceListResponse
    {
        public int? SupportItemPriceListId { get; set; }

    }

    public class AddSupportItemPriceListParams : SupportItemPriceList
    {
        public List<SupportCategory> SupportCategories { get; set; }
        public List<AusState> States { get; set; }
        public int? SupportCategoryId { get; set; }
        public string SupportItemName { get; set; }


    }

    public class DashboardData
    {
        public int? TotalActiveClients { get; set; }
        public int? TotalInactiveClients { get; set; }
        public decimal? CompanyAlloctedBudget { get; set; }
        public decimal? CompanyEstimatedBudget { get; set; }

        public string? CompanyAlloctedBudgetStr { get { return Settings.SetPriceFormat(this.CompanyAlloctedBudget.ToString()); } }
        public string? CompanyEstimatedBudgetStr { get { return Settings.SetPriceFormat(this.CompanyEstimatedBudget.ToString()); } }

    }

    public class BaseResponse
    {
        public int Result { get; set; }

    }
    public class ScheduledFrequencies
    {
        public string ScheduledFrequencyId { get; set; }
        public string ScheduledFrequency { get; set; }
        public int IsMultiWeekday { get; set; }
    }

    public class GetBudgetSchedule {
        public int? ClientId { get; set; }
        public int? ClientBudgetId { get; set; }

        public List<Client> Clients { get; set; }
        public List<ClientBudget> ClientBudget { get; set; }

        public List<ScheduledFrequencies> scheduledFrequencies { get; set; }
    }

}
