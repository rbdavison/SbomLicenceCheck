using CommandLine;

Parser.Default.ParseArguments<
        CheckLicenceActivity.Options, 
        ListLicenseActivity.Options,
        ValidateLicenseActivity.Options>(args)
    .WithParsed<CheckLicenceActivity.Options>(co => CheckLicenceActivity.Run(co))
    .WithParsed<ListLicenseActivity.Options>(lo => ListLicenseActivity.Run(lo))
    .WithParsed<ValidateLicenseActivity.Options>(vo => ValidateLicenseActivity.Run(vo))
    .WithNotParsed(errors => HandleError(errors));

void HandleError(IEnumerable<Error> errors)
{
    Console.WriteLine("Incorrect arguments, use --help");
}
