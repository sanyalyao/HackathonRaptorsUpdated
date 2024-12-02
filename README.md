# APIQuest

APIQuest was designed to help testers ensure the reliability and functionality of Game Service APIs (like https://polite-gecko-e1beb8.netlify.app/). With our tool, you can easily create and run automated tests for your Game Service APIs, without the need for manual testing.

## Features

Autotesting API for Category "Users":
- Delete a user (api-1)
- Create a new user (api-3)
- Update a user (api-4)
- List all users (api-6)
- Get a user by email and password (api-7)
- List all users (api-21)
- Create a new user (api-22)
- Get a user (api-23)
- Update a user (api-24)

## Tech Environment

### Installed packages:
|Package|Version|
|-------|-------|
| Handlebars.Net.Helpers.Xeger | {2.4.7} |
| NUnit.Analyzers | {3.6.1} |
| NLog.Extensions.Logging | {5.3.14} |
| Microsoft.NET.Test.Sdk | {17.6.0} |
| NLog.Targets.Seq | {4.0.2-dev-00175} |
| NUnit | {3.13.3} |
| Faker.Net | {2.0.163} |
| Allure.NUnit | {2.12.1} |
| RestSharp | {112.1.0} |
| coverlet.collector | {6.0.0} |
| NLog | {5.3.4} |
| NUnit3TestAdapter | {4.2.1} |   

### Access

In the RunSettings => settings.runsettings set you email to get access:

```sh
<?xml version="1.0" encoding="utf-8"?>
<RunSettings>
	<TestRunParameters>
		<Parameter name="ReleaseEndpoint" value="https://release-gs.qa-playground.com/api/v1/" />
		<Parameter name="DevEndpoint" value="https://dev-gs.qa-playground.com/api/v1/" />
		<Parameter name="Email" value="your_email@example.exp"/>
		<Parameter name="Environment" value="release"/>
	</TestRunParameters>
</RunSettings>
```

If you want to change Environment, just set "release" or "dev":

```sh
<Parameter name="Environment" value="release"/>
```

### Reporting and documantation

You need to install the below tools

#### Allure

Installation - https://www.nuget.org/packages/Allure.NUnit/

#### Seq

Installation with NLog - https://docs.datalust.co/docs/using-nlog

#### NLog

Installation - https://www.nuget.org/packages/nlog

### Bug Reports

Here bugs were found during QA Hackathon - https://docs.google.com/spreadsheets/d/1zIwuaH6AJMYW0wdOSM4WY-2fKOmR16X-SwWbWlsx_tM/edit?gid=0#gid=0