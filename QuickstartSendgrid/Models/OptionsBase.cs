namespace QuickstartSendgrid.Models;

public abstract class OptionsBase
{
    const string OPTIONS_SUFFIX = "Options";
    
    virtual public string GetName()
        => this.GetType().Name.Replace(OPTIONS_SUFFIX, string.Empty);
}
