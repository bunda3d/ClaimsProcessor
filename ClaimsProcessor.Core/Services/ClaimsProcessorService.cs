using ClaimsProcessor.Core.Models;

namespace ClaimsProcessor.Core.Services;

public class ClaimsProcessorService
{
	public ClaimResult EvaluateClaim(Policy policy, Claim claim)
	{
		// Business Rule #1: Policy must be active on incidentDate
		if (!(claim.IncidentDate >= policy.StartDate && claim.IncidentDate <= policy.EndDate))
		{
			return new ClaimResult(false, 0m, ReasonCode.PolicyInactive);
		}

		// Business Rule #2: incidentType must be covered by policy
		if (!policy.CoveredIncidents.Contains(claim.IncidentType))
		{
			return new ClaimResult(false, 0m, ReasonCode.NotCovered);
		}

		// Business Rule #3: Payout = amountClaimed - deductible
		decimal payout = claim.AmountClaimed - policy.Deductible;

		// Business Rule #4: If payout <= 0, payout = 0 and reason code = "ZERO_PAYOUT"
		if (payout <= 0m)
		{
			return new ClaimResult(false, 0m, ReasonCode.ZeroPayout);
		}

		return new ClaimResult(true, payout, ReasonCode.Approved);
	}
}