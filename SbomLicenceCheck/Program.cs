using CommandLine;
using SbomLicenceCheck.UI.CommandLine;

    
var result = Parser.Default
    .ParseArguments<
        CheckLicenceActivity.Options,
        ListLicenceActivity.Options,
        ValidateLicenceActivity.Options>(args)
    .MapResult(
            (CheckLicenceActivity.Options co) => CheckLicenceActivity.Run(co).Result,
            (ListLicenceActivity.Options lo) => ListLicenceActivity.Run(lo),
            (ValidateLicenceActivity.Options vo) => ValidateLicenceActivity.Run(vo).Result,
            errors => HandleError(errors));

Environment.Exit(result);

int HandleError(IEnumerable<Error> errors)
{
    Console.WriteLine("Incorrect arguments, use --help");
    return int.MinValue;
}
