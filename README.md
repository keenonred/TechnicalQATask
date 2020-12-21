# Technical QA Task
## Description
 
- Automated tests are written in *C#* with *Selenium* and *NUnit*
- Automated tests can be run in Chrome or Firefox albeit Firefox runs much slower due to difference in how Firefox works
- Tests are run on a deployed version of html file [here](https://keenonred.github.io/)
- Test cases are defined in [this document](./TestCases&Bugs.xlsx) on the first tab and bugs are on the second tab
- **Note:** I had to add `Thread.Sleep()` in two test cases as explicit or implicit waits did not help when running with Firefox

## Requirements

- Visual Studio IDE
- Firefox and Chrome browsers
