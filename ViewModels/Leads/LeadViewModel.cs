
using System;
using System.Collections.Generic;
using System.Linq;

namespace nswag_liquid_slim.ViewModels.Leads
{
    public enum LoanStatus
    {
        Matched,
        Rejected,
        Funded
    }

    public class LeadViewModel
    {
        public Guid Id { get; internal set; }
        public LoanStatus Status { get; set; }
        public DateTimeOffset DateCreated { get; internal set; }
        public DateTimeOffset StatusDate { get; internal set; }
        public string? FirstName { get; internal set; }
        public string? LastName { get; internal set; }
        public string? NameOfBusiness { get; internal set; }
        public decimal Amount { get; internal set; }
        public string? UseOfProceeds { get; internal set; }
        public string? LeadSource { get; internal set; }
        public string? ReferredTo { get; internal set; }
        public string? ReferralCode { get; internal set; }
    }
    
    public class LeadWithStatsViewModel : LeadViewModel
    {
        public decimal RequestedAmount { get; internal set; }
        public string? Industry { get; internal set; }
        public int? NumEmployees { get; internal set; }
        public decimal? NetProfit { get; internal set; }
        public bool? HasBusinessPlan { get; internal set; }
        public IEnumerable<string> CollateralTypes { get; internal set; } = Enumerable.Empty<string>();
        public StatisticsViewModel? Statistics { get; internal set; }
        public AddressViewModel? BusinessAddress { get; internal set; }
        public string? MatchedProduct_Name { get; internal set; }
        public string? LeadOwner { get; internal set; }
        public IEnumerable<string> OwnershipDemographicTypes { get; internal set; } = Enumerable.Empty<string>();
    }
    
    public class LeadDetailViewModel : LeadWithStatsViewModel
    {
        public string? Phone { get; internal set; }
        public string? Email { get; internal set; }
        public string? BusinessOwner { get; internal set; }
        public bool? HasDiverseOwnership { get; internal set; }
    }
    
    public class StatisticsViewModel
    {
        public string? CensusTract { get; internal set; }
        public bool? IsCDFIInvestmentArea { get; internal set; }
        public bool? CRAEligibleTract { get; internal set; }
        public decimal? PovertyRateAverage { get; internal set; }
        public decimal? UnemploymentRate { get; internal set; }
    }
}