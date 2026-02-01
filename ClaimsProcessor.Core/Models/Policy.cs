namespace ClaimsProcessor.Core.Models;

public record Policy(
	string PolicyId,
	DateOnly StartDate,
	DateOnly EndDate,
	decimal Deductible,
	decimal CoverageLimit,
	List<IncidentType> CoveredIncidents
);