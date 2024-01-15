+=========================================================================================================================+
|  ____              ____          _ _     _        _ _           _        _     _       __        ___                  _ |
| / ___| _     _    |  _ \ ___  __| (_)___| |_ _ __(_) |__  _   _| |_ __ _| |__ | | ___  \ \      / (_)______ _ _ __ __| ||
|| |   _| |_ _| |_  | |_) / _ \/ _` | / __| __| '__| | '_ \| | | | __/ _` | '_ \| |/ _ \  \ \ /\ / /| |_  / _` | '__/ _` ||
|| |__|_   _|_   _| |  _ <  __/ (_| | \__ \ |_| |  | | |_) | |_| | || (_| | |_) | |  __/   \ V  V / | |/ / (_| | | | (_| ||
| \____||_|   |_|   |_| \_\___|\__,_|_|___/\__|_|  |_|_.__/ \__,_|\__\__,_|_.__/|_|\___|    \_/\_/  |_/___\__,_|_|  \__,_||
+=========================================================================================================================+

## Introduction
Microsoft Redistributable Wizard is a C# tool developed using .NET 6, designed to identify installed Microsoft Visual C++ Redistributable packages on Windows systems.

## Features
- Detection of all Microsoft Visual C++ Redistributables (2005 to 2022)
- Lists installed and missing redistributables
- Supports both x86 and x64 architectures

## Requirements
- Windows Operating System
- .NET 6 Framework

## Installation and Usage
Compile the project and include it as a project reference into your own project
Search NuGet for the package, click install

## Contributing
If you have suggestions for improvements or new features, feel free to fork this repository and submit a pull request. Here’s how you can contribute:
- Fork the repository
- Create a new branch for your feature (`git checkout -b feature/yourfeature`)
- Commit your changes (`git commit -m 'Add some yourfeature'`)
- Push to the branch (`git push origin feature/yourfeature`)
- Open a pull request

## To-Do
Here are some enhancements and features we're looking to add:
- [ ] Give the user the option to install redistributables (Directly from the Microsoft Website)
- [ ] Auto installer
- [ ] Provide different ways to detect the installation of the packages
- [ ] More comprehensive system compatibility checks