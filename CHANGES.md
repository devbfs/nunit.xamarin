### NUnit Xamarin Runner 3.6.0 - January 26, 2017

This release includes new functionality to write and retrieve a TestResult xml file, via either a TCP port or specified file path. Additional changes are also included to improve intergration with CI systems.

Windows Phone 8.1 support has been removed in this release, to improve compatibility with .NET standard platforms.

#### Issues Resolved

 * 17 Add a TcpListener
 * 24 Create a Test Results file
 * 51 Update to NUnit 3.5
 * 53 Update the README and docs
 * 57 String resource Hello without id in Droid Runner project
 * 59 Enable adding multiple assemblies to test 
 * 60 Added TestOption to change xml result file location
 * 62 Get AppVeyor build working
 * 64 Remove support for Win Phone 8.1
 * 65 Update library versions
 * 66 Remove Text/UI references to NUnit 3.0
 * 70 Make the Icon Larger
 * 74 Document framework version dependency of runner
 * 84 Allow app to be terminated automatically after running the tests
 * 86 Include UWP test runner in CI

### NUnit Xamarin Runner 3.0.1 - December 3, 2015

This release updates the test runner to use the new 3.0.1 NUnit Framework which fixes issues with Async tests on Windows 10 UWP. 

### NUnit Xamarin Runner 3.0.0 - November 18, 2015

This is the first release of the NUnit Xamarin Runners supporting Android, iOS, Windows Phone 8.1 and Windows 10 Universal Apps. 