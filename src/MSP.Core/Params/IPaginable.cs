namespace MSP.Core.Params;

public interface IPaginable
{
    int? Skip { get; set; }
    int? Take { get; set; }
}