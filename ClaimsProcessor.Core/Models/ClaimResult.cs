namespace ClaimsProcessor.Core.Models;

public record ClaimResult(
	bool IsApproved,
	decimal Payout,
	string ReasonCode
);