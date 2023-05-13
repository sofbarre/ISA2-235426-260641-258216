Feature: CompraSnack

@ComprarSnacks
Scenario: Comprar un ticket y un snack 
	Given a "post" request of ticket
	And a ticketAmount 1
	And a ConcertId 4
	And a snackId 2
	When the request of "Tickets" is sent 
	Then the result should be the code 200

Scenario: Comprar un ticket sin comprar snacks 
	Given a "post" request of ticket
	And a ticketAmount 1
	And a ConcertId 4
	When the request of "Tickets" is sent 
	Then the result should be the code 200

Scenario: Comprar un snack sin comprar un ticket 
	Given a "post" request of ticket
	And a snackId 5
	When the request of "Tickets" is sent 
	Then the result should be the code 400




