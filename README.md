# SbomLicenceCheck

[![Build Pipeline](https://github.com/crew-briggs/SbomLicenceCheck/actions/workflows/dotnet.yml/badge.svg)](https://github.com/crew-briggs/SbomLicenceCheck/actions/workflows/dotnet.yml)

A Dotnet Command Line Tool Using a Syft generated CyloneDX formatted SBOM (JSON and XML) to display and validate OSS licences.

## Installation

```dotnet tool install --global SbomLicenceCheck```

Hosted as a [Nuget package](https://www.nuget.org/packages/SbomLicenceCheck/).

## Command Line Parameters

### List

Displays a table containing all SPDX recognised licence types.

Taken from [here](https://raw.githubusercontent.com/spdx/Licence-list-data/master/json/Licences.json).

```SbomLicenceCheck list```

e.g.

```terminal
List version a8f83ee
| id  | Licence Id                           | Osi Approved? |
|-----|--------------------------------------|---------------|
| 0   | xpp                                  | False         |
| 1   | GFDL-1.1-only                        | False         |
| 2   | GPL-2.0-with-autoconf-exception      | False         |
| 3   | copyleft-next-0.3.1                  | False         |
| 4   | CC-BY-NC-2.5                         | False         |
| 5   | CC-BY-NC-SA-3.0-IGO                  | False         |
| 6   | QPL-1.0                              | True          |
| 7   | ErlPL-1.1                            | False         |
...
```

### Check

Display the licence types of the components listed within a specified Software Bill of Materials (SBoM) file.

```SbomLicenceCheck check -b <sbom file>```

e.g.

```terminal

| Component              | Id  | Licence Id   | Osi Approved? |
|------------------------|-----|--------------|---------------|
| alpine-baselayout      | 212 | GPL-2.0-only | True          |
| alpine-baselayout-data | 212 | GPL-2.0-only | True          |
| alpine-keys            | 317 | MIT          | True          |
| apk-tools              | 212 | GPL-2.0-only | True          |
| busybox                | 212 | GPL-2.0-only | True          |
| ca-certificates-bundle | 13  | MPL-2.0      | True          |
...
```

### Validate

Will validate the licences found within an SBoM against a specified list of licence Ids (Ids are listed in the ```list``` command)

```SbomLicence validate -b <sbom file> -i 317```

e.g.

```terminal
Warning: Invalid Licences Found.
| Component              | Id  | Licence Id   | Osi Approved? |
|------------------------|-----|--------------|---------------|
| alpine-baselayout      | 212 | GPL-2.0-only | True          |
| alpine-baselayout-data | 212 | GPL-2.0-only | True          |
| apk-tools              | 212 | GPL-2.0-only | True          |
| busybox                | 212 | GPL-2.0-only | True          |
| ca-certificates-bundle | 13  | MPL-2.0      | True          |
| libc-utils             | 488 | BSD-2-Clause | True          |
...
```

Displays a table of licences which aren't specified on the command line.

```SbomLicence validate -b <sbom file> -n MIT```

e.g.

```terminal
Success: No Invalid Licences Found.
```

All licences found were 'valid'.

### Help

```SbomLicence --help```

e.g. 

```terminal
  check       Check the licence type.

  list        List and filter known licence types.

  validate    Validate the licence types.

  help        Display more information on a specific command.

  version     Display version information.
```

### Output Formats

By default **Markdown** is the output format for all data, but **Json** is also supported.

Specify it with ```-f``` or ```-format```

```SbomLicence validate -b <sbom file> -n MIT -f Json```

e.g.

```terminal
{
  "Pillow": [
    {
      "ReferenceNumber": 28,
      "licenseId": "HPND",
      "isOsiApproved": true
    }
  ]
}
```
