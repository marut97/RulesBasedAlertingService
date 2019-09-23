# Rules Based Alerting Service

## Installation
Use git clone or just paste the given solution in a directory 
For git clone use the command
```
git clone 
```

Open the solution (.sln) file.
This should open up a solution with projects with the following structure:
- Contracts
- Controllers
- DataAccessLayer
- Hosts
- Models
- Processors
- SharedResources
- Test.E2E
- Test.Unit
- Utility

## Database Setup

Create a connection with 2 databases - One for test and another for production.
Use the following server name
'''
(localdb)\MSSQLLocalDB
'''
Production Database Name: RulesBasedAlertingDB
Test Database Name: RulesBasedAlertingDB.Test
(Refer Queries.txt for creating tables)

## Documents

The following files have the necessary information:
 - Class Diagram: ClassDiagram.jpg
 - User Stories and API Documentation: User Stories.docx (Word File)
 - Simian Code Duplication Report: Simian_Report.jpeg 
 - Cyclomatic Complexity: CyclometricComplexity.xlsx
 - Queries to create the tables are in Queries.txt
 - Test Cases are documented in: Testing.xlsx
 - Axo Cover Report: AxoCoverReport.html
 

