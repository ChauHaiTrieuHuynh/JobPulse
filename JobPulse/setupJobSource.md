# Setup the Job Source

## 1. Overview
- Setup the job source including url, filtering, and scraping rules.
- Make sure the configuration is flexible to support multiple job sources in the future.

## 2. Planing
- Define the job source structure and required fields.
- Define the filtering and scraping rules for each job source.
- Define the configuration format (e.g., JSON, YAML) for job sources.
- Define the configuration loading mechanism (e.g., from file, environment variables, or database).
- Handling the error using log and retry mechanism.

## 3. Testing 
- Doing a simple test by loading the configuration and see the output results following the expectation or not.
- Take note or document the output to support the mapping process in the next step.

## 4. Implementation Procedure
- Choosing the Setting Models to move to service (based on consumer and producer pattern - not move all just to sync folder).
- Define the Interface and setup DI:
	+ using HttpClient to fetch so DI need AddHttpClient
	+ including add the header configuration in DI to avoid refuse bot or automate from the side server as well
	+ Update Worker configuration name including appsetting attribute, setting models amd class name for easy to read and understand.

## 5. Plan for Future Enhancements to avoid regression and keep the source code clean
- Isolate the fetch feature, not merge to dev right not
- Create a stack branch to develop new feature based on the current fetch:
	+ The new feature is depend from fetch job source so in any circumtance will be cherry pick from dev to avoid system break and easy to approach to new solution
	without doing large modify the new feature
- With new feature, will apporach as module to loose coupling from the risky fetch job source in testing process.

- Will continuouslly update if any change 

  