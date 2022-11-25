using CommandLine;
using SbomLicenceCheck.UI.CommandLine;

var result = Parser.Default.ParseArguments<
        CheckLicenceActivity.Options,
        ListLicenseActivity.Options,
        ValidateLicenseActivity.Options>(args)
        .MapResult(
            (CheckLicenceActivity.Options co) => CheckLicenceActivity.Run(co).Result,
            (ListLicenseActivity.Options lo) => ListLicenseActivity.Run(lo),
            (ValidateLicenseActivity.Options vo) => ValidateLicenseActivity.Run(vo).Result,
            errors => HandleError(errors));

Environment.Exit(result);

int HandleError(IEnumerable<Error> errors)
{
    Console.WriteLine("Incorrect arguments, use --help");
    return int.MinValue;
}
