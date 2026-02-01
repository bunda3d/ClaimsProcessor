using ClaimsProcessor.Core.Models;
using ClaimsProcessor.Core.Services;

Console.WriteLine("==========================================");
Console.WriteLine("   INSURANCE CLAIMS PROCESSING SYSTEM");
Console.WriteLine("==========================================");

// 1. Setup Policy
var policy = new Policy(
		"POL-05161980",
		new DateOnly(2023, 1, 1),
		new DateOnly(2024, 1, 1),
		Deductible: 500m,
		CoverageLimit: 10000m,
		CoveredIncidents: [IncidentType.Accident, IncidentType.Fire]
);

PrintPolicyDetails(policy);

// 2. Define Scenarios (Claims)
var scenarios = new List<(string Title, Claim Claim)>
{
		("1: \n--- Happy Path (Valid Accident)",
		 new Claim(policy.PolicyId, IncidentType.Accident, new DateOnly(2023, 6, 15), 5000m)),

		("2: \n--- Not Covered (Water Damage)",
		 new Claim(policy.PolicyId, IncidentType.WaterDamage, new DateOnly(2023, 7, 20), 8000m)),

		("3: \n--- Date Expired (Outside Policy)",
		 new Claim(policy.PolicyId, IncidentType.Fire, new DateOnly(2024, 2, 1), 2000m)),

		("4: \n--- Payout Capped (Claim Exceeds Limit)",
		 new Claim(policy.PolicyId, IncidentType.Accident, new DateOnly(2023, 10, 31), 20000m))
};

// 3. Process Scenarios
var processor = new ClaimsProcessorService();

foreach (var scenario in scenarios)
{
	Console.WriteLine($"\n\n------------------------------------------ \n--- Claim Scenario #{scenario.Title} \n--- --- --- --- --- --- --- --- --- --- -- ");
	Console.WriteLine($"Incident: {scenario.Claim.IncidentType} on {scenario.Claim.IncidentDate}");
	Console.WriteLine($"Claimed:  {scenario.Claim.AmountClaimed:C0}");

	var result = processor.EvaluateClaim(policy, scenario.Claim);

	Console.ForegroundColor = result.IsApproved ? ConsoleColor.Green : ConsoleColor.Red;
	Console.WriteLine($"Result:   {result.ReasonCode}");
	Console.ResetColor();

	//if (result.IsApproved)
	//{
	Console.WriteLine($"Payout:   {result.Payout:C2}");
	//}
	Console.WriteLine("__________________________________________");
}

Console.WriteLine("\n\n==========================================\n");
Console.WriteLine("Press any key to exit...\n");
Console.ReadKey();

// Helper Method to Print Policy Details
void PrintPolicyDetails(Policy p)
{
	Console.WriteLine($"\n------------------------------------------ \n--- POLICY DETAILS");
	Console.WriteLine("--- --- --- --- --- --- --- --- --- --- -- ");
	Console.WriteLine($"ID:         {p.PolicyId}");
	Console.WriteLine($"Active:     {p.StartDate} to {p.EndDate}");
	Console.WriteLine($"Deductible: {p.Deductible:C0}");
	Console.WriteLine($"Limit:      {p.CoverageLimit:C0}");
	Console.WriteLine($"Covers:     {string.Join(", ", p.CoveredIncidents)}");
	Console.WriteLine("__________________________________________");
}