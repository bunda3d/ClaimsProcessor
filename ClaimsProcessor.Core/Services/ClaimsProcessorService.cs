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

		// Happy path: approve all claims that pass the first rule
		return new ClaimResult(true, 10000m, ReasonCode.Approved);
	}
}