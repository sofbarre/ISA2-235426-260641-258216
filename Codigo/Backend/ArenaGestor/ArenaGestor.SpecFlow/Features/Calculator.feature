Feature: ManteniminetoSnacks
![Calculator](https://specflow.org/wp-content/uploads/2020/09/calculator.png)
Simple calculator for adding **two** numbers

Link to a feature: [Calculator](ArenaGestor.SpecFlow/Features/Calculator.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@GetSnacks
Scenario: Obtener snacks
	Given a "get" request of snacks
	When the request of "snacks" is sent 
	Then the result should be the code "200"

Scenario: obtener snack especifico
	Given a "get" request of snacks
	And a snack with id "1" 
	When the request of "snacks" is sent 
	Then the result should be the code "200"

Scenario: obtener snack inexistente
	Given a "get" request of snacks
	And a snack with "10" 
	When the request of "snacks" is sent 
	Then the result should be the code "404"

Scenario: creacion de snack
Given a snack name "Papas"
And a snack description "Papas Lays"
And a snack price "20"
And a "post" request of snacks
When the request of "snacks" is sent 
Then the result should be the code "200"

Scenario: creacion de snack precio invalido 
Given a snack name "Papas"
And a snack description "Papas Lays"
And a snack price "-20"
And a "post" request of snacks
When the request of "snacks" is sent 
Then the result should be the code "400"

Scenario: creacion de snack nombre invalido 
Given a snack name ""
And a snack description "Papas Lays"
And a snack price "20"
And a "post" request of snacks
When the request of "snacks" is sent 
Then the result should be the code "400"

Scenario: modificacion de snack
Given a snack with id "1"
And a snack name "Papas"
And a snack description "Papas Lays"
And a snack price "20"
And a "put" request of snacks
When the request of "snacks" is sent 
Then the result should be the code "200"

Scenario: modificacion de snack no existente
Given a snack with id "12"
And a snack name "Papas"
And a snack description "Papas Lays"
And a snack price "20"
And a "put" request of snacks
When the request of "snacks" is sent 
Then the result should be the code "404"

Scenario: modificacion de snack nombre invalido
Given a snack with id "1"
And a snack name ""
And a snack description "Papas Lays"
And a snack price "20"
And a "put" request of snacks
When the request of "snacks" is sent 
Then the result should be the code "404"

Scenario: modificacion de snack precio invalido
Given a snack with id "1"
And a snack name "Papas"
And a snack description "Papas Lays"
And a snack price "-20"
And a "put" request of snacks
When the request of "snacks" is sent 
Then the result should be the code "404"

Scenario: borrar snack especifico
Given a snack with id "1"
And a "delete" request of snacks
When the request of "snacks" is sent 
Then the result should be the code "200"

Scenario: borrar snack inexistente
Given a snack with id "13"
And a "delete" request of snacks
When the request of "snacks" is sent 
Then the result should be the code "404"
