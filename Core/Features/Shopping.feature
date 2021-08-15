Feature: Shopping
	As a customer of shufersal I would like to 
	complete my shop online in order to facilitate my shopping 
	experience

Scenario: Find the cheapest milk and add to cart
	Given Shufersal website is open
	When I search for the cheapest "milk"
	And add the "milk" to the cart
	Then Then the price of the milk with shipping cost is displayed in my cart