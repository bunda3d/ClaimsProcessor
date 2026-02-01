namespace ClaimsProcessor.Core.Models;

public record Claim(
	string PolicyId,
	IncidentType IncidentType,
	DateOnly IncidentDate,
	decimal AmountClaimed
);