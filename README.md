## Requirements

Open the visual studio solution named gluh.technicaltest.sln from the zip below.

This purpose of this C# application is to calculate the optimal set of suppliers to purchase products from. ‘Optimal’ in this case means the cheapest set of suppliers that have sufficient stock. 

Your goal is to complete the PurchaseOptimizer.Optimize() method. 

Acceptance Criteria
•	The Optimize() method should return a list of ‘optimal’ purchase orders that fulfil the purchase requirements. 
•	All required items must be included in the result, regardless of supplier availability.
•	A maximum of 1 purchase order per supplier should be created.
•	Print the results to the console.

Before starting, ensure you review all the properties of the Models and Database classes, taking note of important fields that may be relevant. Record any assumptions in a comment at the top of the PurchaseOptimizer class.

## Implementation
The nature of the problem means that a minor change can have significant effects on the overall price.
Given the crazy product and delivery prices that can be seen on ebay, there may be a case where it is better to buy 49 pencils from Supplier A and 1 from Supplier B if that pushes Supplier B's overall order into free delivery.

A number of assumptions were made:
* Each supplier should have a unique ID - there were some duplicates in the data file so I fixed them

* The solution should e generic, handling a $1 delivery on a $100 phone or a $10 delivery on a $1 pencil

* If an order cannot be fulfilled, the algorthm should maximise the number of items delivered regardless of cost

* Non physical items still have stock balances (Since there are varying balances for the Anti-virus product, I am assuming that means the shop only owns a certain number of licences. MS Office has zero stock)
   
## Overall implementation
I time-boxed myself on this project. I started with a dumb check of permutations to form a base-line that I check check smarter algorithms against. With the time I allowed, I was not able to come up with a smarter algorithm so tweaked the permutation check instead. Whereas it does work with smaller orders, it does not scale so could not be used as an actual solution.

Given the need to balance unit price vs order delivery costs, I feel that I would probably be supplier-oriented using a cost-benefit check but I did not have time to do this.