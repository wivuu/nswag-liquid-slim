using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using nswag_liquid_slim.ViewModels.Leads;

namespace nswag_liquid_slim.ViewModels.Statistics
{
    [Flags]
    public enum LeadsSummaryType
    {
        Summary = 0b01,
        Average = 0b10
    }

    internal class LeadDateStatus
    {
        public Guid Id { get; internal set; }
        public DateTimeOffset DateCreated { get; internal set; }
        public LoanStatus Status { get; internal set; }
        public decimal RequestedAmount { get; internal set; }
    }
    
    public class LeadWithReferralInfo
    {
        public LoanStatus Status { get; internal set; }
        public decimal RequestedAmount { get; internal set; }
        public string? ReferredBy { get; internal set; }
        public string? ReferredTo { get; internal set; }
    }

    public class LeadWithLocation : IGeoJsonFeature
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        
        [JsonIgnore]
        public LoanStatus Status { get; internal set; }
        
        [JsonIgnore]
        public decimal RequestedAmount { get; internal set; }
        
        [JsonIgnore]
        public decimal Latitude { get; set; }

        [JsonIgnore]
        public decimal Longitude { get; set; }

        public string Type => "Feature";

        public IGeoJsonGeometry Geometry => 
            new GeoJsonGeometryPoint("Point", Longitude, Latitude);

        public object? Properties => new
        {
            Id, 
            Status, 
            RequestedAmount
        };
    }

    public class LeadVolumeByDate
    {
        public DateTimeOffset Date { get; internal set; }
        public decimal Volume { get; internal set; }
    }

    public class LeadSummary
    {
        public IEnumerable<LeadSummaryByDate>? Summary { get; set; }

        public LeadPipelineAverage? PipelineAverage { get; set; }
    }

    public struct LeadSummaryByDate
    {
        public DateTimeOffset Date { get; internal set; }
        public decimal Pending { get; internal set; }
        public decimal Contacted { get; internal set; }
        public decimal Funded { get; internal set; }
        public decimal Disqualified { get; internal set; }

        public LeadSummaryByDate(DateTimeOffset date, decimal pending, decimal contacted, decimal funded,
                                    decimal disqualified)
        {
            this.Date         = date;
            this.Pending      = pending;
            this.Contacted    = contacted;
            this.Funded       = funded;
            this.Disqualified = disqualified;
        }
    }

    public class LeadPipelineAverage
    {
        public double? AverageDaysPending { get; set; }
        public double? AverageDaysContacted { get; set; }
        public double? AverageDaysFunded { get; set; }
    }
}